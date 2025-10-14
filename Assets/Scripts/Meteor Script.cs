using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Reflection;
using System.Linq;
using NUnit.Framework.Internal;
using static UnityEngine.GraphicsBuffer;

public class MeteorScript : Obstacle
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 moveDirection;
    private Vector3 initialposition;
    [SerializeField] private Image healthbar;
    private Vector3 defaulthealthbarscale;
    [SerializeField] private GameObject asteroidondeathprefab;
    [SerializeField] private int spawnasteroids;

    [Header("AudioClips")]
    public AudioClip collisionsound;
    void Start()
    {
        this.gameObject.tag = "Obstacle";
        healthcomponent = GetComponent<Health>();
        defaulthealthbarscale = healthbar.transform.localScale;
        initalposition = transform.position;
    }
    public override void setDirection(Vector3 pos)
    {
        initialposition = pos;
        moveDirection = (initialposition - (Vector3)transform.position).normalized; //point in direction of player/target position when istanciated
    }
    public void setMoveDirection(Vector3 pos)
    {
        moveDirection = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (initialposition != null || moveDirection != null)
        {
            transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);

            if (transform.position.z != 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
        GneralScript gencam = Camera.main.gameObject.GetComponent<GneralScript>();
        if(transform.position.y > gencam.borderTop)
        {
            transform.position = new Vector3(transform.position.x, gencam.borderBottom + 1, 0);
        }
        else if (transform.position.y < gencam.borderBottom)
        {
            transform.position = new Vector3(transform.position.x, gencam.borderTop - 1, 0);
        }
        if (transform.position.x > gencam.borderRight)
        {
            transform.position = new Vector3(gencam.borderLeft + 1, transform.position.y, 0);
        }
        else if (transform.position.x < gencam.borderLeft)
        {
            transform.position = new Vector3(gencam.borderRight - 1, transform.position.y, 0);
        }



    }

    public void SpawnDeathRocks()
    {
        if(spawnasteroids > 0 && asteroidondeathprefab != null)
        {
            for(int i = 0; i < spawnasteroids; i++) {
                //Calculate angle offset for each object
                float angleStep = 360f / spawnasteroids;
                float angle = i * angleStep;

                // Rotate the base direction around the target’s up axis
                Quaternion rotation = Quaternion.AngleAxis(angle, transform.forward);
                Vector3 offset = rotation * moveDirection * 0.5f;

                // Apply target’s rotation to align circle with its facing direction
                Vector3 spawnPosition = transform.position + offset;

                GameObject obstacle = Instantiate(asteroidondeathprefab, spawnPosition, Quaternion.identity, 
                    Camera.main.GetComponent<GneralScript>().gamelayer.transform) as GameObject;

                obstacle.GetComponent<MeteorScript>().setMoveDirection(moveDirection); //so it still moves in the same direction

                Camera.main.GetComponent<GneralScript>().enemyspawnlist.Add(obstacle);
            }
        }
    }

    public override bool GetInstDeath()
    {
        return (isinstantdeath);
    }

    public override Image gethealthbar()
    {
        return (healthbar);
    }

    public override Vector3 returnHealthScale()
    {
        return (defaulthealthbarscale);
    }

    //if passed string matches the according AudioClip variable name, then passes that variable's value (passing via variabel name instead of actual audio file name, cuz I intend for the audio clips to still
    //be viewable through the inspector, so people can just pass the given/assigned/desired variable name instead of the actual file name. I think this may be better for if you may want to switch what sounds
    //you use for the same mechanics (ex: chanign explosion sounds).
    public override AudioClip getAudio(string audiovariablename) //uses "Reflection" technique
    {
        AudioClip localaudio;
        Type targettype = this.GetType(); //ultimately we want to get variables of type AudioClip within the class (type) of MeteorScript
        FieldInfo[] fields = targettype.GetFields(BindingFlags.Public | BindingFlags.Instance); //create an array of all public AudioClip within this instance of the script
        // This example gets public instance fields. Adjust BindingFlags as needed.
        //print(fields.Count());
        //Debug.Log($"Variables in {targettype.Name}:");
        foreach (FieldInfo field in fields)
        {
            //Debug.Log($"  Name: {field.Name}, Type: {field.FieldType}");
            if (field.Name == audiovariablename && field.FieldType == typeof(AudioClip))
            {
                AudioClip fieldValue = (AudioClip)field.GetValue(this); // 'this' is the instance
                localaudio = fieldValue;
                return (localaudio);
            }
        }


        return collisionsound;
    }

}
