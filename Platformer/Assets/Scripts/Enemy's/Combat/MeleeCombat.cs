using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : EnemyCombat
{
    [SerializeField] Transform attackPoint;
    [SerializeField] int amountOfAttacks = 1;
    [SerializeField] DamageType attackType = DamageType.Boxed;
    [SerializeField] Vector2 rangeOfAttack;
    EnemyMovement movement;
    Animator animator;
    float attackCooldownTimer = Mathf.Infinity;
    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
        GetComponent<HealthComponent>().EnemyDamaged += OnEnemyDamaged;
    }

    private void OnEnemyDamaged()
    {
        Debug.Log(name);
        RaycastHit2D hit = PlayerInSight();
        if (!hit.collider&&movement.enabled)
        {
            movement.DirectionChange();
        }
    }

    public override void Attack()
    {
        if (attackCooldownTimer > attackCooldown)
        {
            if (amountOfAttacks > 1)
            {
                animator.SetTrigger("attack" + Random.Range(1, amountOfAttacks + 1));
            }
            else
                animator.SetTrigger("attack1");
            attackCooldownTimer = 0;
        }
    }

    public override RaycastHit2D PlayerInSight()
    {
        RaycastHit2D hit = new RaycastHit2D();
        switch (attackType)
        {
            case DamageType.Radial:
                hit = Physics2D.CircleCast(attackPoint.position, rangeOfAttack.x, Vector2.right * transform.localScale.normalized, 0, playerLayer);
                break;
            case DamageType.Boxed:
                hit = Physics2D.BoxCast(attackPoint.position, rangeOfAttack, 0, Vector2.right * transform.localScale.normalized, 0, playerLayer);
                break;
            case DamageType.Linear:
                hit = Physics2D.Linecast(new Vector2(attackPoint.position.x - rangeOfAttack.x / 2, attackPoint.position.y), new Vector2(attackPoint.position.x + rangeOfAttack.x / 2, attackPoint.position.y), playerLayer);
                break;
        }
        return hit;
    }
    enum DamageType
    {
        Radial, Boxed, Linear
    }
    void Update()
    {
        attackCooldownTimer += Time.deltaTime;
        if (PlayerInSight().collider)
        {
            movement.enabled = false;
            Attack();
        }
        else
        {
            movement.enabled = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        switch (attackType)
        {
            case DamageType.Radial:
                Gizmos.DrawWireSphere(attackPoint.position, rangeOfAttack.x);
                break;
            case DamageType.Boxed:
                Gizmos.DrawWireCube(attackPoint.position, rangeOfAttack);
                break;
            case DamageType.Linear:
                Gizmos.DrawLine(new Vector2(attackPoint.position.x - rangeOfAttack.x / 2, attackPoint.position.y), new Vector2(attackPoint.position.x + rangeOfAttack.x / 2, attackPoint.position.y));
                break;
        }
    }
    private void OnDestroy()
    {
        GetComponent<HealthComponent>().EnemyDamaged -= OnEnemyDamaged;
    }
}
