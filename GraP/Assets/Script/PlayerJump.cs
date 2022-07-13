
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rb;
    
    private PlayerInput playerInput;
    private PlayerControler playerInputAction;

    [SerializeField]private float jumpStrength = 2;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    private PlayerGroundSensor groundCheck;
    void Start()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<PlayerGroundSensor>();
    }
    void Awake()
    {
        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputAction = new PlayerControler();
        playerInputAction.GamePlay.Enable();
        playerInputAction.GamePlay.Jump.performed += Jump;

    }
    void Jump(InputAction.CallbackContext ctx)
    {
        if (groundCheck.isGrounded)
        {
            rb.velocity += Vector3.up * jumpStrength *10;
            groundCheck.isGrounded = false;
        }
        
    }
}
