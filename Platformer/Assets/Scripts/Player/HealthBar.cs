using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    public void SetHealth(float health)
    {
        slider.value = health/100;
    }
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth / 100;
    }

}
