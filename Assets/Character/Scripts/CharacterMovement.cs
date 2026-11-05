using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{

    public Rigidbody rb;
    public Animator animatorMc;

    
    public InputActionProperty moveAction;
    
    public InputActionProperty runAction;

    
    [SerializeField] float _walkSpeed = 3f;
    [SerializeField] float _runSpeed = 8f;
    [SerializeField] float animationSmoothSpeed = 6f; 
    private float normalWSpeed;
    private float normalRSpeed;

    
    [SerializeField] float groundCheckDistance = 0.5f;
    [SerializeField] LayerMask groundMask;

    
    public bool canMove = true;
    [SerializeField] private bool canRun = true;

    
    private Vector2 rawInput;
    private float currentInputX;
    private float currentInputY;
    private Vector3 moveInput;
    private float currentSpeed;
    private Vector3 groundNormal = Vector3.up;
    private bool isGrounded;
    private bool isRunningInput;

    private void Awake()
    {
        normalWSpeed = _walkSpeed;
        normalRSpeed = _runSpeed;
    }

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (animatorMc == null) animatorMc = GetComponent<Animator>();

        
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    void OnEnable()
    {
        moveAction.action?.Enable();
        runAction.action?.Enable();
    }

    void OnDisable()
    {
        moveAction.action?.Disable();
        runAction.action?.Disable();
    }

    void Update()
    {
        CheckGround();

        if (!canMove)
        {
            StopMovementAndAnimation();
            return;
        }

        
        rawInput = moveAction.action != null ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;
        isRunningInput = runAction.action != null ? runAction.action.IsPressed() : false;

        
        Vector3 inputDir = new Vector3(rawInput.x, 0f, rawInput.y);
        if (inputDir.sqrMagnitude > 1f)
            inputDir.Normalize();

        moveInput = inputDir;

        
        bool hasInput = moveInput.sqrMagnitude > 0.01f;
        bool isRunning = isRunningInput && canRun && hasInput;

        currentSpeed = isRunning ? _runSpeed : _walkSpeed;

        
        float targetX = isRunning ? rawInput.x * (_runSpeed / _walkSpeed) : rawInput.x;
        float targetY = isRunning ? rawInput.y * (_runSpeed / _walkSpeed) : rawInput.y;

        currentInputX = Mathf.MoveTowards(currentInputX, hasInput ? targetX : 0f, animationSmoothSpeed * Time.deltaTime);
        currentInputY = Mathf.MoveTowards(currentInputY, hasInput ? targetY : 0f, animationSmoothSpeed * Time.deltaTime);

        
        animatorMc.SetFloat("X", currentInputX);
        animatorMc.SetFloat("Y", currentInputY);
        animatorMc.SetBool("Walk", hasInput && !isRunning);
        animatorMc.SetBool("Run", isRunning);
        animatorMc.SetBool("CancelAttack", hasInput);
    }

    void FixedUpdate()
    {
        if (!canMove || moveInput == Vector3.zero)
            return;

        
        Vector3 worldDir = transform.TransformDirection(moveInput);
        Vector3 slopeDir = Vector3.ProjectOnPlane(worldDir, groundNormal).normalized;

        rb.MovePosition(rb.position + slopeDir * currentSpeed * Time.fixedDeltaTime);
    }

    void CheckGround()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down,
            out RaycastHit hit, groundCheckDistance, groundMask))
        {
            isGrounded = true;
            groundNormal = hit.normal;
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector3.up;
        }
    }

    private void StopMovementAndAnimation()
    {
        moveInput = Vector3.zero;

        
        currentInputX = Mathf.MoveTowards(currentInputX, 0f, animationSmoothSpeed * Time.deltaTime);
        currentInputY = Mathf.MoveTowards(currentInputY, 0f, animationSmoothSpeed * Time.deltaTime);

        animatorMc.SetFloat("X", currentInputX);
        animatorMc.SetFloat("Y", currentInputY);
        animatorMc.SetBool("Walk", false);
        animatorMc.SetBool("Run", false);
        animatorMc.SetBool("CancelAttack", false);
    }

    
    public void SpeedBuffMc()
    {
        _walkSpeed = 6f;
        _runSpeed = 12f;
    }

    public void SpeedNormal()
    {
        _walkSpeed = normalWSpeed;
        _runSpeed = normalRSpeed;
    }
}
