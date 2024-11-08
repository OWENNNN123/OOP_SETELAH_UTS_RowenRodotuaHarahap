using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
        LevelManager = FindObjectOfType<LevelManager>();
        if (LevelManager == null)
        {
            Debug.LogWarning("LevelManager tidak ditemukan di scene.");
        }
    }
}
