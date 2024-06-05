using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rigidbody;
    [SerializeField] float speed;
    [SerializeField] float attackCooldown;
    Animator animator;
    Boss boss;
    float cooldownTimer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        this.animator = animator;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cooldownTimer += Time.deltaTime;
        Vector2 target = new Vector2(player.position.x, rigidbody.position.y);
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, speed * Time.deltaTime); 
        rigidbody.MovePosition(newPos);
        boss.LookAtPlayer();
        Attack();
    }

    void Attack()
    {
        if (Vector2.Distance(player.position, rigidbody.position) <= boss.range.x * 2 && cooldownTimer >= attackCooldown)
        {
            animator.SetTrigger("attack" + Random.Range(1, 4));
            cooldownTimer = 0;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
        animator.ResetTrigger("attack3");
        cooldownTimer = 0;
    }

}
