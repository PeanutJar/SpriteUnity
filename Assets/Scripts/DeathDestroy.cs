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
            if (gameObject.GetComponent<MeteorScript>().healthcomponent.GetHealth() <= 0) //prevents score being added unless player actualy shoots them down
            {
                Camera.main.GetComponent<GneralScript>().score += gameObject.GetComponent<MeteorScript>().scoreincreaser;
            }
        }
        Destroy(gameObject);
    }
}
