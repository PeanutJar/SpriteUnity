using UnityEngine;

public class ShooterBullet : Shooter
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Shoot()
    {
        Shoot(projectile);
    }

    public void Shoot(GameObject proj)
    {
        Vector3 pawposition = gameObject.transform.position - ((transform.rotation * new Vector3(0, -0.5f, 0)));

        GameObject shot = Instantiate(proj, pawposition, transform.rotation);
    }
}
