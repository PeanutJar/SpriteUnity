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
                Camera.main.GetComponent<GneralScript>().enemyspawnlist.Remove(gameObject); //need to modify the function "game end" in general script so it can be called frm here
                //to see if the player has one, else the objexts could be destroyed before the gneral script update is called to make the check
            }
        }
        else if(gameObject.tag == "Player")
        {
            Camera.main.GetComponent<GneralScript>().GameEnd(false); //an alternative is passing in the gameobject, if wanting to discern between numerous players
        }
        Destroy(gameObject);
    }
}
