using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 movementInput;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D box;
    public Animator anim;
    private SoundManager sound;
    private bool jumped;
    private bool sprinted;
    
    [Header("Vertical Movement")]
    public float jumpForce = 16f;
    public float gravity = 2f;
    public float fallMultiplier = 5f;
    public float jumpTimer;
    public float jumpDelay = 0.25f;
    [SerializeField] private float extraHeight = 0.1f;
    [SerializeField] private LayerMask platformMask;

    [Header("Horizontal Movement")]
    public float baseSpeed = 20f;
    [SerializeField] private float smoothTime = 1f;
    [SerializeField] private float smoothTimeGrounded = 0.3f;
    [SerializeField] private float acceleration = 1.5f;

    private float yvelocity;
    private float xvelocity;
    private float finalSpeed;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        sound = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }
    
    void FixedUpdate()
    {
        ApplyMovement();
        ApplyAnimation();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        CheckDirection();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
        
    }

    public void OnSprintInput(InputAction.CallbackContext context)
    {
        sprinted = context.action.triggered;
    }

    private void ApplyMovement()
    {
        if (rb.velocity.y < 0.1f)
        {
            Jump();
        }
        
        Sprint();        
        
        yvelocity = rb.velocity.y;
        xvelocity = rb.velocity.x;

        velocity.x += movementInput.x * acceleration; // velocity buildup on the horizontal axis

        if (movementInput.x == 0) // if there's no input on the horizontal axis
        {
            if (isGrounded()) {
                velocity.x = Mathf.SmoothDamp (velocity.x, 0f, ref xvelocity, smoothTimeGrounded);
            }
            else {
                velocity.x = Mathf.SmoothDamp (velocity.x, 0f, ref xvelocity, smoothTime);
            }
        }

        velocity = Vector2.ClampMagnitude (velocity, baseSpeed); // clamp the speed so the character doesn't accelerate forever

        rb.velocity = new Vector2(velocity.x * finalSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void CheckDirection()
    {
        if (movementInput.x > 0f)
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        else if (movementInput.x < 0f)
        {
            transform.rotation = new Quaternion(0f, -180f, 0f, 0f);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, extraHeight, platformMask);
        return raycastHit.collider != null;
    }

    private bool isGrounded2()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, extraHeight * 1.5f, platformMask);
        return raycastHit.collider != null;
    }

    private bool isWalledRight()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(new Vector2(box.bounds.center.x, box.bounds.center.y + 0.079f), new Vector2(box.bounds.size.x, 0.05f), 0f, Vector2.right, extraHeight /2, platformMask);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private bool isWalledLeft()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(new Vector2(box.bounds.center.x, box.bounds.center.y + 0.079f), new Vector2(box.bounds.size.x, 0.05f), 0f, Vector2.left, extraHeight /2, platformMask);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private bool isWalledAbove()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(new Vector2(box.bounds.center.x, box.bounds.center.y + 0.31f), new Vector2(box.bounds.size.x, 0.1f), 0f, Vector2.up, extraHeight, platformMask);
        return raycastHit.collider != null;
    }

    private void ApplyAnimation()
    {
        if (rb.velocity.y > 0.1f && isWalledAbove() == false && isGrounded() == false)
        {
            anim.SetBool("Jump", true);
            
        }
        else if (rb.velocity.x < -0.1f)
        {
            anim.SetBool("isRunning", true);
            sound.PlayerSound("walk");
        }
        else if (rb.velocity.x > 0.1f)
        {
            anim.SetBool("isRunning", true);
            sound.PlayerSound("walk");
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (rb.velocity.y < -0.1f)
        {
            anim.SetBool("Jump", false);
        }

        if (isWalledRight() && rb.velocity.x > 0.1f)
        {
            anim.SetBool("Squished", true);
        }
        else if (isWalledLeft() && rb.velocity.x < -0.1f)
        {
            anim.SetBool("Squished", true);
        }
        else 
        {
            anim.SetBool("Squished", false);
        }

        if (rb.velocity.y < -0.01f && isGrounded2() && isWalledAbove() == false)
        {
            anim.SetBool("Landed", true);
            anim.SetBool("isGrounded", false);
            sound.PlayerSound("landing");
        }
        else if (isGrounded())
        {
            anim.SetBool("isGrounded", true);
            anim.SetBool("Landed", false);

        }
        else
        {
            anim.SetBool("Landed", false);
            anim.SetBool("isGrounded", false);
        }
    }

    private void Jump()
    {
        if (jumped && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            sound.PlayerSound("jump");
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravity * fallMultiplier;
        }
        else if (rb.velocity.y > 0)
        {
            rb.gravityScale = gravity * (fallMultiplier / 2);
        }
    }

    private void Sprint()
    {
        if (sprinted && isGrounded())
        {
            finalSpeed = baseSpeed * 1.37f;
        } 
        else 
        {
            finalSpeed = baseSpeed;
        }
    }
}
