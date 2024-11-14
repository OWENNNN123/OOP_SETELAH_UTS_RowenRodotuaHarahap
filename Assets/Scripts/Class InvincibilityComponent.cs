using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7;            
    [SerializeField] private float blinkInterval = 0.1f;       
    [SerializeField] private Color blinkColor;           
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public bool isInvincible = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;   
    }

    public void TriggerInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        for (int i = 0; i < blinkingCount; i++)
        {
            spriteRenderer.color = blinkColor;    
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = originalColor; 
            yield return new WaitForSeconds(blinkInterval);
        }
        isInvincible = false;
    }
}
