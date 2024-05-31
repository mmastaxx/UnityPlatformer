using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    Collider spikeCollider;
    Rigidbody2D body;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        Collider collider = GetComponent<Collider>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            var targetHealth = collision.GetComponent<HealthComponent>();
            targetHealth.TakeDamage(10);
        }
    }
    // Update is called once per frame

}
