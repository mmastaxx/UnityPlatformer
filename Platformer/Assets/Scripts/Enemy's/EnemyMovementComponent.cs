using System;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;
    private float idleTimer;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Transform groundCollider;
    [SerializeField] private Transform wallCollider;
    [SerializeField] private float offsetX = 0;
    [SerializeField] private float castDistance;
    [SerializeField] private float boundColliderRadius;
    private Animator animator;
    private Vector2 leftPos;
    private Vector3 initScale;
    private bool movingLeft;
    private int direction;
    private void Awake()
    {
        initScale = transform.localScale;
        direction = Mathf.CeilToInt (transform.localScale.normalized.x);
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

    private void Move()
    {
        RaycastHit2D groundCast = Physics2D.CircleCast(groundCollider.position, boundColliderRadius, Vector2.right, 0, ground);
        RaycastHit2D wallCast = Physics2D.CircleCast(wallCollider.position, boundColliderRadius, Vector2.right, 0, ground);
        if (groundCast.collider != null && wallCast.collider == null)
        {
            MoveInDirection();
        }
        else
        {
            DirectionChange();
        }
    }

    private void DirectionChange()
    {
        animator.SetBool("bRun", false);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            movingLeft = !movingLeft;
            direction = -direction;
            transform.localScale = new Vector3(MathF.Abs(initScale.x) * direction, initScale.y, initScale.z);
        }
    }
    private void MoveInDirection()
    {
        idleTimer = 0;
        animator.SetBool("bRun", true);
        transform.position = new Vector3(transform.position.x + Time.deltaTime * direction * speed, transform.position.y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCollider.position, boundColliderRadius);
        Gizmos.DrawWireSphere(wallCollider.position, boundColliderRadius);
        Gizmos.DrawWireCube(new Vector3(transform.position.x + offsetX, transform.position.y - castDistance, transform.position.z), boxSize);
    }
}
