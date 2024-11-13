using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public void ActivateInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(BlinkingCoroutine());
        }
    }

    private IEnumerator BlinkingCoroutine()
    {
        isInvincible = true;
        
        for (int i = 0; i < blinkingCount; i++)
        {
            spriteRenderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkInterval);
        }

        isInvincible = false;
    }
}
