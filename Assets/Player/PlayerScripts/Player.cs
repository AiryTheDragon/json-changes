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
    [SerializeField] float currentSpeed = 2f;
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

    public AudioSource _source = null;

    private float messageTimeRemaining;
    private bool isMessage = false;

    public GameObject speechObject;
    public InvScript invScript;

    public int Suspicion;

    public int MaxSuspicion = 100;

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
                currentSpeed = runSpeed;
            }
            else if (Input.GetAxis("Fire2") > 0)
            {
                currentSpeed = slowSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }
        }
        else{
            currentSpeed = 0;
        }

        float xChange = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        float yChange = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

        CharacterBehavior.UpdateHead(Input.GetAxis("Horizontal") * currentSpeed, Input.GetAxis("Vertical") * currentSpeed);

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
            createMessage("Ow.");
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
            createMessage("Aaaaaaaaaah.");
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
            createMessage("I don't have the key.");
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

                // check morale achievements
                if (npc.ManipulationLevel >= 5)
                {
                    
                    bool maxMike = false;
                    bool maxDaniel = false;
                    bool maxOnna = false;
                    bool maxDonald = false;
                    foreach (var person in PeopleKnown.Values)
                    {
                        if (person.Name.Equals("Mike") && person.ManipulationLevel >= 5)
                        {
                            maxMike = true;
                            Debug.Log("Mike is maxed");
                        }
                        if (person.Name.Equals("Daniel") && person.ManipulationLevel >= 5)
                        {
                            maxDaniel = true;
                            Debug.Log("Daniel is maxed");
                        }
                        if (person.Name.Equals("Onna") && person.ManipulationLevel >= 5)
                        {
                            maxOnna = true;
                            Debug.Log("Onna is maxed");
                        }
                        if (person.Name.Equals("Donald") && person.ManipulationLevel >= 5)
                        {
                            maxDonald = true;
                            Debug.Log("Donald is maxed");
                        }
                    }
                    if (maxMike && maxDaniel)
                    {
                        AchievementItem achItem = achievementList.getItem(Achievement.WinOverTheCreators);
                        if (!achItem.isDone)
                        {
                            achievementList.makeAchievement(achItem);
                        }
                    }
                    if (maxOnna && maxDonald)
                    {
                        AchievementItem achItem = achievementList.getItem(Achievement.ParentialApproval);
                        if (!achItem.isDone)
                        {
                            achievementList.makeAchievement(achItem);
                        }
                    }
                    if (npc.Name.Equals("Airy"))
                    {
                        AchievementItem achItem = achievementList.getItem(Achievement.TameTheDragon);
                        if (!achItem.isDone)
                        {
                            achievementList.makeAchievement(achItem);
                        }
                    }
                    if (npc.Name.Equals("Manager George"))
                    {
                        AchievementItem achItem = achievementList.getItem(Achievement.ManageTheManager);
                        if (!achItem.isDone)
                        {
                            achievementList.makeAchievement(achItem);
                        }
                    }
                }            
                if (npc.Name.Equals("Manager George"))
                {
                    AchievementItem achItem = achievementList.getItem(Achievement.Hypocrisy);
                    if (!achItem.isDone)
                    {
                        achievementList.makeAchievement(achItem);
                    }
                }
                if (npc.tag.Equals("GuardNPC"))
                {
                    AchievementItem achItem = achievementList.getItem(Achievement.CatchTheGuards);
                    if (!achItem.isDone)
                    {
                        achievementList.makeAchievement(achItem);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            _source.clip = _tada;
            createMessage("Awesome!");
            _source.Play();
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Paper")
        {
            createMessage("More paper for more letters!");

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
            createMessage("Another pen for another letter!");
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
            createMessage("Freedom!");
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Cupcake")
        {
            createMessage("Yum!");
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
                createMessage("I don't have a pen to write anything.");
                _source.clip = _ugh;
                _source.Play();
            }
            else{
                createMessage("I don't have any paper make letters.");
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
                createMessage("Stay Home.\nStay Safe.\nUgh.");
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

    void createMessage(string text)
    {
        speechObject.SetActive(true);

        speechObject.GetComponentInChildren<TextMeshPro>().text = text;
        messageTimeRemaining = messageDuration;
        isMessage = true;
    }

    public void AddSuspicion(int suspicion)
    {
        this.Suspicion += suspicion;
    }

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
            Debug.Log("Max value:" + person.Value + " change:" + change);
            score += change;
        }
        Debug.Log("Total score:" + score);

        Score = score;
    }
}
