using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rigidBody; // 2D Object body
    public float movingSpeed; // Objects moving speed
    public float jumpingForce; // Objects jumping power
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float rememberGroundedFor;
    float lastTimeGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // Setting body component
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        OnGround();
    }

    /// <summary>
    /// Movement function, that's called upon every frame.
    /// </summary>
    void Move()
    {
        // Horizontal movement (setting A - left or -1, D - right or 1, no input - 0)
        float xAxis = Input.GetAxisRaw("Horizontal");
        // Calculating moving distance = movement axis * moving speed
        float moveBy = xAxis * movingSpeed; 
        // Accessing rigibody velocity variable
        rigidBody.velocity = new Vector2(moveBy, rigidBody.velocity.y); 
    }

    /// <summary>
    /// Jumping function, thats called upon pressing button
    /// </summary>
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingForce);
        }
    }

    /// <summary>
    /// Function checks if the player body is near the ground
    /// </summary>
    void OnGround()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, 
            checkGroundRadius, groundLayer);
        if (colliders != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }
}
