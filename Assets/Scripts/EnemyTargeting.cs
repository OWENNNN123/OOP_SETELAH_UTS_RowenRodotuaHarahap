using UnityEngine;

public class EnemyTargeting : Enemy
{
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = Player.Instance.transform;

        float spawnX = Random.Range(-8f, 8f);
        float spawnY = Random.Range(6f, 10f);
        transform.position = new Vector2(spawnX, spawnY);

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.up = direction;
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, playerTransform.position) < 0.5f)
            {
                Destroy(gameObject);
            }
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
                playerHealth.Subtract(enemyHealth.GetHealth()); 
            }
            Destroy(gameObject); 
        }
    } 
}
