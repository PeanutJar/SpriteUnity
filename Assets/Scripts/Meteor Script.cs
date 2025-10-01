using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class MeteorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private bool isinstantdeath = false;
    public Health healthcomponent;
    private Vector3 moveDirection;
    private Vector3 initialposition;
    void Start()
    {
        this.gameObject.tag = "Obstacle";
        healthcomponent = GetComponent<Health>();
    }
    public void istanctiate(Vector3 pos)
    {
        initialposition = pos;
        moveDirection = (initialposition - (Vector3)transform.position).normalized;
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

}
