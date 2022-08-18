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

    [SerializeField] private AudioClip _ow = null;
    [SerializeField] private AudioClip _tada = null;
    [SerializeField] private AudioClip _ugh = null;
    [SerializeField] private AudioClip _sigh = null;
    private int ughCount = 0;
    [SerializeField] private AudioClip _locked = null;
    [SerializeField] private AudioClip _brush = null;
    [SerializeField] private AudioClip _wallbump = null;

#pragma warning disable 414
    [SerializeField] private AudioClip _footsteps = null;
    #pragma warning restore 414
    [SerializeField] private AudioClip _pen = null;
    [SerializeField] private AudioClip _paper = null;

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

    public Dictionary<string, Person> PeopleKnown = new Dictionary<string, Person>();

    public CharacterBehavior CharacterBehavior;

    public NPCInfoBehavior NPCInfoUI;

    public AchievementList achievementList;

    public static string Name = "DaDarkWizard";

    public bool beingEscorted;

    public static int Score = 0;

    public Log log;

    //Input keys
    private MenuBehavior menu;
    private bool iDown;
    private bool escDown;

    public bool inputEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        speechObject.SetActive(false);
        NPCInfoUI = Resources.FindObjectsOfTypeAll<NPCInfoBehavior>().First();
        menu = Resources.FindObjectsOfTypeAll<MenuBehavior>().First();
        _source = GetComponent<AudioSource>();
        if (_source == null)
        {
            Debug.Log("Audio Source is NULL");
        }
        else
        {
            _source.clip = _ow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inputEnabled)
        {
            if (Input.GetAxis("Fire1")>0)
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
        else{
            CurrentSpeed = 0;
        }

        float xChange = Input.GetAxis("Horizontal") * CurrentSpeed * Time.deltaTime;
        float yChange = Input.GetAxis("Vertical") * CurrentSpeed * Time.deltaTime;

        CharacterBehavior.UpdateHead(Input.GetAxis("Horizontal") * CurrentSpeed, Input.GetAxis("Vertical") * CurrentSpeed);

        if(!beingEscorted)
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

        if(inputEnabled)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                if(!menu.Active())
                {
                    menu.Open(KeyCode.Escape);
                }
            }

            if(Input.GetKey(KeyCode.I))
            {
                if(!menu.Active())
                {
                    menu.Open(KeyCode.I);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if ( collision.gameObject.tag == "Lava")
        {
            _source.clip = _ow;
            CreateMessage("Ow.");
            _source.Play();

            AchievementItem achItem = achievementList.getItem(Achievement.Burned);
            if (!achItem.isDone)
            {
                achievementList.makeAchievement(achItem);
            }

        }

        if ( collision.gameObject.tag == "Shrub")
        {
            _source.clip = _brush;
            _source.Play();
        }

        if (collision.gameObject.tag == "PeeShrub")
        {
            CreateMessage("Aaaaaaaaaah.");
            _source.clip = _brush;
            _source.Play();
            AchievementItem achItem = achievementList.getItem(Achievement.Aaaaaaaaaah);
            if (!achItem.isDone)
            {
                achievementList.makeAchievement(achItem);
            }
        }

        if (collision.gameObject.tag == "Door")
        {
            CreateMessage("I don't have the key.");
            _source.clip = _locked;
            _source.Play();
        }

        if (collision.gameObject.tag == "Rock" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Garbage")
        {
            _source.clip = _wallbump;
            _source.Play();
        }

        if (collision.gameObject.tag == "NPC" || collision.gameObject.tag == "GuardNPC")
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

                // Check whether colliding with this npc gave us an achievement.
                achievementList.CheckMoralAchievements(npc);
            }   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            _source.clip = _tada;
            CreateMessage("Awesome!");
            _source.Play();
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Paper")
        {
            CreateMessage("More paper for more letters!");

            _source.clip = _paper;
            _source.Play();
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);

            if (invScript.Paper>=10 && invScript.Pens>=10)
            {
                AchievementItem achItem = achievementList.getItem(Achievement.Hoarder);
                if (!achItem.isDone)
                {
                    achievementList.makeAchievement(achItem);
                }
            }
        }
        else if(collision.gameObject.tag == "Pen")
        {
            CreateMessage("Another pen for another letter!");
            _source.clip = _pen;
            _source.Play();
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);

            if (invScript.Paper >= 10 && invScript.Pens >= 10)
            {
                AchievementItem achItem = achievementList.getItem(Achievement.Hoarder);
                if (!achItem.isDone)
                {
                    achievementList.makeAchievement(achItem);
                }
            }
        }
        else if (collision.gameObject.tag == "FreedomBook")
        {
            CreateMessage("Freedom!");
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Cupcake")
        {
            CreateMessage("Yum!");
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Writing Desk")
        {
            if(invScript.Paper >= 1 && invScript.Pens >= 1)
            {
                collision.gameObject.GetComponentInParent<WritingDeskBehavior>().OpenLetterCreator();
            }
            else if(invScript.Pens < 1)
            {
                CreateMessage("I don't have a pen to write anything.");
                _source.clip = _ugh;
                _source.Play();
            }
            else{
                CreateMessage("I don't have any paper make letters.");
                _source.clip = _ugh;
                _source.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TV")
        {
            if (ughCount % 10 == 0)
            {
                CreateMessage("Stay Home.\nStay Safe.\nUgh.");
                _source.clip = _ugh;
            }
            else
            { 
                _source.clip = _sigh;
            }
            ughCount++;
            _source.Play();      
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
        if(Suspicion >= MaxSuspicion)
        {
            Revolt();
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
        foreach(var person in PeopleKnown.Values)
        {
            int change = person.Value - ((Math.Max(0, Math.Min(person.Value, 5 - person.ManipulationLevel))));
            score += change;
        }

        Score = score;
    }

}
