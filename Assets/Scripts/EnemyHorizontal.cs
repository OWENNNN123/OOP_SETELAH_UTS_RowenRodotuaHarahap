using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private int direction;

    private void Start()
    {
        if (Random.Range(0, 2) == 0)
        {
            transform.position = new Vector2(-10f, Random.Range(-5f, 5f));
            direction = 1;
        }
        else
        {
            transform.position = new Vector2(10f, Random.Range(-5f, 5f));
            direction = -1;
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (transform.position.x > 10f)
        {
            direction = -1;
        }
        else if (transform.position.x < -10f)
        {
            direction = 1;
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
