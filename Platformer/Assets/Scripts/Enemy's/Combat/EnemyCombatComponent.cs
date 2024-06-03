using UnityEngine;

public abstract class EnemyCombat: MonoBehaviour
{
    public float damage;
    public float attackCooldown;
    public LayerMask playerLayer;

    public abstract RaycastHit2D PlayerInSight();
    public abstract void Attack();
    public void PerformDamage()
    {
        RaycastHit2D hit = PlayerInSight();
        if (hit.collider)
        {
            hit.collider.GetComponent<HealthComponent>().TakeDamage(damage);
        }
    }
    
}
