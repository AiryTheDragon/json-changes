using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
using TMPro;
	using Pathfinding.Util;

public class NPCBehavior : AIPath, INeedsClockUpdate
{
    //public List<GameObject> paths;

    public Player Player;

    public List<Activity> Activities;

    public CharacterBehavior CharacterBehavior;

    private DateTime LastPathfind = DateTime.Now;

    public RunActivity runningActivity;

    private ClockTime waitUntil;

    [SerializeField] float messageDuration = 5f;
    private float messageTimeRemaining;
    private bool isMessage = false;
    public GameObject speechObject;

    public string PositionName;

    public string Name;

    public int Value;

    public int ManipulationLevel;

    //[SerializeField] private AudioClip _ow = null;
    //private AudioSource _source = null;


    // Start is called before the first frame update
    protected override void Start()
    {
        speechObject.SetActive(false);


        if (Activities != null  && Activities.Count>0)
        { 
            RunActivity(Activities[0]);
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


        // Set how much the agent wants to move during this frame
        var delta2D = lastDeltaPosition = CalculateDeltaToMoveThisFrame(movementPlane.ToPlane(currentPosition), distanceToEnd, deltaTime);
        nextPosition = currentPosition + movementPlane.ToWorld(delta2D, verticalVelocity * lastDeltaTime);
        CalculateNextRotation(slowdown, out nextRotation);
    }

    void OnMouseUpAsButton()
    {
        var person = GetPersonInformation();
        if(Player.PeopleKnown.ContainsKey(person.Name))
        {
            if(runningActivity != null && !Player.PeopleKnown[person.Name].SeenActivities.Contains(runningActivity.activity))
            {
                Player.PeopleKnown[person.Name].SeenActivities.Add(runningActivity.activity);
            }
        }
        else
        {
            Player.PeopleKnown.Add(this.Name, person);
            if(runningActivity != null)
            {
                person.SeenActivities.Add(runningActivity.activity);
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

    public void RunActivity(Activity activity)
    {
        runningActivity = new RunActivity(activity);
        BeginAction(runningActivity.GetCurrentAction());
    }

    public void BeginAction(ActivityAction action)
    {
        if(action is null)
        {
            return;
        }
        if(action is ActivityWait)
        {
            waitUntil = new ClockTime(ClockBehavior.MainClockBehavior.Time);
            waitUntil.AddMinutes(((ActivityWait)action).Minutes);
            ClockBehavior.NeedsClockUpdate.Add(this);
        }
        else if (action is ActivityRepeat)
        {
            runningActivity.ResetActivity();
            BeginAction(runningActivity.GetCurrentAction());
        }
        else if (action is ActivityWalk)
        {
            var walk = (ActivityWalk)action;
            GetComponent<AIDestinationSetter>().target = walk.Destination.GetComponent<Transform>();
        }
        else if (action is ActivityWaitUntilTime)
        {
            waitUntil = new ClockTime(((ActivityWaitUntilTime)action).Time);
            waitUntil.Day = ClockBehavior.MainClockBehavior.Time.Day;
            ClockBehavior.NeedsClockUpdate.Add(this);
        }
        else if (action is ActivitySpeak)
        {
            createMessage(((ActivitySpeak)action).text);
            runningActivity.CompleteAction();
            BeginAction(runningActivity.GetCurrentAction());
        }
    }


    public override void OnTargetReached()
    {
        /*runningActivity.DestinationReached();
        if(runningActivity.GetDestination() != null)
        {
            GetComponent<AIDestinationSetter>().target = runningActivity.GetDestination().GetComponent<Transform>();
        }*/
        if(runningActivity.GetCurrentAction() is ActivityWalk)
        {
            runningActivity.CompleteAction();
            BeginAction(runningActivity.GetCurrentAction());
        }
        base.OnTargetReached();
    }

    public void UpdateClock(ClockTime time)
    {
        if(runningActivity.GetCurrentAction() is ActivityWait)
        {
            if(waitUntil.Day < time.Day ||
                waitUntil.Day == time.Day && waitUntil.Hour < time.Hour ||
                waitUntil.Day == time.Day && waitUntil.Hour == time.Hour && waitUntil.Minute <= time.Minute)
            {
                runningActivity.CompleteAction();
                ClockBehavior.NeedsClockUpdate.Remove(this);
                BeginAction(runningActivity.GetCurrentAction());
            }
        }
        else if (runningActivity.GetCurrentAction() is ActivityWaitUntilTime)
        {
            if(waitUntil.Day < time.Day ||
                waitUntil.Day == time.Day && waitUntil.Hour < time.Hour ||
                waitUntil.Day == time.Day && waitUntil.Hour == time.Hour && waitUntil.Minute <= time.Minute)
            {
                runningActivity.CompleteAction();
                ClockBehavior.NeedsClockUpdate.Remove(this);
                BeginAction(runningActivity.GetCurrentAction());
            }
        }
    }

    void createMessage(string text)
    {
        speechObject.SetActive(true);

        speechObject.GetComponentInChildren<TextMeshPro>().text = text;
        messageTimeRemaining = messageDuration;
        isMessage = true;
    }



}