using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private PlayerMovement playerMovement;
    private Animator animator;

    
    private Weapon activeWeapon;

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

    private void Update()
    {
        // Check input to shoot
        if (Input.GetButtonDown("Fire1") && HasWeapon())
        {
            activeWeapon.Shoot();
        }
    }

    
    public void EquipWeapon(Weapon newWeapon)
    {
        
        if (activeWeapon != null)
        {
            activeWeapon.gameObject.SetActive(false);
        }

        
        activeWeapon = newWeapon;

        
        activeWeapon.transform.SetParent(transform);

        
        activeWeapon.transform.localPosition = Vector3.zero; 
        activeWeapon.transform.localRotation = Quaternion.identity;

        
        activeWeapon.gameObject.SetActive(true);
    }

    
    public bool HasWeapon()
    {
        return activeWeapon != null;
    }
}
