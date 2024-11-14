using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag(gameObject.tag))
            return;

        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();
            if (invincibility != null && !invincibility.isInvincible)
            {
                hitbox.Damage(damage);
                invincibility.TriggerInvincibility(); 
            }
        }
    }
}
