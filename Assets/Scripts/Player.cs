using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private PlayerMovement playerMovement;
    private Animator animator;

    // Referensi untuk senjata yang sedang aktif
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

    // Method untuk mengganti senjata
    public void EquipWeapon(Weapon newWeapon)
    {
        // Jika ada senjata aktif, nonaktifkan
        if (activeWeapon != null)
        {
            activeWeapon.gameObject.SetActive(false);
        }

        // Aktifkan senjata baru dan set sebagai senjata aktif
        activeWeapon = newWeapon;
        activeWeapon.gameObject.SetActive(true);
    }

    // Method untuk mengecek apakah Player memiliki senjata
    public bool HasWeapon()
    {
        return activeWeapon != null;
    }
}
