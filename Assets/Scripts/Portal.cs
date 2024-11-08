using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed = 2f;       
    [SerializeField] private float rotateSpeed = 50f; 
    private Vector2 newPosition;                    
    private LevelManager levelManager;               

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        ChangePosition(); 
    }

    private void Update()
    {
        
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (levelManager != null)
            {
                levelManager.TriggerGameOver();
            }
        }
    }

    private void ChangePosition()
    {
        
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        newPosition = new Vector2(x, y);
    }
}
