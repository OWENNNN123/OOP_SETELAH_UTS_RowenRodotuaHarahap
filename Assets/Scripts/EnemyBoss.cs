using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : MonoBehaviour
{
    [Header("Movement Stats")]
    [SerializeField] private float horizontalSpeed = 2f;
    private bool movingRight = true; 

    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 2f;
    [SerializeField] private Bullet bulletPrefab; 
    [SerializeField] private Transform bulletSpawnPoint; 

    private IObjectPool<Bullet> bulletPool;
    private float shootTimer;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            false, 
            10, 
            50
        );
    }

    private void Update()
    {
        
        MoveHorizontally();

        
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootIntervalInSeconds)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void MoveHorizontally()
    {
        
        if (movingRight)
        {
            transform.Translate(Vector2.right * horizontalSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * horizontalSpeed * Time.deltaTime);
        }

        
        if (transform.position.x > 7f)  
        {
            movingRight = false;
        }
        else if (transform.position.x < -7f) 
        {
            movingRight = true;
        }
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bulletPrefab);
        newBullet.gameObject.SetActive(false);
        newBullet.SetPool(bulletPool);
        return newBullet;
    }

    private void OnTakeFromPool(Bullet bullet)
    {
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.isEnemyBullet = true; 
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
            rb.velocity = bulletSpawnPoint.up * bullet.bulletSpeed;
        }
    }
}
