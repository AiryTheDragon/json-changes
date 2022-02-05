using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] float slowSpeed = 1f;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float currentSpeed = 2f;
    [SerializeField] float messageDuration = 5f;

    [SerializeField] private AudioClip _ow = null;
    [SerializeField] private AudioClip _tada = null;
    private AudioSource _source = null;


    private float messageTimeRemaining;
    private bool isMessage = false;

    public GameObject speechObject;
    public InvScript invScript;

    public int Suspicion;

    public int MaxSuspicion = 100;

    

    // Start is called before the first frame update
    void Start()
    {
        speechObject.SetActive(false);

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

        transform.Translate(xChange, 0, 0);
        transform.Translate(0, yChange, 0);

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

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            _source.clip = _ow;
            createMessage("Ow.");
            _source.Play();
        }

        if (collision.gameObject.tag == "Door")
        {
            createMessage("I don't have the key.");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Debug.Log("found a key");
            _source.clip = _tada;
            createMessage("Awesome!");
            _source.Play();
            invScript.addItem(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TV")
        {
            createMessage("Stay Home.\nStay Safe.\nUgh.");
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


}
