using System;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] LayerMask ground;
    [SerializeField] float offsetX = 0;
    [SerializeField] float castDistance;

    [Header("Colliders")]
    [SerializeField] Vector2 boxSize;
    [SerializeField] Transform groundCollider;
    [SerializeField] Transform wallCollider;
    const float BoundColliderRadius = 0.1f;

    [Header("Patrol")]
    [SerializeField] float speed;
    [SerializeField] float idleDuration;

    Animator animator;
    Vector3 initScale;
    int direction;
    float idleTimer;
    private void Awake()
    {
        initScale = transform.localScale;
        direction = Mathf.CeilToInt(transform.localScale.normalized.x);
        animator = GetComponent<Animator>();
    }
    private void OnDisable()
    {
        animator.SetBool("bRun", false);
    }
    void Update()
    {
        Move();
    }

    public void Move()
    {
        RaycastHit2D groundCast = Physics2D.CircleCast(groundCollider.position, BoundColliderRadius, Vector2.right, 0, ground);
        RaycastHit2D wallCast = Physics2D.CircleCast(wallCollider.position, BoundColliderRadius, Vector2.right, 0, ground);
        if (groundCast.collider != null && wallCast.collider == null)
        {
            MoveInDirection();
        }
        else
        {
            animator.SetBool("bRun", false);
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                DirectionChange();
            }
        }
    }

    public void DirectionChange()
    {
        direction = -direction;
        transform.localScale = new Vector3(MathF.Abs(initScale.x) * direction, initScale.y, initScale.z);
        boxSize.Scale(new Vector2(-1f, 1f));
        offsetX *= -1;
    }
    private void MoveInDirection()
    {
        idleTimer = 0;
        animator.SetBool("bRun", true);
        transform.position = new Vector3(transform.position.x + Time.deltaTime * direction * speed, transform.position.y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCollider.position, BoundColliderRadius);
        Gizmos.DrawWireSphere(wallCollider.position, BoundColliderRadius);
        Gizmos.DrawWireCube(new Vector3(transform.position.x + offsetX, transform.position.y - castDistance, transform.position.z), boxSize);
    }
}
