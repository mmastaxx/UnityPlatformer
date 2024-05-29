using UnityEngine;
[RequireComponent(typeof(PlayerInput))]
public class Movement : MonoBehaviour
{
    
    [SerializeField] private float airSpeed = 5;
    [SerializeField] private float groundSpeed = 10;
    public float speed;
    [SerializeField] private float jumpHeight = 3;
    [SerializeField] private float castDistance;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator animator;
    private PlayerInput playerInput;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        speed = groundSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        isGrounded();
        Jump();
    }

    private void Move()
    {
        if (isGrounded())
            body.velocity = new Vector2(playerInput.movementInput.x * groundSpeed, body.velocity.y) ;
        else
            body.velocity = new Vector2(playerInput.movementInput.x * airSpeed, body.velocity.y);

        if (playerInput.movementInput.x > 0.01)
            transform.localScale = Vector2.one;
        else if (playerInput.movementInput.x < -0.01)
            transform.localScale = new Vector2(-1, transform.localScale.y);

        animator.SetBool("bRun", playerInput.movementInput.x != 0);
    }

    void Jump() 
    {
        if ((playerInput.jumpInput) && isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            animator.SetBool("bGrounded", false);
        }
    }
    public bool isGrounded() 
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)) 
        {
            animator.SetBool("bGrounded", true);
            animator.SetBool("bFalling", false);
            return true;
        }
        animator.SetBool("bGrounded", false);
        animator.SetBool("bFalling", true);
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(transform.position.x-0.07f, transform.position.y, transform.position.z)-transform.up*castDistance,boxSize);
    }
}
