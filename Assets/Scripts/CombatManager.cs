using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners; // Array spawner enemy
    public float timer = 0; // Timer untuk wave
    [SerializeField] private float waveInterval = 5f; // Interval antar wave
    public int waveNumber = 1; // Nomor wave saat ini
    public int totalEnemies = 0; // Total jumlah enemy di wave

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waveInterval)
        {
            StartNextWave();
            timer = 0;
        }
    }

    private void StartNextWave()
    {
        waveNumber++;
        totalEnemies = 0;

        foreach (var spawner in enemySpawners)
        {
            spawner.isSpawning = true; // Aktifkan spawning di semua spawner
            spawner.defaultSpawnCount = waveNumber; // Tingkatkan jumlah spawn
            spawner.totalKillWave = 0; // Reset kill wave
        }
    }

    public void EnemyKilled()
    {
        totalEnemies--;

        if (totalEnemies <= 0)
        {
            foreach (var spawner in enemySpawners)
            {
                spawner.isSpawning = false;
            }
        }
    }
}
