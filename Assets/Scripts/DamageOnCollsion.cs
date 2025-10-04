using System.ComponentModel;
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
                    if (other.gameObject.tag == "Player")
                    {
                        other.gameObject.GetComponent<AudioPlayer>().PlayAudio(other.gameObject.GetComponent<PawnSpaceShip>().getAudio("impactsound"), 1f);
                        other.gameObject.GetComponent<Health>().ChangeHealthBar(impactdamage, other.gameObject.GetComponent<PawnSpaceShip>().gethealthbar(), other.gameObject.GetComponent<PawnSpaceShip>().returnHealthScale());
                    }
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
                    //other.gameObject.GetComponent<MeteorScript>().changehealthbar(impactdamage);
                    other.gameObject.GetComponent<Health>().ChangeHealthBar(impactdamage, other.gameObject.GetComponent<MeteorScript>().gethealthbar(), other.gameObject.GetComponent<MeteorScript>().returnHealthScale());
                    other.gameObject.GetComponent<AudioPlayer>().PlayAudio(other.gameObject.GetComponent<MeteorScript>().getAudio("explosionsound"), 1f);
                    other.gameObject.GetComponent<Health>().TakeDamage(impactdamage, false);
                }
            }
        }
        else if(gameObject.tag == "Player")
        {
            if (other.gameObject != null && other.gameObject.tag == "Obstacle")
            {
                if (other.gameObject.GetComponent<DeathDestroy>() != null)
                {
                    other.gameObject.GetComponent<DeathDestroy>().Die();
                }
            }
        }

    }
}
