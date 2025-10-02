using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;

public class GneralScript : MonoBehaviour
{
    public static GneralScript instance;

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
        }
    }
    void Start()
    {
        players = new List<ControllerPlayer>();
        SpawnPlayerController();
        SpawnPlayer();
        timecount = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        timecount += Time.deltaTime;

        if(timecount > 2 )
        {
            timecount = 0;
            SpawnObstacle();
        }
        scoretext.text = "Score: " + score;
    }

    public void SpawnPlayer()
    {
        if (players[0].pawnobject != null)
        {
            Destroy(players[0].pawnobject.gameObject);
        }
        else
        {
            GameObject _pawn = Instantiate(playerpawnprefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            if (_pawn != null)
            {
                Pawn newpawn = _pawn.GetComponent<Pawn>();
                if (newpawn != null)
                {
                    players[0].pawnobject = newpawn;
                }
            }
        }
    }

    public void SpawnPlayerController()
    {
        GameObject _controller = Instantiate(playercontrollerprefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        ControllerPlayer newcontroller = _controller.GetComponent<ControllerPlayer>();
        //if (players.Count !>= 1)
        {
            players.Add(newcontroller);
        }
        //else
        {
            //players[0] = newcontroller;
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
                GameObject obstacle = Instantiate(meteorprefab, pos + new Vector3(-6, 6, 0), Quaternion.identity) as GameObject; //spawns top left from character
                obstacle.GetComponent<MeteorScript>().istanctiate(pos);
            }
            else if (r1 == 2)
            {
                GameObject obstacle = Instantiate(meteorprefab, pos + new Vector3(-6, -6, 0), Quaternion.identity) as GameObject; //spawns bottom left from character
                obstacle.GetComponent<MeteorScript>().istanctiate(pos);
            }
            else if (r1 == 3)
            {
                GameObject obstacle = Instantiate(meteorprefab, pos + new Vector3(6, 6, 0), Quaternion.identity) as GameObject; //spawns top right from character
                obstacle.GetComponent<MeteorScript>().istanctiate(pos);
            }
            else if (r1 == 4)
            {
                GameObject obstacle = Instantiate(meteorprefab, pos + new Vector3(6, -6, 0), Quaternion.identity) as GameObject; //spawns bottom right from character
                obstacle.GetComponent<MeteorScript>().istanctiate(pos);
            }
        }
    }
}
