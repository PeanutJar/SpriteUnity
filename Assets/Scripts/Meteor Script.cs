using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
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
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;
        if (transform.position.y > (Camera.main.transform.position.y + halfHeight + 2) || transform.position.y < (Camera.main.transform.position.y - halfHeight - 2) ||
            transform.position.x > (Camera.main.transform.position.x + halfWidth + 2) || transform.position.x < (Camera.main.transform.position.x - halfWidth - 2)) //can also incorporated a timer (for depending on how long projectile  has been off screen)
        {
            gameObject.GetComponent<DeathDestroy>().Die();
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
            }
        }
    }

    public override bool GetInstDeath()
    {
        return (isinstantdeath);
    }

    /*
    public void changehealthbar(int damage)
    {

        //new scale = default scale * ([health-damage]/maxhealth)
        int healthnum = healthcomponent.GetHealth();
        int maxhealthnum = healthcomponent.GetMaxHealth();
        float scalefactor = ((float)(healthnum - damage)/ maxhealthnum); //makes sure covert to float or double before devsion, else it will round down (due to c# calculation via integers)
        Vector3 currentScale = transform.localScale;
        Vector3 newScale = new Vector3(defaulthealthbarscale.x * scalefactor, defaulthealthbarscale.y, defaulthealthbarscale.z);
        // Assign the new scale to the object's localScale
        healthbar.transform.localScale = newScale;
    }
    */
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
