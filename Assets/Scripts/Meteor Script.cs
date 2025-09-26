using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private bool isinstantdeath = false;
    [SerializeField] private int impactdamage = 1;
    void Start()
    {
        this.gameObject.tag = "Obstacle";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetDamage()
    {
        return (impactdamage);
    }
    public bool GetInstDeath()
    {
        return (isinstantdeath);
    }
}
