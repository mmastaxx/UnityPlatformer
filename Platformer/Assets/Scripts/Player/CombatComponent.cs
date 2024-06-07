using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatComponent : MonoBehaviour
{
    [SerializeField] float Damage;

    [Header("Attack")]
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float attackRadius;

    [Header("Block")]
    [SerializeField] Transform blockPoint;
    [SerializeField] LayerMask projectileMask;
    [SerializeField] float blockRadius;

    [Header("Cooldowns")]
    [SerializeField] float attackCooldown = 0.1f;
    [SerializeField] float rollCooldown = 0.5f;
    [SerializeField] float comboCooldown = 1;

    int attackIndex = 1;
    float timeSinceAttack = Mathf.Infinity;
    float timeSinceRoll = Mathf.Infinity;
    bool isRoll=  false;
    Animator animator;
    PlayerInput playerInput;
    Movement playerMovement;
    HealthComponent healthComponent;
    Rigidbody2D body;
    AudioManager audioManager;
    // Update is called once per frame
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<Movement>();
        healthComponent = GetComponent<HealthComponent>();
        body = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
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
                timeSinceAttack = 0;
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
        if (playerInput.blockDownInput&&animator.GetBool("bGrounded"))
        {
            animator.SetBool("bBlock", true);
            body.velocity = Vector3.zero;
            playerMovement.enabled = false;
            Collider2D[] blockedArray = Physics2D.OverlapCircleAll(blockPoint.position, blockRadius, projectileMask);
            if (blockedArray.Length != 0)
            {
                animator.SetTrigger("parry");
            }
        }
        else if (playerInput.blockUpInput)
        {
            animator.SetBool("bBlock", false);
            playerMovement.enabled = true;
            playerMovement.speed = 6;
        }
    }
    private void Roll()
    {
        timeSinceRoll += Time.deltaTime;
        if (playerInput.rollInput && timeSinceRoll > rollCooldown)
        {
            animator.SetTrigger("roll");
            isRoll = true;
            playerMovement.enabled = false;
            timeSinceRoll = 0;
            healthComponent.IFramesOn();
        }
        if (isRoll)
        {
            body.velocity = new Vector2(transform.localScale.normalized.x * playerMovement.speed, body.velocity.y);
        }
        if (!playerMovement.isGrounded())
            animator.SetBool("bFalling", false);
    }
    private void EndRoll()
    {
        isRoll = false;
        playerMovement.enabled = true;
        healthComponent.IFramesOff();
        if (!playerMovement.isGrounded())
            animator.SetBool("bFalling", true);
    }
    private void AttackStart()
    {
        ++attackIndex;
        audioManager.Play("attack" + (attackIndex-1));
        
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(blockPoint.position, blockRadius);
    //    Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    //}

}
