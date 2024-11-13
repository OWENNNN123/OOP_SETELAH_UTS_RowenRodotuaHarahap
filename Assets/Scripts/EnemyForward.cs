using UnityEngine;

public class EnemyForward : Enemy
{
    private void Start()
    {
        // Spawn di posisi acak di atas layar
        transform.position = new Vector2(Random.Range(-8f, 8f), 10f);

        // Atur rotasi agar enemy tidak menghadap ke player, hanya mengarah ke bawah layar
        transform.rotation = Quaternion.Euler(0, 0, 180); // Menghadap ke bawah layar
    }

    private void Update()
    {
        // Gerakkan enemy ke bawah
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);

        // Hapus jika keluar layar di bagian bawah
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
