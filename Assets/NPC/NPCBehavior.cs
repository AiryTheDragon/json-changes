using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
using TMPro;
	using Pathfinding.Util;

public class NPCBehavior : AIPath, INeedsClockUpdate
{
    //public List<GameObject> paths;

    private Player Player;

    [SerializeField] private AudioClip _halt = null;

    // Activity Variables.
    public List<GroupOfActivities> ActivityGroups;
    public RunActivityGroups ActivityTracker;

    public CharacterBehavior CharacterBehavior;

    private DateTime LastPathfind = DateTime.Now;

    private ClockTime waitUntil;

    [SerializeField] float messageDuration = 5f;
    private float messageTimeRemaining;
    private bool isMessage = false;
    public GameObject speechObject;

    public string PositionName;

    public string Name;

    public int Value;

    public int ManipulationLevel;

    public Vector2 Velocity;

    public double Suspicion { get; private set; }

    public double MaxSuspicion =  500;

    public bool beingEscorted;

    public Transform home;

    private ClockBehavior Clock;

    public GameObject WaypointPrefab;

    public GameObject ReturnLocation;

    public AudioSource _source = null;

    //[SerializeField] private AudioClip _ow = null;
    //private AudioSource _source = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        speechObject.SetActive(false);
        Clock = GameObject.Find("Clock").GetComponent<ClockBehavior>();
        Clock.NeedsClockUpdate.Add(this);
        Player = GameObject.Find("Player").GetComponent<Player>();
        if(ActivityTracker is null)
        {
            ActivityTracker = new RunActivityGroups(ActivityGroups);
        }
        if (ActivityGroups != null  && ActivityGroups.Count>0)
        { 
            RunNextActivityGroup();
        }

        _source = GetComponent<AudioSource>();
        if (_source == null)
        {
            Debug.Log("Audio Source is NULL");
        }
        else
        {
            _source.clip = _halt;
        }


        if (home is null)
        {
            var waypoint = Instantiate(WaypointPrefab);
            waypoint.transform.SetParent(GameObject.Find("Clock").transform, true);
            home = waypoint.transform;
        }

        //GetComponent<AIDestinationSetter>().target = runningActivity.GetDestination().GetComponent<Transform>();
        //base.Start();
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (isMessage)
        {
            messageTimeRemaining -= Time.deltaTime;

            if (messageTimeRemaining < 0)
            {
                speechObject.SetActive(false);
                isMessage = false;
            }
        }

        if(ActivityTracker.GetCurrentAction() is ActivityEscortPlayer)
        {
            Player.GetComponent<Transform>().position = GetComponent<Transform>().position + (new Vector3(Velocity.normalized.x, Velocity.normalized.y, 0) * 0.6f);
        }
        else if(ActivityTracker.GetCurrentAction() is ActivityEscortNPC)
        {
            GetComponentInChildren<GuardBehavior>().Target.GetComponent<Transform>().position = GetComponent<Transform>().position + (new Vector3(Velocity.normalized.x, Velocity.normalized.y, 0) * 0.6f);
        }
        else if (ActivityTracker.GetCurrentAction() is ActivityCatchPlayer)
        {
            var activity = (ActivityCatchPlayer)ActivityTracker.GetCurrentAction();
            if((GetComponent<Transform>().position - Player.GetComponent<Transform>().position).magnitude < 0.6)
            {
                ActivityTracker.CompleteAction(Clock.Time);
                BeginAction(ActivityTracker.GetCurrentAction());
            }
        }
        else if (ActivityTracker.GetCurrentAction() is ActivityCatchNPC)
        {
            var activity = (ActivityCatchNPC)ActivityTracker.GetCurrentAction();
            if((GetComponent<Transform>().position - GetComponentInChildren<GuardBehavior>().Target.GetComponent<Transform>().position).magnitude < 0.6)
            {
                ActivityTracker.CompleteAction(Clock.Time);
                BeginAction(ActivityTracker.GetCurrentAction());
            }
        }
    }

    /// <summary>Called during either Update or FixedUpdate depending on if rigidbodies are used for movement or not</summary>
    protected override void MovementUpdateInternal (float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation) {
        float currentAcceleration = maxAcceleration;

        // If negative, calculate the acceleration from the max speed
        if (currentAcceleration < 0) currentAcceleration *= -maxSpeed;

        if (updatePosition) {
            // Get our current position. We read from transform.position as few times as possible as it is relatively slow
            // (at least compared to a local variable)
            simulatedPosition = tr.position;
        }
        if (updateRotation) simulatedRotation = tr.rotation;

        var currentPosition = simulatedPosition;

        // Update which point we are moving towards
        interpolator.MoveToCircleIntersection2D(currentPosition, pickNextWaypointDist, movementPlane);
        var dir = movementPlane.ToPlane(steeringTarget - currentPosition);

        // Calculate the distance to the end of the path
        float distanceToEnd = dir.magnitude + Mathf.Max(0, interpolator.remainingDistance);

        // Check if we have reached the target
        var prevTargetReached = reachedEndOfPath;
        reachedEndOfPath = distanceToEnd <= endReachedDistance && interpolator.valid;
        if (!prevTargetReached && reachedEndOfPath) OnTargetReached();
        float slowdown;

        // Normalized direction of where the agent is looking
        var forwards = movementPlane.ToPlane(simulatedRotation * (orientation == OrientationMode.YAxisForward ? Vector3.up : Vector3.forward));

        // Check if we have a valid path to follow and some other script has not stopped the character
        bool stopped = isStopped || (reachedDestination && whenCloseToDestination == CloseToDestinationMode.Stop);
        if (interpolator.valid && !stopped) {
            // How fast to move depending on the distance to the destination.
            // Move slower as the character gets closer to the destination.
            // This is always a value between 0 and 1.
            slowdown = distanceToEnd < slowdownDistance? Mathf.Sqrt(distanceToEnd / slowdownDistance) : 1;

            if (reachedEndOfPath && whenCloseToDestination == CloseToDestinationMode.Stop) {
                // Slow down as quickly as possible
                velocity2D -= Vector2.ClampMagnitude(velocity2D, currentAcceleration * deltaTime);
            } else {
                velocity2D += MovementUtilities.CalculateAccelerationToReachPoint(dir, dir.normalized*maxSpeed, velocity2D, currentAcceleration, rotationSpeed, maxSpeed, forwards) * deltaTime;
            }
        } else {
            slowdown = 1;
            // Slow down as quickly as possible
            velocity2D -= Vector2.ClampMagnitude(velocity2D, currentAcceleration * deltaTime);
        }

        velocity2D = MovementUtilities.ClampVelocity(velocity2D, maxSpeed, slowdown, slowWhenNotFacingTarget && enableRotation, forwards);

        CharacterBehavior.UpdateHead(velocity2D.x, velocity2D.y);

        ApplyGravity(deltaTime);
        Velocity = velocity2D;

        // Set how much the agent wants to move during this frame
        var delta2D = lastDeltaPosition = CalculateDeltaToMoveThisFrame(movementPlane.ToPlane(currentPosition), distanceToEnd, deltaTime);
        nextPosition = currentPosition + movementPlane.ToWorld(delta2D, verticalVelocity * lastDeltaTime);
        CalculateNextRotation(slowdown, out nextRotation);
    }

    void OnMouseUpAsButton()
    {
        if(IsHidden())
        {
            return;
        }
        var person = GetPersonInformation();
        if(Player.PeopleKnown.ContainsKey(person.Name))
        {
            if(ActivityTracker.RunningActivity != null &&
                !Player.PeopleKnown[person.Name].SeenActivities.Any(x => x.SystemName == ActivityTracker.RunningActivity.name && x.ReadableName == ActivityTracker.RunningActivity.Name))
            {
                Player.PeopleKnown[person.Name].SeenActivities.Add(new SeenActivity() { SystemName = ActivityTracker.RunningActivity.name,
                                                                                        ReadableName = ActivityTracker.RunningActivity.Name });
            }
        }
        else
        {
            Player.PeopleKnown.Add(this.Name, person);
            if(ActivityTracker.RunningActivity != null)
            {
                person.SeenActivities.Add(new SeenActivity() { SystemName = ActivityTracker.RunningActivity.name,
                                                               ReadableName = ActivityTracker.RunningActivity.Name });
            }
        }
        Player.NPCInfoUI.OpenNPCInfo(this);
    }

    public Person GetPersonInformation()
    {
        var person = new Person();
        person.Name = Name;
        person.ManipulationLevel = ManipulationLevel;
        person.Value = Value;
        person.PositionName = PositionName;
        return person;
    }

    public void RunNextActivityGroup()
    {
        ActivityTracker.RunNextActivityGroup(Clock.Time);
        BeginAction(ActivityTracker.RunningAction);
    }

    /*public void RunActivityGroup(int index)
    {
        ActivityGroupIndex = index;
        if(ActivityGroupIndex > ActivityGroups.Count)
        {
            throw new Exception("Activity group index out of range!");
        }
        RunActivity(0);
    }

    public void RunActivity(int index)
    {
        ActivityIndex = index;
        if(ActivityIndex > ActivityGroups[ActivityGroupIndex].Activities.Count)
        {
            throw new Exception("Activity index out of range for activity group!");
        }
        RunActivity(Activities[index]);
    }

    public void RunActivity(Activity activity)
    {
        runningActivity = new RunActivity(activity);
        BeginAction(runningActivity.GetCurrentAction());
    }*/

    public void BeginAction(ActivityAction action)
    {
        if(action is null)
        {
            Clock.NeedsClockUpdate.Add(this);
            return;
        }
        if(action is ActivityWait)
        {
            waitUntil = new ClockTime(Clock.Time);
            waitUntil.AddMinutes(((ActivityWait)action).Minutes);
            Clock.NeedsClockUpdate.Add(this);
        }
        else if (action is ActivityRepeat)
        {
            throw new Exception("ActivityRepeat is depreciated! Do not use it!");
            //runningActivity.ResetActivity();
            //BeginAction(runningActivity.GetCurrentAction());
        }
        else if (action is ActivityWalk)
        {
            var walk = (ActivityWalk)action;
            GetComponent<AIDestinationSetter>().target = walk.Destination.GetComponent<Transform>();
        }
        else if (action is ActivityWaitUntilTime)
        {
            waitUntil = new ClockTime(((ActivityWaitUntilTime)action).Time);
            waitUntil.Day = Clock.Time.Day;
            Clock.NeedsClockUpdate.Add(this);
        }
        else if (action is ActivitySpeak)
        {
            createMessage(((ActivitySpeak)action).text);
            ActivityTracker.CompleteAction(Clock.Time);
            BeginAction(ActivityTracker.GetCurrentAction());
        }
        else if (action is ActivityTurnOn)
        {
            ((ActivityTurnOn)action).lampObject.GetComponent<LampBehavior>().TurnOn();
            ActivityTracker.CompleteAction(Clock.Time);
            BeginAction(ActivityTracker.GetCurrentAction());
        }
        else if (action is ActivityTurnOff)
        {
            ((ActivityTurnOff)action).lampObject.GetComponent<LampBehavior>().TurnOff();
            ActivityTracker.CompleteAction(Clock.Time);
            BeginAction(ActivityTracker.GetCurrentAction());

        }
        else if (action is ActivityCatchPlayer)
        {
            GetComponent<AIDestinationSetter>().target = Player.GetComponent<Transform>();
            //Player._source.Stop();
            //((ActivityCatchPlayer)action).chaseMusic.Play();
        }
        else if (action is ActivityCatchNPC)
        {
            var npcAction = (ActivityCatchNPC)action;
            GetComponent<AIDestinationSetter>().target = gameObject.GetComponentInChildren<GuardBehavior>().Target.GetComponent<Transform>();
        }
        else if (action is ActivityEscortPlayer)
        {
            GetComponent<AIDestinationSetter>().target = ((ActivityEscortPlayer)action).Destination.GetComponent<Transform>();
            Player.beingEscorted = true;
            //Player._source.Stop();
            //((ActivityEscortPlayer)action).escortMusic.Play();
        }
        else if (action is ActivityEscortNPC)
        {
            //GetComponent<AIDestinationSetter>().target = ((ActivityEscortNPC)action).Destination.GetComponent<Transform>();
            GetComponent<AIDestinationSetter>().target = GetComponentInChildren<GuardBehavior>().Target.GetComponent<NPCBehavior>().ReturnLocation.GetComponent<Transform>();
            GetComponentInChildren<GuardBehavior>().Target.GetComponent<NPCBehavior>().beingEscorted = true;
        }
        else if (action is ActivityEndEscort)
        {
            //GetComponentInChildren<GuardBehavior>().Patrolling = true;
            RunNextActivityGroup();
            //Player._source.Stop();
        }
        else if (action is ActivityBGMusicStop)  
        {
            ((ActivityBGMusicStop)action).stopClip();
            ActivityTracker.CompleteAction(Clock.Time);
            BeginAction(ActivityTracker.GetCurrentAction());
        }
        else if (action is ActivityBGMusicUpdate) 
        {
            ((ActivityBGMusicUpdate)action).playClip();
            ActivityTracker.CompleteAction(Clock.Time);
            BeginAction(ActivityTracker.GetCurrentAction());
        }
        else if (action is ActivityPlaySound)
        {
            _source.clip = ((ActivityPlaySound)action).audioClip;
            _source.Play();
            ActivityTracker.CompleteAction(Clock.Time);
            BeginAction(ActivityTracker.GetCurrentAction());
        }

        else if (action is ActivityEnd)
        {
            throw new Exception("Do not use ActivityEnd! It is depreciated!");
            /* 
            if(activityPos + 1 < Activities.Count)
            {
                RunActivity(activityPos+1);
                return;
            }
            RunActivity(0);*/
        }
        else if (action is ActivityGoHome)
        {
            GetComponent<AIDestinationSetter>().target = home;
            Debug.Log("Going Home");
        }
        else if (action is ActivityFindNewActivity)
        {
            
        }
        else if (action is ActivityStartPatrol)
        {
            GetComponentInChildren<GuardBehavior>().Patrolling = true;
            ActivityTracker.CompleteAction(Clock.Time);
            BeginAction(ActivityTracker.GetCurrentAction());
        }
        else if (action is ActivityEndPatrol)
        {
            GetComponentInChildren<GuardBehavior>().Patrolling = false;
            ActivityTracker.CompleteAction(Clock.Time);
            BeginAction(ActivityTracker.GetCurrentAction());
        }
    }


    public override void OnTargetReached()
    {
        /*runningActivity.DestinationReached();
        if(runningActivity.GetDestination() != null)
        {
            GetComponent<AIDestinationSetter>().target = runningActivity.GetDestination().GetComponent<Transform>();
        }*/
        if(ActivityTracker.GetCurrentAction() is ActivityWalk || ActivityTracker.GetCurrentAction() is ActivityGoHome)
        {
            ActivityTracker.CompleteAction(Clock.Time);
            BeginAction(ActivityTracker.GetCurrentAction());
        }
        else if (ActivityTracker.GetCurrentAction() is ActivityCatchPlayer)
        {
            ActivityTracker.CompleteAction(Clock.Time);
            BeginAction(ActivityTracker.GetCurrentAction());
        }
        else if (ActivityTracker.GetCurrentAction() is ActivityCatchNPC)
        {
            ActivityTracker.CompleteAction(Clock.Time);
            BeginAction(ActivityTracker.GetCurrentAction());
        }
        else if(ActivityTracker.GetCurrentAction() is ActivityEscortNPC)
        {
            ActivityTracker.CompleteAction(Clock.Time);
            GetComponentInChildren<GuardBehavior>().Target.GetComponent<NPCBehavior>().beingEscorted = false;
            BeginAction(ActivityTracker.GetCurrentAction());
        }
        else if(ActivityTracker.GetCurrentAction() is ActivityEscortPlayer)
        {
            ActivityTracker.CompleteAction(Clock.Time);
            Player.beingEscorted = false;
            BeginAction(ActivityTracker.GetCurrentAction());
        }
        base.OnTargetReached();
    }

    public void UpdateClock(ClockTime time)
    {
        if(ActivityTracker.GetCurrentAction() is ActivityWait)
        {
            if(waitUntil.Day < time.Day ||
                waitUntil.Day == time.Day && waitUntil.Hour < time.Hour ||
                waitUntil.Day == time.Day && waitUntil.Hour == time.Hour && waitUntil.Minute <= time.Minute)
            {
                ActivityTracker.CompleteAction(Clock.Time);
                Clock.NeedsClockUpdate.Remove(this);
                BeginAction(ActivityTracker.GetCurrentAction());
            }
        }
        else if (ActivityTracker.GetCurrentAction() is ActivityWaitUntilTime)
        {
            if(waitUntil.Day < time.Day ||
                waitUntil.Day == time.Day && waitUntil.Hour < time.Hour ||
                waitUntil.Day == time.Day && waitUntil.Hour == time.Hour && waitUntil.Minute <= time.Minute)
            {
                ActivityTracker.CompleteAction(Clock.Time);
                Clock.NeedsClockUpdate.Remove(this);
                BeginAction(ActivityTracker.GetCurrentAction());
            }
        }
        else if (ActivityTracker.GetCurrentAction() is ActivityCatchPlayer)
        {
            var activity = (ActivityCatchPlayer)ActivityTracker.GetCurrentAction();
            if(activity.DistanceLimit >= 0 && Vector3.Distance(Player.GetComponent<Transform>().position, GetComponent<Transform>().position) > activity.DistanceLimit)
            {
                RunNextActivityGroup();
            }
        }
        else if (ActivityTracker.GetCurrentAction() is ActivityCatchNPC)
        {
            var activity = (ActivityCatchNPC)ActivityTracker.GetCurrentAction();
            if(activity.DistanceLimit >= 0 && Vector3.Distance(
                gameObject.GetComponentInChildren<GuardBehavior>().Target.GetComponent<Transform>().position,
                GetComponent<Transform>().position) > activity.DistanceLimit)
            {
                RunNextActivityGroup();
            }
        }
        else if (ActivityTracker.GetCurrentAction() is null)
        {
            ActivityTracker.RunNextActivityGroup(time);
            if(ActivityTracker.GetCurrentAction() != null)
            {
                Clock.NeedsClockUpdate.Remove(this);
                BeginAction(ActivityTracker.GetCurrentAction());
            }
        }
    }

    bool IsHidden()
    {
        Vector3 position = GetComponent<Transform>().position;
        var castRay = new Ray(position + new Vector3(0, 0, 1), new Vector3(0, 0, -1));
        var hits = Physics2D.OverlapBoxAll(new Vector2(position.x - 0.5f, position.y - 0.5f), new Vector2(1, 1), 0.0f);
        for(int i = 0; i < hits.Length; i++)
        {
            if(hits[i].gameObject.tag == "Ceiling" && hits[i].gameObject.GetComponentInChildren<SpriteRenderer>().enabled)
            {
                return true;
            }
        }

        return false;
    }

    void createMessage(string text)
    {
        // First we need to check if we're able to say hi.
        if(IsHidden())
        {
            return;
        }
        
        

        speechObject.SetActive(true);

        speechObject.GetComponentInChildren<TextMeshPro>().text = text;
        messageTimeRemaining = messageDuration;
        isMessage = true;
    }

    // This method sets the current activity action, activity and group of activity, based on information given object.  It does so by using the
    // current information stored in the NPC's Activity groups
    public void setBehavior(BehaviorSet behaviorSet)
    {
        // set the group of activities
        bool found = false;
        for (int i=0; i< ActivityTracker.ActivityGroups.Count && !found; i++)
        {
            if (ActivityTracker.ActivityGroups[i].GroupName.Equals(behaviorSet.groupOfActivitiesName))
            {
                found = true;
                ActivityTracker.ActivityGroupIndex = i;
                ActivityTracker.RunningGroup = ActivityTracker.ActivityGroups[i];
            }
        }
        if (!found)
        {
            ActivityTracker.ActivityGroupIndex = 0;
            ActivityTracker.RunningGroup = ActivityTracker.ActivityGroups[0];
            ActivityTracker.ActivityIndex = 0;
            ActivityTracker.RunningActivity = ActivityTracker.RunningGroup.Activities[0];
            ActivityTracker.ActionIndex = 0;
            ActivityTracker.RunningAction = ActivityTracker.RunningActivity.actions[0];

            Debug.LogError("Activity Group " + behaviorSet.groupOfActivitiesName + " not found, resetting all indexes in Activity Tracker to 0.");
            return;
        }

        ActivityTracker.ActivityIndex = behaviorSet.activityPos;
        ActivityTracker.RunningActivity = ActivityTracker.RunningGroup.Activities[ActivityTracker.ActivityIndex];
        ActivityTracker.ActionIndex = behaviorSet.actionPos;
        ActivityTracker.RunningAction = ActivityTracker.RunningActivity.actions[ActivityTracker.ActionIndex];

        // some activities need set up
        if (behaviorSet.actionInfo.type == ActivityActionType.CatchNPC)
        {
            if (!(ActivityTracker.RunningAction is ActivityCatchNPC))
            {
                Debug.LogError("Action " + ActivityTracker.RunningAction.name + " is not of type " +behaviorSet.actionInfo.type + ".");
                return;
            }
            //TODO
            Debug.Log("NPC " + name + " needs to be set to catch NPC " + behaviorSet.actionInfo.data + ".");
        }
        else if (behaviorSet.actionInfo.type == ActivityActionType.CatchPlayer)
        {
            //TODO
            Debug.Log("NPC " + name + " needs to be set to catch Player.");
        }
        else if (behaviorSet.actionInfo.type == ActivityActionType.EscortNPC)
        {
            if (!(ActivityTracker.RunningAction is ActivityEscortNPC))
            {
                Debug.LogError("Action " + ActivityTracker.RunningAction.name + " is not of type " + behaviorSet.actionInfo.type + ".");
                return;
            }
            //TODO
            Debug.Log("NPC " + name + " needs to be set to escort NPC " + behaviorSet.actionInfo.data + ".");
        }
        else if (behaviorSet.actionInfo.type == ActivityActionType.EscortPlayer)
        {
            //TODO
            Debug.Log("NPC " + name + " needs to be set to escort Player.");
        }
        else if (behaviorSet.actionInfo.type == ActivityActionType.Wait)
        {
            waitUntil = behaviorSet.actionInfo.endTime;
        }

        if (behaviorSet.isGuard) // if NPC is a guard, check patrolling status
        {
            gameObject.GetComponentInChildren<GuardBehavior>().Patrolling = behaviorSet.isPatrolling;
        }

    }

    // This method gets the current activity action, activity and group of activity information and saves it as an object which can be later reset
    public BehaviorSet getBehavior()
    {
        ActivityActionSet actionInfo = new();
        BehaviorSet behaviorInfo = new();
        ActivityAction action = ActivityTracker.GetCurrentAction();

        behaviorInfo.NPCName = Name;
        Debug.Log("Storing " + Name + "'s behaviors.");

        behaviorInfo.groupOfActivitiesName = ActivityTracker.RunningGroup.GroupName;
        behaviorInfo.activityPos = ActivityTracker.ActivityIndex;
        behaviorInfo.actionPos = ActivityTracker.ActionIndex;

        if (action is ActivityBGMusicStop )
        {
            actionInfo.type = ActivityActionType.BGMusicStop;
        }
        else if (action is ActivityBGMusicUpdate)
        {
            actionInfo.type = ActivityActionType.BGMusicUpdate;
        }
        else if (action is ActivityCatchNPC)
        {
            actionInfo.type = ActivityActionType.CatchNPC;
            actionInfo.data = gameObject.GetComponentInChildren<GuardBehavior>().Target.name;
        }
        else if (action is ActivityCatchPlayer)
        {
            actionInfo.type = ActivityActionType.CatchPlayer;
        }
        else if (action is ActivityEndEscort)
        {
            actionInfo.type = ActivityActionType.EndEscort;
        }
        else if (action is ActivityEndPatrol)
        {
            actionInfo.type = ActivityActionType.EndPatrol;
        }
        else if (action is ActivityEscortNPC)
        {
            actionInfo.type = ActivityActionType.EscortNPC;
            actionInfo.data = gameObject.GetComponentInChildren<GuardBehavior>().Target.name;
        }
        else if (action is ActivityEscortPlayer)
        {
            actionInfo.type = ActivityActionType.EscortPlayer;
        }
        else if (action is ActivityPlaySound)
        {
            actionInfo.type = ActivityActionType.PlaySound;
        }
        else if (action is ActivitySpeak)
        {
            actionInfo.type = ActivityActionType.Speak;
        }
        else if (action is ActivityStartPatrol)
        {
            actionInfo.type = ActivityActionType.StartPartrol;
        }
        else if (action is ActivityTurnOff)
        {
            actionInfo.type = ActivityActionType.TurnOff;
        }
        else if (action is ActivityTurnOn)
        {
            actionInfo.type = ActivityActionType.TurnOn;
        }
        else if (action is ActivityWait)
        {
            actionInfo.type = ActivityActionType.Wait;
            actionInfo.endTime = waitUntil;
        }
        else if (action is ActivityWaitUntilTime)
        {
            actionInfo.type = ActivityActionType.WaitUntil;
        }
        else if (action is ActivityWalk)
        {
            actionInfo.type = ActivityActionType.Walk;
        }
        else
        {
            actionInfo.type = ActivityActionType.Unknown;
            Debug.Log("No activity associated with this action.");
        }
        behaviorInfo.actionInfo = actionInfo;

        // check if NPC is a guard and set patrolling info
        GuardBehavior guardBehavior = gameObject.GetComponentInChildren<GuardBehavior>();
        if (guardBehavior==null)    
        {
            behaviorInfo.isGuard = false;
            behaviorInfo.isPatrolling = false;
        }
        else
        {
            behaviorInfo.isGuard = true;
            behaviorInfo.isPatrolling = guardBehavior.Patrolling;
        }



        return behaviorInfo;
    }

    public void AddSuspicion(double amount)
    {
        this.Suspicion += amount;
    }

}