using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder; 
    private Weapon weapon;

    private void Awake()
    {
        weapon = weaponHolder;
    }

    private void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            weapon.transform.SetParent(other.transform);

        
            Player.Instance.EquipWeapon(weapon);

            
            weapon.transform.localPosition = Vector3.zero;

            
            TurnVisual(true);
        }
    }

    private void TurnVisual(bool on)
    {
        weapon.gameObject.SetActive(on); // Mengaktifkan atau menonaktifkan tampilan senjata
    }
}
