
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;
    
    
    private PlayerControler playerInputAction;

    [SerializeField]private float jumpStrength = 2;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    //private PlayerGroundSensor groundCheck;
    
    void Awake()
    {
        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();
        

        playerInputAction = new PlayerControler();
        playerInputAction.GamePlay.Enable();
        
        playerInputAction.GamePlay.Jump.performed += Jump;

    }
    void Jump(InputAction.CallbackContext ctx)
    {
        if (PlayerGroundSensor.isGrounded && Time.time!=0)
        {
            rb.velocity += Vector3.up * jumpStrength *10;
            PlayerGroundSensor.isGrounded = false;
        }
        
    }
}
