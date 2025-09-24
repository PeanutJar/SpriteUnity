using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isinstantdeath = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Health>() != null) 
            {
                other.gameObject.GetComponent<Health>().TakeDamage(1, isinstantdeath);
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
