using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            var targetHealth = collision.GetComponent<HealthComponent>();
            targetHealth.TakeDamage(10);
        }
    }
}
