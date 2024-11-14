using UnityEngine;

public class EnemyForward : Enemy
{
    private void Start()
    {
        transform.position = new Vector2(Random.Range(-8f, 8f), 10f);
        transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthComponent playerHealth = other.GetComponent<HealthComponent>();
            HealthComponent enemyHealth = GetComponent<HealthComponent>();

            if (playerHealth != null && enemyHealth != null)
            {
                playerHealth.Subtract(enemyHealth.GetHealth()); // Damage sebesar health enemy
            }
            Destroy(gameObject); // Hancurkan enemy setelah menabrak player
        }
    }
}
