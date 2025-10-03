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
        print("Health:" + health + " " + gameObject.tag);
        if (health <= 0 || _instdeath)
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

    public void ChangeHealthBar(int _damage, Image _healthbar, Vector3 _defaulthealthbarscale)
    {
        //new scale = default scale * ([health-damage]/maxhealth)
       
        float scalefactor = ((float)(health - _damage) / maxhealth); //makes sure covert to float or double before devsion, else it will round down (due to c# calculation via integers)
        Vector3 currentScale = transform.localScale;
        Vector3 newScale = new Vector3(_defaulthealthbarscale.x * scalefactor, _defaulthealthbarscale.y, _defaulthealthbarscale.z);
        // Assign the new scale to the object's localScale
        _healthbar.transform.localScale = newScale;
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
