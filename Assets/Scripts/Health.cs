using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int health;
    [SerializeField] private int maxhealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int _damage, bool _instdeath)
    {
        health -= _damage;
        print("Health:" + health);
        if(health <= 0 || _instdeath)
        {
            Die();
        }
    }

    public void Heal(int _heal)
    {
        if ((health + _heal) < maxhealth)
        {
            health += _heal;
        }
        else
        {
            health = maxhealth;
        }
    }

    public void Die()
    {
        Death deathcomponent = GetComponent<Death>();

        if(deathcomponent != null)
        {
            deathcomponent.Die();
        }
        else
        {
            Debug.Log("This gameobject does not have a death component");
        }
    }

    public int GetMaxHealth()
    {
        return (maxhealth);
    }
    public int GetHealth()
    {
        return (health);
    }
}
