using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health;
    private InvincibilityComponent invincibility;

    private void Awake()
    {
        invincibility = GetComponent<InvincibilityComponent>();
    }

    public void Damage(int damageAmount)
    {
        
        if (invincibility != null && invincibility.isInvincible)
            return;

        
        if (health != null)
        {
            health.Subtract(damageAmount);

            
            if (invincibility != null)
            {
                invincibility.TriggerInvincibility();
            }
        }
    }
}
