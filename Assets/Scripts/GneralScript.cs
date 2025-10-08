using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;

public class GneralScript : MonoBehaviour
{
    public static GneralScript instance;

    [Header("GameLayers")]
    public GameObject gamelayer;
    public GameObject menulayer;
    public GameObject creditslayer;
    [SerializeField] private Canvas gamecanvas;

    [Header("Players")]
    public List<ControllerPlayer> players;

    [Header("Prefabs")]
    public GameObject playerpawnprefab;
    public GameObject playercontrollerprefab;
    public GameObject meteorprefab;

    private float timecount;
    public int score;
    public TextMeshProUGUI scoretext;

    //[Header("GameData")]
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Awake()
    {
        //does a gamemanager already exidst??? check
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            gamelayer.SetActive(false);
            creditslayer.SetActive(false);
            menulayer.SetActive(true);
        }
    }
    void Start()
    {
        players = new List<ControllerPlayer>(); //needs to stay, otherwise previously created game objects do not get deleted (since they are no longer associated with this new list)
        //could fix by createing a function that removes all existing unwated game objects, but I don't feel like doing that -> Edit: I....uhhh. I went ahead and did that...
        Reset();
    }

    public void Reset()
    {
        foreach(Transform gameobj in gamelayer.transform) //destoryed gameobjects in the gamelayer that aren't the canvas elements (UI)
        {
            if(gameobj.gameObject != gamecanvas.gameObject)
            {
                Destroy(gameobj.gameObject);
            }
        }
        
        SpawnPlayerController();
        SpawnPlayer();
        timecount = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //gamelayer.SetActive(!gamelayer.activeSelf);
            //menulayer.SetActive(!menulayer.activeSelf);
            gamelayer.SetActive(false);
            creditslayer.SetActive(false);
            menulayer.SetActive(true);
        }
        if (!menulayer.activeSelf && !creditslayer.activeSelf && gamelayer.activeSelf) //game only runs if in "game mode"
        {

            timecount += Time.deltaTime;

            if (timecount > 2)
            {
                timecount = 0;
                SpawnObstacle();
            }
            scoretext.text = "Score: " + score;
        }
    }

    public void SpawnPlayer()
    {
        if (players[0].pawnobject != null)
        {
            //print("meep2");
            Destroy(players[0].pawnobject.gameObject);
        }
        
        GameObject _pawn = Instantiate(playerpawnprefab, new Vector3(0,0,0), Quaternion.identity, gamelayer.transform) as GameObject;
        if (_pawn != null)
        {
            Pawn newpawn = _pawn.GetComponent<Pawn>();
            if (newpawn != null)
            {
                players[0].pawnobject = newpawn;
                players[0].gameObject.GetComponent<ControllerPlayer>().IstantiateHealthBar();
            }
        }
        
    }

    public void SpawnPlayerController()
    {
        if (players.Count > 0) //if list is not empty
        {
            if (players[0].gameObject != null) //if there is already a controller
            {
                //print("meep");
                Destroy(players[0].gameObject);
                GameObject _controller = Instantiate(playercontrollerprefab, new Vector3(0, 0, 0), Quaternion.identity, gamelayer.transform) as GameObject; //instantiated controller and as a child of gamelayer
                ControllerPlayer newcontroller = _controller.GetComponent<ControllerPlayer>();
                players[0] = newcontroller;
            }
        }
        else
        {
            GameObject _controller = Instantiate(playercontrollerprefab, new Vector3(0, 0, 0), Quaternion.identity, gamelayer.transform) as GameObject; //instantiated controller and as a child of gamelayer
            ControllerPlayer newcontroller = _controller.GetComponent<ControllerPlayer>();
            players.Add(newcontroller);
        }

    }

    void SpawnObstacle()
    {
        if (players[0].pawnobject != null) //if pawn object no longer exists
        {
            Vector3 pos = players[0].pawnobject.gameObject.transform.position;
            System.Random random = new System.Random();
            int r1 = random.Next(1, 5); //1-4
            if (r1 == 1)
            {
                GameObject obstacle = Instantiate(meteorprefab, pos + new Vector3(-6, 6, 0), Quaternion.identity, gamelayer.transform) as GameObject; //spawns top left from character
                obstacle.GetComponent<MeteorScript>().istanctiate(pos);
            }
            else if (r1 == 2)
            {
                GameObject obstacle = Instantiate(meteorprefab, pos + new Vector3(-6, -6, 0), Quaternion.identity, gamelayer.transform) as GameObject; //spawns bottom left from character
                obstacle.GetComponent<MeteorScript>().istanctiate(pos);
            }
            else if (r1 == 3)
            {
                GameObject obstacle = Instantiate(meteorprefab, pos + new Vector3(6, 6, 0), Quaternion.identity, gamelayer.transform) as GameObject; //spawns top right from character
                obstacle.GetComponent<MeteorScript>().istanctiate(pos);
            }
            else if (r1 == 4)
            {
                GameObject obstacle = Instantiate(meteorprefab, pos + new Vector3(6, -6, 0), Quaternion.identity, gamelayer.transform) as GameObject; //spawns bottom right from character
                obstacle.GetComponent<MeteorScript>().istanctiate(pos);
            }
        }
    }
}
