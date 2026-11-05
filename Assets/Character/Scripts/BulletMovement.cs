using BattleRoyaleUI;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    
    public int damage = 20;
    public float shootForce = 45f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Animator animatorMc;

    private int _clickLeft = 0;

    void Update()
    {
        
        if (Input.GetMouseButtonDown(_clickLeft))
        {
            TriggerAttackAnimation();
        }
    }

    private void TriggerAttackAnimation()
    {
        animatorMc.ResetTrigger("AttackTrigger");
        animatorMc.SetTrigger("AttackTrigger");
    }

    
    public void ShootGun()
    {
       
        GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        
        //GunBullet bullet = bulletObj.GetComponent<GunBullet>();
        //if (bullet != null)
        {
           // bullet.SetDamage(damage);
        }

        Rigidbody rb = bulletObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(bulletSpawnPoint.forward * shootForce, ForceMode.Impulse);
        }
    }

    public void BuffDamage() => damage *= 2;
    public void NormalDamage() => damage /= 2;
}
