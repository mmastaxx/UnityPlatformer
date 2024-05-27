using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    enum DamageType
    {
        Radial, Boxed, Linear
    }
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private Vector2 rangeOfAttack;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int amountOfAttacks=1;
    [SerializeField] private DamageType attackType = DamageType.Boxed;
    private float cooldownTimer = Mathf.Infinity;
    private Animator animator;
    private EnemyMovement movement;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        movement.enabled = !(PlayerInSight().collider != null);
        if (!movement.enabled&& cooldownTimer > attackCooldown)
        {
            if (amountOfAttacks > 1)
            {
                animator.SetTrigger("attack" + Random.Range(1, amountOfAttacks + 1));
            }
            else
                animator.SetTrigger("attack1");
            cooldownTimer = 0;
        }
    }
    private RaycastHit2D PlayerInSight()
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
                hit = Physics2D.Linecast(new Vector2(attackPoint.position.x - rangeOfAttack.x / 2, attackPoint.position.y), new Vector2(attackPoint.position.x + rangeOfAttack.x / 2, attackPoint.position.y),playerLayer);
                break;
        }
 
        return hit;
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
    private void PerformDamage()
    {
        RaycastHit2D hit = PlayerInSight();
        if (hit.collider)
        {
            hit.collider.GetComponent<HealthComponent>().TakeDamage(damage);
        }
        for (int i = 1; i <= amountOfAttacks; i++)
        {
            animator.ResetTrigger("attack"+i);
        }
    }
}
