using UnityEngine;

public class BulletScript : Projectile
{
    public float speed = 10f;
    public float speedmultiplier = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Camera cam;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        this.gameObject.tag = "Projectile";
        movercomponent = GetComponent<ProjectileMover>();
        healthcomponent = GetComponent<Health>();
        cam = Camera.main;
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > (cam.transform.position.y + halfHeight) || transform.position.y < (cam.transform.position.y - halfHeight) || 
            transform.position.x > (cam.transform.position.x + halfWidth) || transform.position.x < (cam.transform.position.x - halfWidth)) //can also incorporated a timer (for depending on how long projectile  has been off screen)
        {
            gameObject.GetComponent<DeathDestroy>().Die();
        }
    }

    public override float GetSpeed()
    {
        return (speed * speedmultiplier);
    }
}
