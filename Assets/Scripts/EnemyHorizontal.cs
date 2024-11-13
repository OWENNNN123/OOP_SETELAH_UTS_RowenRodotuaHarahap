using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private int direction;

    private void Start()
    {
        // Atur posisi spawn secara acak di kiri atau kanan layar
        if (Random.Range(0, 2) == 0)
        {
            transform.position = new Vector2(-10f, Random.Range(-5f, 5f)); // Spawn di sisi kiri layar
            direction = 1; // Gerak ke kanan
        }
        else
        {
            transform.position = new Vector2(10f, Random.Range(-5f, 5f)); // Spawn di sisi kanan layar
            direction = -1; // Gerak ke kiri
        }
    }

    private void Update()
    {
        // Gerakan horizontal
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        // Balik arah jika mencapai batas layar
        if (transform.position.x > 10f)
        {
            direction = -1;
        }
        else if (transform.position.x < -10f)
        {
            direction = 1;
        }
    }
}
