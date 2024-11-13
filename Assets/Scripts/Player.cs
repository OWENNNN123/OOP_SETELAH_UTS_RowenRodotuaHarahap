using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private PlayerMovement playerMovement;
    private Animator animator;

    // Reference to the currently active weapon
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

    // Method to equip a new weapon
    public void EquipWeapon(Weapon newWeapon)
    {
        // Deactivate any currently active weapon
        if (activeWeapon != null)
        {
            activeWeapon.gameObject.SetActive(false);
        }

        // Set the new weapon as the active weapon
        activeWeapon = newWeapon;

        // Parent the weapon to the player so it follows the player's movement
        activeWeapon.transform.SetParent(transform);

        // Reset the weapon's local position and rotation to align it with the player
        activeWeapon.transform.localPosition = Vector3.zero; // Adjust this if you need a specific offset
        activeWeapon.transform.localRotation = Quaternion.identity;

        // Activate the weapon
        activeWeapon.gameObject.SetActive(true);
    }

    // Method to check if the player has a weapon
    public bool HasWeapon()
    {
        return activeWeapon != null;
    }
}
