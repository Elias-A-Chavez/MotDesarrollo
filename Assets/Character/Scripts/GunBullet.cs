using UnityEngine;

public class GunBullet : MonoBehaviour
{
    private int damage;
    public float lifetime = 5f;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            //Basic_Entity enemy = other.GetComponent<Basic_Entity>();
           // if (enemy != null)
            {
                //enemy.GetDamage(damage);
            }
        }

        
        Destroy(gameObject);
    }
}
