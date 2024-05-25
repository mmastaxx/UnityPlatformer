using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatComponent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform blockPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private float blockRadius;
    private int attackIndex = 0;
    private float timeSinceAttack;
    [SerializeField] private float Damage;
    [SerializeField] private float attackCooldown = 0.1f;
    [SerializeField] private float comboCooldown = 1;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask projectileMask;
    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        Block();
    }
    void Attack()
    {
        if (timeSinceAttack > attackCooldown)
        {
            attackIndex++;
            if (timeSinceAttack > comboCooldown)
            {
                attackIndex = 1;
            }
            if (attackIndex > 3)
            {
                attackIndex = 1;
            }
            animator.SetInteger("attackIndex", attackIndex);
            animator.SetTrigger("attack");
            timeSinceAttack = 0.0f;
        }
    }
    void PerformDamage() 
    {
        Collider2D[] enemyArray = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyMask);
        foreach (var enemy in enemyArray)
        {
            enemy.GetComponent<HealthComponent>().TakeDamage(Damage);
        }
    }
    void Block()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("bBlock", true);
            Collider2D[] blockedArray = Physics2D.OverlapCircleAll(blockPoint.position, blockRadius, projectileMask);
            foreach (var projectile in blockedArray)
            {
                Debug.Log(projectile.name);
            }
            if (blockedArray.Length != 0)
            {
                animator.SetTrigger("parry");
            }
        }
        else if (Input.GetMouseButtonUp(1)) 
        {
            animator.SetBool("bBlock", false);
        }
    }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(blockPoint.position, blockRadius);
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
