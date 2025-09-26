using UnityEngine;

public class ProjectileMoveStraight : ProjectileMover
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Projectile proj = gameObject.GetComponent<Projectile>();
        transform.position = transform.position + (transform.up * proj.GetSpeed() * Time.deltaTime);
    }
}
