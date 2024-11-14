using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 1; 
    public float speed = 2f; 

    private void Start()
    {
        
        transform.up = Vector2.down;
    }

    
}
