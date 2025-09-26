using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private bool isinstantdeath = false;
    public Health healthcomponent;
    void Start()
    {
        this.gameObject.tag = "Obstacle";
        healthcomponent = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetInstDeath()
    {
        return (isinstantdeath);
    }
}
