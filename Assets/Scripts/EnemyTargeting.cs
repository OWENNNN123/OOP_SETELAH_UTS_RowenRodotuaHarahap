using UnityEngine;

public class EnemyTargeting : Enemy
{
    private Transform playerTransform; // Referensi ke posisi player

    private void Start()
    {
        // Cari objek Player di scene dan simpan referensinya
        playerTransform = Player.Instance.transform;

        // Atur posisi spawn enemy di sekitar layar secara acak
        float spawnX = Random.Range(-8f, 8f);
        float spawnY = Random.Range(6f, 10f);
        transform.position = new Vector2(spawnX, spawnY);

        // Atur rotasi enemy agar menghadap ke arah player
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.up = direction;
    }

    private void Update()
    {
        // Jika ada player, gerakkan enemy menuju ke arah player
        if (playerTransform != null)
        {
            // Pindahkan enemy ke arah player
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

            // Jika jarak enemy ke player sangat dekat, hancurkan enemy
            if (Vector2.Distance(transform.position, playerTransform.position) < 0.5f)
            {
                Destroy(gameObject); // Hapus enemy ketika mencapai player
            }
        }
    }
}
