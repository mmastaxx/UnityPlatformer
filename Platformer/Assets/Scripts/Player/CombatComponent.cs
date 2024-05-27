using UnityEngine;

public class CombatComponent : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform blockPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask projectileMask;
    [SerializeField] private float attackRadius;
    [SerializeField] private float blockRadius;
    [SerializeField] private float Damage;
    [SerializeField] private float attackCooldown = 0.1f;
    [SerializeField] private float rollCooldown = 0.5f;
    [SerializeField] private float comboCooldown = 1;
    private int attackIndex = 0;
    private float timeSinceAttack;
    private float timeSinceRoll;
    private Animator animator;
    private PlayerInput playerInput;
    private Movement playerMovement;
    private HealthComponent healthComponent;
    // Update is called once per frame
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<Movement>();
        healthComponent = GetComponent<HealthComponent>();
    }
    private void Update()
    {
        Attack();
        Block();
        Roll();
    }
    private void Attack()
    {
        timeSinceAttack += Time.deltaTime;
        if (playerInput.attackInput)
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
                timeSinceAttack = 0;
            }
        }
    }
    private void PerformDamage()
    {
        Collider2D[] enemyArray = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyMask);
        foreach (var enemy in enemyArray)
        {
            enemy.GetComponent<HealthComponent>().TakeDamage(Damage);
        }
    }
    private void Block()
    {
        if (playerInput.blockDownInput)
        {
            animator.SetBool("bBlock", true);
            Collider2D[] blockedArray = Physics2D.OverlapCircleAll(blockPoint.position, blockRadius, projectileMask);
            if (blockedArray.Length != 0)
            {
                animator.SetTrigger("parry");
            }
        }
        else if (playerInput.blockUpInput)
        {
            animator.SetBool("bBlock", false);
        }
    }
    private void Roll()
    {
        timeSinceRoll += Time.deltaTime;
        if (playerInput.rollInput&& timeSinceRoll>rollCooldown)
        {
            animator.SetTrigger("roll");
            playerMovement.enabled = false; 
            timeSinceRoll = 0;
            healthComponent.IFramesOn();
        }
    }
    private void EndRoll() 
    {
        healthComponent.IFramesOff();
        playerMovement.enabled = true;
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(blockPoint.position, blockRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
