using UnityEngine;

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
                if(gameObject.GetComponent<MeteorScript>() != null)
                {
                    gameObject.GetComponent<MeteorScript>().SpawnDeathRocks();
                }
            }
        }
        else if(gameObject.tag == "Player")
        {
            Camera.main.GetComponent<GneralScript>().GameEnd(); //an alternative is passing in the gameobject, if wanting to discern between numerous players
        }
        Destroy(gameObject);
    }
}
