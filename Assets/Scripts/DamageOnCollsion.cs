using UnityEngine;

public class DamageOnCollsion : MonoBehaviour
{
    [SerializeField] private int impactdamage = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "Obstacle") //all collision triggers conditions relating to obstacles
        {
            if (other.gameObject.tag != "Obstacle")
            {
                if (other.gameObject.GetComponent<Health>() != null)
                {
                    bool isdie = gameObject.GetComponent<MeteorScript>().GetInstDeath();
                    other.gameObject.GetComponent<Health>().TakeDamage(impactdamage, isdie);
                }

            }
        }
        else if(gameObject.tag == "Projectile") //all collision triggers conditions relating to projectiles
        {
            if(other.gameObject.tag == "Obstacle")
            {
                if (other.gameObject.GetComponent<Health>() != null)
                {
                    //int damage = gameObject.GetComponent<MeteorScript>().GetDamage();
                    //bool isdie = gameObject.GetComponent<MeteorScript>().GetInstDeath();
                    //other.gameObject.GetComponent<Health>().TakeDamage(damage, isdie);
                }
            }
        }
        /*
        if (other.gameObject.GetComponent<SpriteRenderer>() != null) //if object has the sprite renderer component
        {
            //set triggered sprite's color to red if already white and white if not already white
            other.gameObject.GetComponent<SpriteRenderer>().color = other.gameObject.GetComponent<SpriteRenderer>().color == Color.white ? Color.red : Color.white;
        }
        */






    }
}
