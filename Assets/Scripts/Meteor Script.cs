using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class MeteorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private bool isinstantdeath = false;
    public Health healthcomponent;
    private Vector3 moveDirection;
    private Vector3 initialposition;
    public int scoreincreaser;
    [SerializeField] private Image healthbar;
    private Vector3 defaulthealthbarscale;
    void Start()
    {
        this.gameObject.tag = "Obstacle";
        healthcomponent = GetComponent<Health>();
        defaulthealthbarscale = healthbar.transform.localScale;
    }
    public void istanctiate(Vector3 pos)
    {
        initialposition = pos;
        moveDirection = (initialposition - (Vector3)transform.position).normalized; //point in direction of player position when istanciated
    }

    // Update is called once per frame
    void Update()
    {
        if (initialposition != null || moveDirection != null)
        {
            transform.position += (Vector3)(moveDirection * 2 * Time.deltaTime);

            if (transform.position.z != 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;
        if (transform.position.y > (Camera.main.transform.position.y + halfHeight + 2) || transform.position.y < (Camera.main.transform.position.y - halfHeight - 2) ||
            transform.position.x > (Camera.main.transform.position.x + halfWidth + 2) || transform.position.x < (Camera.main.transform.position.x - halfWidth - 2)) //can also incorporated a timer (for depending on how long projectile  has been off screen)
        {
            gameObject.GetComponent<DeathDestroy>().Die();
        }



    }

    public bool GetInstDeath()
    {
        return (isinstantdeath);
    }

    /*
    public void changehealthbar(int damage)
    {

        //new scale = default scale * ([health-damage]/maxhealth)
        int healthnum = healthcomponent.GetHealth();
        int maxhealthnum = healthcomponent.GetMaxHealth();
        float scalefactor = ((float)(healthnum - damage)/ maxhealthnum); //makes sure covert to float or double before devsion, else it will round down (due to c# calculation via integers)
        Vector3 currentScale = transform.localScale;
        Vector3 newScale = new Vector3(defaulthealthbarscale.x * scalefactor, defaulthealthbarscale.y, defaulthealthbarscale.z);
        // Assign the new scale to the object's localScale
        healthbar.transform.localScale = newScale;
    }
    */
    public Image gethealthbar()
    {
        return (healthbar);
    }

    public Vector3 returnHealthScale()
    {
        return (defaulthealthbarscale);
    }

}
