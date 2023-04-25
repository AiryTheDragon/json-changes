using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Assets.Scenes.ActualGame;

public class EndgamePanel0Script : MonoBehaviour
{
    public GameObject NextPanel;
    public GameObject Button;
    public EndGameMusicController endGameMusicController;
    public double timer = 0.0;
    public int stage = 0;
    private int MaxScore = 78;

    public GameObject Airy;
    public GameObject Autumn;
    public GameObject Bethany;
    public GameObject Daniel;
    public GameObject DeafJacob;
    public GameObject Don;
    public GameObject George;
    public GameObject HoboJoe;
    public GameObject Isaac;
    public GameObject Jade;
    public GameObject Jon;
    public GameObject Karen;
    public GameObject Liberty;
    public GameObject Mike;
    public GameObject Max;
    public GameObject Onna;
    public GameObject Phil;
    public GameObject Sarah;
    public GameObject SassySam;
    public GameObject DaDarkWizard;

    public GameObject Bob;
    public GameObject Dee;
    public GameObject Dum;
    public GameObject Frank;
    public GameObject Jessie;
    public GameObject Karon;
    public GameObject Pear;
    public GameObject Slinky;
    public GameObject Slowpoke;
    public GameObject Tiny;
    public GameObject Bob1;
    public GameObject Dee1;
    public GameObject Dum1;
    public GameObject Frank1;
    public GameObject Jessie1;
    public GameObject Karon1;
    public GameObject Pear1;
    public GameObject Slinky1;
    public GameObject Slowpoke1;
    public GameObject Tiny1;


    // Start is called before the first frame update
    void Start()
    {
        endGameMusicController.currentAudio = EndGameMusicListEnum.HeyYouMale;
        endGameMusicController.Play();
        if (Player.PeopleMax.Contains("Mike"))
        {
            Mike.SetActive(true);
        }
        if (Player.PeopleMax.Contains("Daniel"))
        {
            Daniel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (stage==0 & timer>2.0)
        {
            stage = 1;
            timer = 0.0;
            endGameMusicController.currentAudio = EndGameMusicListEnum.HeyYouFemale;
            endGameMusicController.Play();
            if (Player.PeopleMax.Contains("Onna"))
            {
                Onna.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Donald"))
            {
                Don.SetActive(true);
            }

        }
        if (stage == 1 & timer > 1.5)
        {
            stage = 2;
            timer = 0.0;
            endGameMusicController.currentAudio = EndGameMusicListEnum.HaltMale;
            endGameMusicController.Play();
            if (Player.PeopleMax.Contains("Autumn"))
            {
                Autumn.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Bethany"))
            {
                Bethany.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Isaac"))
            {
                Isaac.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Jade"))
            {
                Jade.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Jonathan"))
            {
                Jon.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Liberty"))
            {
                Liberty.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Sassy Sam"))
            {
                SassySam.SetActive(true);
            }
        }
        if (stage == 2 & timer > 1.5)
        {
            stage = 3;
            timer = 0.0;
            endGameMusicController.currentAudio = EndGameMusicListEnum.HaltFemale;
            endGameMusicController.Play();
            if (Player.PeopleMax.Contains("Airy"))
            {
                Airy.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Deaf Jacob"))
            {
                DeafJacob.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Manager George"))
            {
                George.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Joe (Unknown)"))
            {
                HoboJoe.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Karen"))
            {
                Karen.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Maxxy JJ"))
            {
                Max.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Helpful Phil"))
            {
                Phil.SetActive(true);
            }
            if (Player.PeopleMax.Contains("Sarah"))
            {
                Sarah.SetActive(true);
            }
        }
        if (stage == 3 & timer > 2.0)
        {
            endGameMusicController.Stop();
            endGameMusicController.GetComponent<AudioSource>().loop = true;
            Button.SetActive(true);
            if (Player.PeopleMax.Contains("Bob"))
            {
                Bob.SetActive(true);
                Bob1.SetActive(false);
            }
            if (Player.PeopleMax.Contains("Dee"))
            {
                Dee.SetActive(true);
                Dee1.SetActive(false);
            }
            if (Player.PeopleMax.Contains("Dum"))
            {
                Dum.SetActive(true);
                Dum1.SetActive(false);
            }
            if (Player.PeopleMax.Contains("Frank"))
            {
                Frank.SetActive(true);
                Frank1.SetActive(false);
            }
            if (Player.PeopleMax.Contains("Jessie"))
            {
                Jessie.SetActive(true);
                Jessie1.SetActive(false);
            }
            if (Player.PeopleMax.Contains("Karon"))
            {
                Karon.SetActive(true);
                Karon1.SetActive(false);
            }
            if (Player.PeopleMax.Contains("Pear"))
            {
                Pear.SetActive(true);
                Pear1.SetActive(false);
            }
            if (Player.PeopleMax.Contains("Slinky"))
            {
                Slinky.SetActive(true);
                Slinky1.SetActive(false);
            }
            if (Player.PeopleMax.Contains("Slowpoke"))
            {
                Slowpoke.SetActive(true);
                Slowpoke1.SetActive(false);
            }
            if (Player.PeopleMax.Contains("Tiny"))
            {
                Tiny.SetActive(true);
                Tiny1.SetActive(false);
            }
            if (Player.Score>=MaxScore)
            {
                DaDarkWizard.SetActive(true);
            }

        }



        //SceneManager.LoadScene("StartScene");
    }

    public void NextPanelClick()
    {
        NextPanel.SetActive(true);
        gameObject.SetActive(false);

    }
}
