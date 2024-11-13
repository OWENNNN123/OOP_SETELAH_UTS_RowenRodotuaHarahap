using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 0.5f;

    [Header("Bullets")]
    public Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> bulletPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;

    private float shootTimer;

    private void Awake()
    {
        // Inisialisasi object pool untuk bullet
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            collectionCheck,
            defaultCapacity,
            maxSize
        );
    }

    private void Update()
    {
        // Mengatur interval untuk tembakan
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootIntervalInSeconds)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private Bullet CreateBullet()
    {
        // Buat bullet baru dan set pool-nya
        Bullet newBullet = Instantiate(bulletPrefab);
        newBullet.gameObject.SetActive(false);
        newBullet.SetPool(bulletPool);
        return newBullet;
    }

    private void OnTakeFromPool(Bullet bullet)
    {
        // Aktifkan bullet dan set posisi serta rotasinya
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Bullet bullet)
    {
        // Nonaktifkan bullet saat dikembalikan ke pool
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void Shoot()
    {
        // Ambil bullet dari pool dan set velocity-nya
        Bullet bullet = bulletPool.Get();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = bulletSpawnPoint.up * bullet.bulletSpeed;
        }
    }
}
