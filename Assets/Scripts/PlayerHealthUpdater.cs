using TMPro;
using UnityEngine;

public class PlayerHealthUpdater : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public HealthComponent playerHealth;

    private void Update()
    {
        healthText.text = $"Health: {playerHealth.GetHealth()}";
    }
}
