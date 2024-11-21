using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy; 
    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3; 

    public int totalKill = 0; 
    public int totalKillWave = 0; 
    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0; 
    public int defaultSpawnCount = 1; 
    public int spawnCountMultiplier = 1; 
    public int multiplierIncreaseCount = 1; 

    public CombatManager combatManager; 
    public bool isSpawning = false; 

    private void Start()
    {
        
        StartCoroutine(SpawnEnemies());
    }

    private System.Collections.IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (isSpawning)
            {
                for (int i = 0; i < defaultSpawnCount * spawnCountMultiplier; i++)
                {
                    Instantiate(spawnedEnemy, GetRandomSpawnPosition(), Quaternion.identity);
                    spawnCount++;
                    yield return new WaitForSeconds(spawnInterval);
                }
            }
            yield return null;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        
        float spawnX = Random.Range(-8f, 8f);
        float spawnY = Random.Range(-4f, 4f);
        return new Vector3(spawnX, spawnY, 0f);
    }

    public void EnemyKilled()
    {
        totalKill++;
        totalKillWave++;

        if (totalKill % minimumKillsToIncreaseSpawnCount == 0)
        {
            spawnCountMultiplier++;
        }
    }
}
