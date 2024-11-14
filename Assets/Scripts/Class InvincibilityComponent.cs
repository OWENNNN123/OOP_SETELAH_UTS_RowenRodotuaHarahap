using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7;            // Jumlah blinking
    [SerializeField] private float blinkInterval = 0.1f;       // Interval blinking
    [SerializeField] private Material blinkMaterial;           // Material saat blinking (warna merah untuk player, putih untuk enemy)
    
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;   // Menyimpan material asli
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
            spriteRenderer.material = blinkMaterial;    // Mengganti material ke material blink
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.material = originalMaterial; // Kembali ke material asli
            yield return new WaitForSeconds(blinkInterval);
        }
        isInvincible = false;
    }
}
