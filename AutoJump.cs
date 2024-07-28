using UnityEngine;

public class AutoJump : MonoBehaviour
{
    [SerializeField] public Animator anim;

    private Rigidbody rb;
    
    public float jumpForce = 4f; // If your game is Hyper Casual, I recommend setting jumpForce to 4.4f.
    public float groundCheckRadius = 0.2f; // If you are doing hyper casual gaming, it would be best to make this value 0.1f.
    public Transform groundCheck; // Put this right under the object.
    public LayerMask groundLayer;
    
    private bool hasJumped;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {        
        CheckGround();
    }
    
    private void CheckGround()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        
        if (!isGrounded && !hasJumped)
        {
            Jump();
        }
    }

    private void Jump()
    {
        anim.SetBool("Jump", true); // Jump animation
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        hasJumped = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            hasJumped = false;
            anim.SetBool("Jump", false); // Jump animation
        }
    }
}