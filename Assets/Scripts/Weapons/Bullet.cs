using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> bulletPool;
    public bool isEnemyBullet = false; // Menandai apakah bullet ini dari enemy atau player

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Memastikan bullet tidak terpengaruh fisika
    }

    private void OnEnable()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();

        // Pastikan bullet hanya mengenai target yang sesuai
        if (hitbox != null)
        {
            if ((isEnemyBullet && other.CompareTag("Player")) || (!isEnemyBullet && other.CompareTag("Enemy")))
            {
                if (invincibility != null && !invincibility.isInvincible)
                {
                    hitbox.Damage(damage);
                    invincibility.TriggerInvincibility(); // Memicu efek flashing
                }

                // Kembalikan bullet ke pool setelah mengenai target
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
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        bulletPool = pool;
    }
}
