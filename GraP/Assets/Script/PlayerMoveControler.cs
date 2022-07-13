
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMoveControler : MonoBehaviour
{
    [SerializeField]private float speed = 5;
    [SerializeField] private float drag = 10;

    [Header("Flying")]
    [SerializeField] private float flyingSpeed = 0.5f;
    [SerializeField] private float flyingDrag = 1;

    //[Header("Running")]
    //public bool canRun = true;
    //public bool IsRunning { get; private set; }
    //public float runSpeed = 9;

    //public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rb;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    //public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();
    private PlayerInput playerInput;
    private PlayerControler playerInputAction;

    private float targetMovingSpeed;

    PlayerGroundSensor groundCheck;
    void Start()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<PlayerGroundSensor>();
        groundCheck.GroundEnter += Ground;
        groundCheck.GroundExit += Flying;
        Ground();
    }
    void Flying()
    {
        rb.drag = flyingDrag;
        targetMovingSpeed = flyingSpeed;
    }
    void Ground()
    {
        rb.drag = drag;
        targetMovingSpeed = speed;
    }
    void Awake()
    {
        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputAction = new PlayerControler();
        playerInputAction.GamePlay.Enable();
        //playerInputAction.GamePlay.Move.performed += SetMove;
       
    }

    /*void FixedUpdate()
    {
        // Update IsRunning from input.
        //IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }
        
        // Get targetVelocity from input.
        //Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        //rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }*/
    private void Update()
    {

        
        /*if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }*/
        Vector2 targetVelocity = playerInputAction.GamePlay.Move.ReadValue<Vector2>() * targetMovingSpeed;//new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rb.velocity += (transform.rotation * new Vector3(targetVelocity.x, 0, targetVelocity.y))*Time.deltaTime*30;
    }
   
}
