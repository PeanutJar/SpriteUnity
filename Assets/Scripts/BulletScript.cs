using UnityEngine;

public class BulletScript : Projectile
{
    public float speed = 10f;
    public float speedmultiplier = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.tag = "Projectile";
        movercomponent = GetComponent<ProjectileMover>();
        healthcomponent = GetComponent<Health>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override float GetSpeed()
    {
        return (speed * speedmultiplier);
    }
}
