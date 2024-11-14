using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> bulletPool;
    public bool isEnemyBullet = false; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; 
    }

    private void OnEnable()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();

        
        if (hitbox != null && ((isEnemyBullet && other.CompareTag("Player")) || (!isEnemyBullet && other.CompareTag("Enemy"))))
        {
            
            if (invincibility != null && !invincibility.isInvincible)
            {
                hitbox.Damage(damage);
                invincibility.TriggerInvincibility(); 
            }

            
            if (bulletPool != null)
            {
                bulletPool.Release(this);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        bulletPool = pool;
    }
}
