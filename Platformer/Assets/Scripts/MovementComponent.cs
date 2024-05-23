using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    [SerializeField] private float airSpeed = 5;
    [SerializeField] private float groundSpeed = 10;
    public float speed;
    [SerializeField] private float jumpHeight = 3;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;
    private float horizontalInput;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = groundSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);
        if(horizontalInput > 0.01)
            transform.localScale = Vector2.one;
        else if(horizontalInput < -0.01) 
            transform.localScale = new Vector2(-1, transform.localScale.y);
        isGrounded();
        if (Input.GetKey(KeyCode.Space)&& isGrounded())
            Jump();
        animator.SetBool("bRun", horizontalInput != 0);
        speed = groundSpeed;
    }
    void Jump() 
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        animator.SetBool("bGrounded",false);
    }
    public bool isGrounded() 
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)) 
        {
            animator.SetBool("bGrounded", true);
            animator.SetBool("bFalling", false);
            speed = groundSpeed;
            return true;
        }
        animator.SetBool("bGrounded", false);
        animator.SetBool("bFalling", true);
        speed = airSpeed;
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(transform.position.x-0.07f, transform.position.y, transform.position.z)-transform.up*castDistance,boxSize);
    }
}
