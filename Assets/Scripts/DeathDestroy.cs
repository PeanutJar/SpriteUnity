using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DeathDestroy : Death
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        if(gameObject.tag == "Obstacle")
        {
            if (gameObject.GetComponent<Obstacle>().healthcomponent.GetHealth() <= 0) //prevents score being added unless player actualy shoots them down
            {
                Camera.main.GetComponent<GneralScript>().score += gameObject.GetComponent<Obstacle>().scoreincreaser;
                Camera.main.GetComponent<GneralScript>().scoretext.text = "Score: " + Camera.main.GetComponent<GneralScript>().score;
                if (gameObject.GetComponent<MeteorScript>() != null)
                {
                    gameObject.GetComponent<MeteorScript>().SpawnDeathRocks();
                }
                Camera.main.GetComponent<GneralScript>().haswon = Camera.main.GetComponent<GneralScript>().GameEnd();
                Camera.main.GetComponent<GneralScript>().enemyspawnlist.Remove(gameObject);
            }
        }
        else if(gameObject.tag == "Player")
        {
            Camera.main.GetComponent<GneralScript>().GameEnd(false);
        }
        Destroy(gameObject);
    }
}
