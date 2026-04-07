using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 8f;


    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;

    private Vector3 StartPosition;

    private Rigidbody2D rb;

   private bool jumpPressed = false;
    private bool isGrounded = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }
    public void Initialize()
    {
        StartPosition = transform.position;
        rb.simulated = true;

    }


    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameStates.InGame) return;

        CheckGrounded();
        if(transform.position.y < -10f)
        {
            GameManager.Instance.GameOver();
        }

    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState != GameStates.InGame)  return; 

        //Constant lateral movement
        Vector2 velocity = rb.linearVelocity;
        velocity.x = speed;
        rb.linearVelocity = velocity;

        if (jumpPressed && isGrounded)
        {
            Jump();
        }
        jumpPressed = false;   


    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
    private void Jump()
    {
        //Reset vertical speed for consistent jump Height
        Vector2 velocity = rb.linearVelocity;
        velocity.y = 0;
        rb.linearVelocity = velocity;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        SoundManager.Instance.PlayJumpSound();
    }
//Called automatically by PlayerInput (Send Messages) when the jump action is triggered

    public void OnJump()
    {
        jumpPressed = true;
    }

    public void Reset()
    {
        rb.linearVelocity = Vector2.zero;

        transform.position = StartPosition;
        transform.rotation = Quaternion.identity;


       
        if (rb != null) rb.simulated = false;
        jumpPressed = false;

    }
}
