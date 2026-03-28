using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image ImageHealthbar;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        ImageHealthbar.fillAmount = currentHealth / maxHealth;
    }

}