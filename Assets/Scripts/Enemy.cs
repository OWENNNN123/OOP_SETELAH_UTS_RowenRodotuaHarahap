using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 1; // Level setiap enemy
    public float speed = 2f; // Kecepatan dasar enemy

    private void Start()
    {
        // Atur posisi menghadap ke bawah layar
        transform.up = Vector2.down;
    }

    // Update method akan diimplementasikan di class turunan
}
