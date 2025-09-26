using UnityEngine;

public class DeathRecenter : Death
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
        gameObject.GetComponent<CharacterController>().enabled = false;
        gameObject.transform.position = new Vector3(0, 0, 0);
        gameObject.GetComponent<CharacterController>().enabled = false;
    }
}
