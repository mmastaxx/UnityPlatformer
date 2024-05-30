using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public void SetHealth(float health)
    {
        slider.value = health/100;
    }
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth / 100;
    }

}
