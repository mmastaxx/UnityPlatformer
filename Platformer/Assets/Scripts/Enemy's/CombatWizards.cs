using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatWizards : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private Vector2 rangeOfAttack;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform attackPoint;

    // private MovementWizards movementWizard;
    private float cooldownTimer = Mathf.Infinity;
    private Animator animator;
    private MovementWizards movement;
    private void Awake()
    {
        //movementWizard = GetComponentInParent<MovementWizards>();
        animator = GetComponent<Animator>();
        movement = GetComponent<MovementWizards>();
    }
    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        movement.enabled = !(PlayerInSight().collider != null);
        if (!movement.enabled)
        {
            cooldownTimer = 0;
            animator.SetTrigger("attack");
        }
    }
    private RaycastHit2D PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(attackPoint.position, rangeOfAttack, 0, Vector2.right * transform.localScale.normalized, 0, playerLayer);
        return hit;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, new Vector3(rangeOfAttack.x, rangeOfAttack.y));
    }
    private void PerformDamage()
    {
        RaycastHit2D hit = PlayerInSight();
        if (hit.collider)
        {
            hit.collider.GetComponent<HealthComponent>().TakeDamage(damage);
        }
        animator.ResetTrigger("attack");
    }
}
