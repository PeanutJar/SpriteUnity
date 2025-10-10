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
    public GameObject gameoverlayer;
    [SerializeField] private Canvas gamecanvas;
    //public GameObject heartsobj;
    //public List<Image> hearts;

    [Header("Players")]
    public List<ControllerPlayer> players;

    [Header("Prefabs")]
    public GameObject playerpawnprefab;
    public GameObject playercontrollerprefab;
    public GameObject meteorprefab;
    public GameObject ufoprefab;
    public Image heartimage;


    [Header("Misc")]
    private float timecount;
    public int score;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI losetext;
    public TextMeshProUGUI wintext;
    private float top;
    private float bottom;
    private float left;
    private float right;
    [SerializeField] private int isufochancepercentage;
    public int initialenemyspawnlimit;
    private int spawnedmainenemies;
    public List<GameObject> enemyspawnlist;
    public bool haswon;
    public float borderTop;
    public float borderLeft;
    public float borderBottom;
    public float borderRight;

    [Header("AudioClips")] //only putting this here for requiremetns sake, personally I just don't see the need for the information being here, when I can just have some audio object
    public AudioClip explosionsound;
    public AudioClip ufosound;
    public AudioClip spaceshipimpactsound;
    public AudioClip projectilesound;
    public AudioClip backgroundmusic;

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
            gameoverlayer.SetActive(false);
            losetext.gameObject.SetActive(false);
            wintext.gameObject.SetActive(false);
            GetComponent<AudioSource>().Stop();
            menulayer.SetActive(true);
        }
    }
    void Start()
    {
        players = new List<ControllerPlayer>();
        enemyspawnlist = new List<GameObject>();
        Reset();

        top = Camera.main.orthographicSize;
        bottom = -top;
        right = Camera.main.aspect * Camera.main.orthographicSize;
        left = -right;
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
        scoretext.text = "Score:";
        spawnedmainenemies = 0;
        haswon = false;
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
            gameoverlayer.SetActive(false);
            losetext.gameObject.SetActive(false);
            wintext.gameObject.SetActive(false);
            GetComponent<AudioSource>().Stop();
            menulayer.SetActive(true);
        }
        if (!menulayer.activeSelf && !creditslayer.activeSelf && !gameoverlayer.activeSelf && gamelayer.activeSelf) //game only runs if in "game mode"
        {

            if(spawnedmainenemies < initialenemyspawnlimit)
            {
                timecount += Time.deltaTime;

                if (timecount > 2)
                {
                    timecount = 0;
                    SpawnObstacle();
                }
            }
            if (haswon)
            {
                GameEnd(true);
            }

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
                players[0].gameObject.GetComponent<ControllerPlayer>().IstantiatePawnPlayerConnection();
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
            Vector3 pos;
            System.Random random = new System.Random();
            int r1 = random.Next(1, 5); //1-4
            int r2 = random.Next(1, 101); //1-100
            GameObject obstacle;
            GameObject tester;
            if(r2 > isufochancepercentage) //if random num is greater than desired ufo chance percentage (ex: 70% chance of being ufo, so 71 or more num in order to be meteor)
            {
                tester = meteorprefab;
                int r3 = random.Next((int)bottom, (int)top); 
                int r4 = random.Next((int)left, (int)right);
                pos = new Vector3(r3, r4, 0);
            }
            else
            {
                tester = ufoprefab;
                pos = players[0].pawnobject.gameObject.transform.position;
            }
            if (r1 == 1)
            {
                obstacle = Instantiate(tester, new Vector3(left, top, 0), Quaternion.identity, gamelayer.transform) as GameObject; //spawns top left from character
                obstacle.GetComponent<Obstacle>().setDirection(pos);
                enemyspawnlist.Add(obstacle);
            }
            else if (r1 == 2)
            {
                obstacle = Instantiate(tester, new Vector3(left, bottom, 0), Quaternion.identity, gamelayer.transform) as GameObject; //spawns bottom left from character
                obstacle.GetComponent<Obstacle>().setDirection(pos);
                enemyspawnlist.Add(obstacle);
            }
            else if (r1 == 3)
            {
                obstacle = Instantiate(tester, new Vector3(right, top, 0), Quaternion.identity, gamelayer.transform) as GameObject; //spawns top right from character
                obstacle.GetComponent<Obstacle>().setDirection(pos);
                enemyspawnlist.Add(obstacle);
            }
            else if (r1 == 4)
            {
                obstacle = Instantiate(tester, new Vector3(right, bottom, 0), Quaternion.identity, gamelayer.transform) as GameObject; //spawns bottom right from character
                obstacle.GetComponent<Obstacle>().setDirection(pos);
                enemyspawnlist.Add(obstacle);
            }
            spawnedmainenemies += 1;
        }
    }

    public void GameEnd(bool iswin)
    {
        //if (players[0].gameObject.getComponent<ControllerPlayer>().getLives)
        gameoverlayer.SetActive(true);
        if (!iswin)
        {
            losetext.gameObject.SetActive(true);
        }
        else
        {
            wintext.gameObject.SetActive(true);
        }
        gamelayer.SetActive(false);
    }

    public bool GameEnd()
    {
        if (enemyspawnlist.Count == 1 && spawnedmainenemies >= initialenemyspawnlimit)
        {
            print("meep");
            if (enemyspawnlist[0].gameObject.GetComponent<Obstacle>().isfinalenemy) //if there is a single enemy that does not have the condition "isfinalenemy" set to true, the player cannot win yet
            {
                print("meep2");
                return true;
            }
        }
        return false;
    }
}
