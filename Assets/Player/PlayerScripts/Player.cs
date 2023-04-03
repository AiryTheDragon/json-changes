using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float slowSpeed = 1f;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 4f;

    /// <summary>
    /// The current speed of the player.
    /// </summary>
    public float CurrentSpeed { get; set; }

    [SerializeField] float messageDuration = 5f;

    private int ughCount = 0;

    public PlayerSounds _playerSounds;

    /// <summary>
    /// The source player noises come from.
    /// </summary>
    private AudioSource _source = null;

    private float messageTimeRemaining;
    private bool isMessage = false;

    public GameObject speechObject;
    public InvScript invScript;

    public double Suspicion { get; private set; }

    public double MaxSuspicion = 100;
    public double warningSuspicion;

    public Dictionary<string, Person> PeopleKnown = new ();

    public CharacterBehavior CharacterBehavior;

    public NPCInfoBehavior NPCInfoUI;

    public AchievementList achievementList;

    public static string Name = "DaDarkWizard";

    // flags used for credits scene
    public static List<string> PeopleMax = new();

    public bool beingEscorted;

    public static int Score = 0;

    public Log log;

    public static int SaveFile;

    //Input keys
    private MenuBehavior menu;
    private bool iDown;
    private bool escDown;

    public bool inputEnabled = true;

    // used for confirmation notice
    public GameObject confirmObject;
    public GameObject yesButton;

    public MusicController musicController;

    public LetterCreator letterCreator;

    // Start is called before the first frame update
    void Start()
    {
        speechObject.SetActive(false);
        NPCInfoUI = Resources.FindObjectsOfTypeAll<NPCInfoBehavior>().First();
        menu = Resources.FindObjectsOfTypeAll<MenuBehavior>().First();
        _source = GetComponent<AudioSource>();
        _playerSounds = GetComponent<PlayerSounds>();
        if (_source == null)
        {
            Debug.Log("Audio Source is NULL");
        }
        else
        {
            _source.clip = _playerSounds.Ow;
        }

        if (musicController is null)
        {
            musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();

        }
        musicController.Play();
        warningSuspicion = .75 * MaxSuspicion;

        if (TutorialBedBehavior.tutorialComplete)
        {
            achievementList.TryGetAchievement(Achievement.Awoken);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
        {
            if (Input.GetAxis("Fire1") > 0)
            {
                CurrentSpeed = runSpeed;
            }
            else if (Input.GetAxis("Fire2") > 0)
            {
                CurrentSpeed = slowSpeed;
            }
            else
            {
                CurrentSpeed = walkSpeed;
            }
        }
        else
        {
            CurrentSpeed = 0;
        }

        float xChange = Input.GetAxis("Horizontal") * CurrentSpeed * Time.deltaTime;
        float yChange = Input.GetAxis("Vertical") * CurrentSpeed * Time.deltaTime;

        CharacterBehavior.UpdateHead(Input.GetAxis("Horizontal") * CurrentSpeed, Input.GetAxis("Vertical") * CurrentSpeed);

        if (!beingEscorted)
        {
            transform.Translate(xChange, 0, 0);
            transform.Translate(0, yChange, 0);
        }

        if (isMessage)
        {
            messageTimeRemaining -= Time.deltaTime;

            if (messageTimeRemaining < 0)
            {
                speechObject.SetActive(false);
                isMessage = false;
            }
        }

        if (inputEnabled)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (!menu.Active())
                {
                    menu.Open(KeyCode.Escape);
                }
            }

            if (Input.GetKey(KeyCode.I))
            {
                if (!menu.Active())
                {
                    menu.Open(KeyCode.I);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Lava"))
        {
            _source.clip = _playerSounds.Ow;
            CreateMessage("Ow.");
            _source.Play();

            achievementList.TryGetAchievement(Achievement.Burned);

        }

        if (collision.gameObject.CompareTag("Shrub"))
        {
            _source.clip = _playerSounds.Brush;
            _source.Play();
        }

        if (collision.gameObject.CompareTag("PeeShrub"))
        {
            CreateMessage("Aaaaaaaaaah.");
            _source.clip = _playerSounds.Brush;
            _source.Play();
            achievementList.TryGetAchievement(Achievement.Aaaaaaaaaah);
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            CreateMessage("I don't have the key.");
            _source.clip = _playerSounds.Locked;
            _source.Play();
        }

        if (collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Garbage"))
        {
            _source.clip = _playerSounds.Wallbump;
            _source.Play();
        }

        if (collision.gameObject.CompareTag("NPC") || collision.gameObject.CompareTag("GuardNPC"))
        {
            var npc = collision.gameObject.GetComponent<NPCBehavior>();
            if (invScript.Letters.Where(x => x.Recieving.Name == npc.Name).Any())
            {
                Letter letter = invScript.Letters.Where(x => x.Recieving.Name == npc.Name).First();
                invScript.RemoveLetter(letter);
                npc.ManipulationLevel += letter.ManipulationLevelIncrease;
                PeopleKnown[npc.Name].ManipulationLevel = npc.ManipulationLevel;

                Debug.Log("Delivered a letter to " + npc.Name + " affecting morale by " + letter.ManipulationLevelIncrease);
                Debug.Log(npc.Name + "'s level is now " + npc.ManipulationLevel);
                log.AddItem("Notice", "Delivered a letter to " + npc.Name + " affecting morale by " + letter.ManipulationLevelIncrease);
                log.AddItem(npc.Name, "Morale is now " + npc.ManipulationLevel + ".");

                confirmObject.GetComponent<ConfirmMenu>().UpdateText("Letter delivered to " + npc.Name + ".");
                //_source.clip = _inBed;
                //_source.Play();
                confirmObject.SetActive(true);
                confirmObject.GetComponent<ConfirmMenu>().UpdateNoText("Ok!");
                yesButton.SetActive(false); // additional option not needed
                
                // Check whether colliding with this npc gave us an achievement.
                achievementList.CheckMoralAchievements(npc);
                if (letter.Type == LetterType.Blackmail || letter.Type == LetterType.Gaming)
                {
                    achievementList.CheckIllegalAchievements(npc);
                }
                if (letter.Type == LetterType.Gaming)
                {
                    achievementList.CheckLetterTypeAchievements(letter);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            CreateMessage("Awesome!");
            PlayAudioClip(_playerSounds.Tada);
            if (!invScript.haveItem(collision.gameObject.GetComponent<KeyScript>().objectName))
            {
                invScript.addItem(collision.gameObject);
            }
            else
            {
                Debug.LogWarning(collision.gameObject.GetComponent<KeyScript>().objectName + " was already in the inventory.");
            }

            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Paper"))
        {
            CreateMessage("More paper for more letters!");

            PlayAudioClip(_playerSounds.Paper);
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);

            if (invScript.Paper >= 10 && invScript.Pens >= 10)
            {
                AchievementItem achItem = AchievementList.GetItem(Achievement.Hoarder);
                if (!achItem.isDone)
                {
                    AchievementList.MakeAchievement(achItem, achievementList);
                }
            }
        }
        else if (collision.gameObject.CompareTag("Pen"))
        {
            CreateMessage("Another pen for another letter!");
            PlayAudioClip(_playerSounds.Pen);
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);

            if (invScript.Paper >= 10 && invScript.Pens >= 10)
            {
                AchievementItem achItem = AchievementList.GetItem(Achievement.Hoarder);
                if (!achItem.isDone)
                {
                    AchievementList.MakeAchievement(achItem, achievementList);
                }
            }
        }
        else if (collision.gameObject.CompareTag("FreedomBook"))
        {
            CreateMessage("Freedom!");
            invScript.addItem(collision.gameObject);
            PlayAudioClip(_playerSounds.Freedom);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Cupcake"))
        {
            CreateMessage("Yum!");
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);
            log.AddItem("Note", "While you enjoy the small cupcake, you feel it is a lie.  Could a bigger one exist?");
            NPCInfoUI.OpenMessage("You eat the cupcake.  Yum.");

        }
        else if (collision.gameObject.CompareTag("GiantCupcake"))
        {
            CreateMessage("Yum!");
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);
            achievementList.TryGetAchievement(Achievement.TheCakeIsNotALie);
        }
        else if (collision.gameObject.CompareTag("Writing Desk"))
        {
            if (invScript.Paper >= 1 && invScript.Pens >= 1)
            {
                collision.gameObject.GetComponentInParent<WritingDeskBehavior>().OpenLetterCreator();
            }
            else if (invScript.Pens < 1)
            {
                CreateMessage("I don't have a pen to write anything.");
                PlayAudioClip(_playerSounds.Ugh);
            }
            else
            {
                CreateMessage("I don't have any paper make letters.");
                PlayAudioClip(_playerSounds.Ugh);
            }
        }
        else if(collision.gameObject.CompareTag("SecurityCamera"))
        {
            collision.gameObject.GetComponent<SecurityCameraBehavior>().seesPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TV"))
        {
            if (ughCount % 10 == 0)
            {
                CreateMessage("Stay Home.\nStay Safe.\nUgh.");
                _source.clip = _playerSounds.Ugh;
            }
            else
            {
                _source.clip = _playerSounds.Sigh;
            }
            ughCount++;
            _source.Play();
        }
        else if (collision.gameObject.CompareTag("SecurityCamera"))
        {
            collision.gameObject.GetComponent<SecurityCameraBehavior>().seesPlayer = false;
        }
        else if (collision.gameObject.CompareTag("Writing Desk"))
        {
            letterCreator.LeaveCreator();
        }
    }
    /// <summary>
    /// Activate the speech bubble above the player's head and<br/>
    /// set the text inside it.
    /// </summary>
    private void CreateMessage(string text)
    {
        speechObject.SetActive(true);

        speechObject.GetComponentInChildren<TextMeshPro>().text = text;
        messageTimeRemaining = messageDuration;
        isMessage = true;
    }

    /// <summary>
    /// Called when we need to add suspicion to the player.<br/>
    /// Handles player-based multipliers.
    /// </summary>
    public void AddSuspicion(double suspicion)
    {
        // If we multiply by the move speed and divide by two, suspicion will range from 1 - 4
        this.Suspicion += suspicion / 2f * CurrentSpeed;

        if (Suspicion >= MaxSuspicion)
        {
            Revolt();
        }

        if (Suspicion >= warningSuspicion)
        {
            musicController.IsHighSus();
        }
        else
        {
            musicController.IsNotHighSus();
        }

    }

    /// <summary>
    /// Called when we need to lower suspicion to the player.<br/>
    /// Called when a player sleeps
    /// Reduces suspicion by 1% per hour sleeping
    /// </summary>
    public void ReduceSuspicion( int min )
    {
        this.Suspicion -= min * MaxSuspicion / 6000;
        if (this.Suspicion<0d)
        { 
            this.Suspicion = 0d;
        }
    }

    /// <summary>
    /// Called when we want to end the game.
    /// </summary>
    public void Revolt()
    {
        CalculateScore();
        SceneManager.LoadScene("CreditsScene");
    }

    private void CalculateScore()
    {
        int score = 0;
        PeopleMax = new List<string>();
        foreach (var person in PeopleKnown.Values)
        {
            int change = person.Value - ((Math.Max(0, Math.Min(person.Value, 5 - person.ManipulationLevel))));
            score += change;
            Debug.Log("NPC " + person.Name + " of importance " + person.Value + " has morale " + person.ManipulationLevel + " and is scored " + change + ".");
            if (person.ManipulationLevel>=5)
            {
                PeopleMax.Add(person.Name);
                Debug.Log("NPC " + person.Name + " added to Max list.");
            }
        }

        Score = score;
    }

    /// <summary>
    /// Helper function for playing an audio clip.
    /// </summary>
    public void PlayAudioClip(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }

}
