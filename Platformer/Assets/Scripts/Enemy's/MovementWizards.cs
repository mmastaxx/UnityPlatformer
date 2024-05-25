using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovementWizards : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;
    private float idleTimer;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    private Animator animator;
    private Vector2 leftPos;
    private Vector2 rightPos;
    private Vector3 initScale;
    private bool movingLeft;
    private void Awake()
    {
        initScale = transform.localScale;
        animator = GetComponent<Animator>();
        RaycastHit2D res = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, ground);
        if (res.collider != null) 
        { 
            leftPos = new Vector2(res.collider.bounds.center.x - res.collider.bounds.size.x / 2, transform.position.y);
            rightPos = new Vector2(res.collider.bounds.center.x + res.collider.bounds.size.x / 2, transform.position.y);
        }
    }
    private void OnDisable()
    {
        animator.SetBool("bRun", false);
    }
    void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x >= leftPos.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (transform.position.x <= rightPos.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }
  
    private void DirectionChange()
    {
        animator.SetBool("bRun", false);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
            movingLeft = !movingLeft;
    }
    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        animator.SetBool("bRun", true);
        transform.localScale = new Vector3(MathF.Abs(initScale.x) * direction, initScale.y, initScale.z);
        transform.position = new Vector3(transform.position.x + Time.deltaTime * direction * speed, transform.position.y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(leftPos, 0.1f);
        Gizmos.DrawWireSphere(rightPos, 0.1f);
        Gizmos.DrawWireCube(new Vector3(transform.position.x - 0.07f, transform.position.y, transform.position.z) - transform.up * castDistance, boxSize);
    }
}
