using UnityEngine;
using System.Collections.Generic;
using System;

public class GneralScript : MonoBehaviour
{

    [Header("Players")]
    public List<ControllerPlayer> players;

    [Header("Prefabs")]
    public GameObject playerpawnprefab;
    public GameObject playercontrollerprefab;
    public GameObject meteorprefab;

    //[Header("GameData")]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        players = new List<ControllerPlayer>();
        SpawnPlayerController();
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
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
}
