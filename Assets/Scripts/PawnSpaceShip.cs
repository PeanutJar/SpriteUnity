using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;
using System.Collections.Generic;

public class PawnSpaceShip : Pawn
{
    CharacterController controller;
    BoxCollider collider;
    public float speed = 8f;
    private float speedreg;
    public float speedmultiplier = 1.5f;
    public float rotatespeed = 360f;
    private bool isboost;
    //private Image healthbar;
    //private Vector3 defaulthealthbarscale;
    private ControllerPlayer pawnparent;
    //private int lives;
    //private List<Image> hearts;


    [Header("AudioClips")]
    public AudioClip impactsound;
    public AudioClip firingsound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = this.gameObject.AddComponent<CharacterController>();
        collider = this.gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        isboost = false;
        speedreg = speed;
        this.gameObject.tag = "Player";
        health = GetComponent<Health>();
       
    }

    public void IstantiatePawnPlayerConnection(ControllerPlayer parent)
    {
        pawnparent = parent;
        //healthbar = parent.gethealthbar();
        //defaulthealthbarscale = healthbar.transform.localScale;
        //lives = parent.getLives();
        //hearts = parent.hearts;
    }

    public override void Move(Vector3 movevector)
    {
        if(isboost)
        {
            speed = speedreg * speedmultiplier;
        }
        else
        {
            speed = speedreg;
        }

        //adding Time.deltaTime so it updates in accordance to seconds rather than per frame
        controller.Move((movevector * speed) * Time.deltaTime);  //move the controller	
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z != 0)
        {
            controller.enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            controller.enabled = true;
        }
    }

    public override void Rotate(float angle)
    {
        transform.Rotate(new Vector3(0,0,angle * rotatespeed) * Time.deltaTime);
    }

    public void ToggleBoost()
    {
        isboost = !isboost;
        print(isboost ? "Boost Activated" : "Boost Deactivated");
    }

    public Image gethealthbar()
    {
        return (pawnparent.gethealthbar());
    }

    public Vector3 returnHealthScale()
    {
        return (pawnparent.returnHealthScale());
    }

    public int getLives()
    {
        return (pawnparent.getLives());
    }

    public List<Image> getHeartsList()
    {
        return (pawnparent.getHeartsList());
    }

    public AudioClip getAudio(string audiovariablename) //uses "Reflection" technique
    {
        AudioClip localaudio;
        Type targettype = this.GetType();
        FieldInfo[] fields = targettype.GetFields(BindingFlags.Public | BindingFlags.Instance); //create an array of all public AudioClip within this instance of the script
        // This example gets public instance fields. Adjust BindingFlags as needed.

        //Debug.Log($"Variables in {targettype.Name}:");
        foreach (FieldInfo field in fields)
        {
            //Debug.Log($"  Name: {field.Name}, Type: {field.FieldType}");
            if (field.Name == audiovariablename && field.FieldType == typeof(AudioClip))
            {
                print("meep2");
                AudioClip fieldValue = (AudioClip)field.GetValue(this); // 'this' is the instance
                localaudio = fieldValue;
                return (localaudio);
            }
        }


        return impactsound;
    }

    public bool IsOutOfLives()
    {
        if(pawnparent.getLives() <= 0)
        {
            return true;
        }
        else
        {
            pawnparent.setLives(-1);
            List<Image> _heartslist = pawnparent.getHeartsList();
            if(_heartslist.Count > 0)
            {
                Destroy(_heartslist[_heartslist.Count - 1].gameObject);
                _heartslist.Remove(_heartslist[_heartslist.Count - 1]);
                pawnparent.setHeartsList(_heartslist);
                if (_heartslist.Count <= 0)
                {
                    return true;
                }
            }
        }
        return false; 
    }

}
