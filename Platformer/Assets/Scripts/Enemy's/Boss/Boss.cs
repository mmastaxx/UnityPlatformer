using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float damage = 10; 
    Transform player;
    public Transform attackPos;
    public Vector2 range;
   
    bool isFlipped = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void LookAtPlayer() 
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped) 
        {
            transform.localScale = flipped; 
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    public void PerformDamage()
    {
        RaycastHit2D hit = Physics2D.CircleCast(attackPos.position, range.x, Vector2.right * transform.localScale.normalized, 0, playerLayer);
        if (hit.collider)
        {
            hit.collider.GetComponent<HealthComponent>().TakeDamage(damage);
        }
    }
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(attackPos.position.x, attackPos.position.y), range.x);
    }
}
