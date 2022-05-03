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
    [SerializeField] private AudioClip _locked = null;

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

    public static string Name = "DaDarkWizard";

    public bool beingEscorted;

    public static int Score = 0;

    //Input keys
    private MenuBehavior menu;
    private bool iDown;
    private bool escDown;



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

        if(Input.GetKey(KeyCode.Escape) && !escDown)
        {
            escDown = true;
            if(!menu.Active())
            {
                menu.Open(KeyCode.Escape);
            }
        }
        else if(escDown)
        {
            escDown = false;
        }

        if(Input.GetKey(KeyCode.I) && !iDown)
        {
            iDown = true;
            if(!menu.Active())
            {
                menu.Open(KeyCode.I);
            }
        }
        else if(iDown)
        {
            iDown = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Rock" || collision.gameObject.tag == "Shrub" 
            || collision.gameObject.tag == "Lava")
        {
            _source.clip = _ow;
            createMessage("Ow.");
            _source.Play();
        }

        if (collision.gameObject.tag == "Door")
        {
            createMessage("I don't have the key.");
            _source.clip = _locked;
            _source.Play();
        }

        if (collision.gameObject.tag == "NPC")
        {
            var npc = collision.gameObject.GetComponent<NPCBehavior>();
            if(invScript.Letters.Where(x => x.Recieving.Name == npc.Name).Any())
            {
                Letter letter = invScript.Letters.Where(x => x.Recieving.Name == npc.Name).First();
                invScript.RemoveLetter(letter);
                npc.ManipulationLevel += letter.ManipulationLevelIncrease;
                PeopleKnown[npc.Name].ManipulationLevel = npc.ManipulationLevel;
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
        }
        else if(collision.gameObject.tag == "Pen")
        {
            createMessage("Another pen for another letter!");
            _source.clip = _pen;
            _source.Play();
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);
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
            createMessage("Stay Home.\nStay Safe.\nUgh.");
            _source.clip = _ugh;
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
