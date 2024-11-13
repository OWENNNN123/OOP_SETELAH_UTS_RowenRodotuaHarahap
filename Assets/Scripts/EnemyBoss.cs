using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : MonoBehaviour
{
    public int level = 1;
    public float speed = 2f;
    public float shootIntervalInSeconds = 2f;

    [Header("Bullets")]
    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;

    private IObjectPool<Bullet> bulletPool;
    private float shootTimer;
    private float direction = 1; // 1 untuk kanan, -1 untuk kiri
    private float screenLimit = 8f; // Batas layar horizontal (atur sesuai dengan lebar layar)

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject
        );
    }

    private void Update()
    {
        // Gerakan horizontal bolak-balik
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        // Jika EnemyBoss mencapai batas layar, balikkan arah
        if (transform.position.x >= screenLimit)
        {
            direction = -1;
        }
        else if (transform.position.x <= -screenLimit)
        {
            direction = 1;
        }

        // Timer untuk menembak peluru
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootIntervalInSeconds)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.gameObject.SetActive(false);
        bullet.SetPool(bulletPool);
        return bullet;
    }

    private void OnTakeFromPool(Bullet bullet)
    {
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void Shoot()
    {
        Bullet bullet = bulletPool.Get();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * bullet.bulletSpeed;
        }
    }
}
