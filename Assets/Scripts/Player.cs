using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public static Player Instance { get; private set; }

    private PlayerMovement playerMovement;
    private Animator animator;

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
    }

    private void Start()
    {
        
        playerMovement = GetComponent<PlayerMovement>();
        
        
        animator = transform.Find("Engine/EngineEffect").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        
        playerMovement.Move();
    }

    private void LateUpdate()
    {
        
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}
