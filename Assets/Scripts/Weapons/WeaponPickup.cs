using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder; // Reference to the weapon to be picked up
    private Weapon weapon;

    private void Awake()
    {
        weapon = weaponHolder;
    }

    private void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false); // Hide the weapon initially
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the weapon as a child of the player
            weapon.transform.SetParent(other.transform);

            // Equip the weapon on the player
            Player.Instance.EquipWeapon(weapon);

            // Position and rotate the weapon relative to the player
            weapon.transform.localPosition = new Vector3(0, 0.5f, 0); // Adjust to position weapon above player
            weapon.transform.localRotation = Quaternion.Euler(0, 0, 0); // Ensure the rotation is zeroed

            // Show the weapon's visual
            TurnVisual(true);

            // Optionally, deactivate the WeaponPickup object to prevent re-picking
            gameObject.SetActive(false);
        }
    }

    private void TurnVisual(bool on)
    {
        weapon.gameObject.SetActive(on); // Toggle weapon visibility
    }
}
