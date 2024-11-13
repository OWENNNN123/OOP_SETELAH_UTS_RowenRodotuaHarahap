using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;

    private Rigidbody2D rb;
    private IObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true; // Agar bullet tidak terpengaruh oleh fisika
        }
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed; // Set kecepatan bullet ke arah atas
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Damage ke HitboxComponent jika ada
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            hitbox.Damage(damage);
        }

        // Kembalikan bullet ke pool setelah mengenai sesuatu
        if (bulletPool != null)
        {
            bulletPool.Release(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        // Kembalikan bullet ke pool jika keluar dari layar
        if (bulletPool != null)
        {
            bulletPool.Release(this);
        }
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        bulletPool = pool;
    }
}
