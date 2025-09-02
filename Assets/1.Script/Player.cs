using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [Header("이동 설정")]
    private float currentSpeed = 0;
    private Vector2 moveInput;
    [Tooltip("이동 속도")] public float speed;
    [Tooltip("회전 속도")] public float turnSpeed;

    private float saveSpeed;
    private bool isRunning = false;
    private Animator anim;


    private Rigidbody rb;

    void Awake()
    {
        saveSpeed = speed;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        if (anim == null)
        {
            Debug.LogWarning("Animator component not found on " + gameObject.name);
        }
    }

    void Start()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void GetInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("moveInput: " + moveInput);
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            saveSpeed = speed * 1.3f;
            isRunning = true;
            Debug.Log("isRunning: " + isRunning);
        }
        else if (context.canceled)
        {
            saveSpeed = speed;
            isRunning = false;
            Debug.Log("isRunning: " + isRunning);
        }
    }
    void FootR() { }
    void FootL() { }
    void Move()
    {
        Vector3 moveVec = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        transform.position += moveVec * saveSpeed * Time.fixedDeltaTime;

        anim.SetBool("isWalk", moveVec != Vector3.zero && !isRunning);
        anim.SetBool("isRun", isRunning && moveVec != Vector3.zero);

        if (moveVec != Vector3.zero) anim.SetBool("isWalk", true);
        else if (isRunning) anim.SetBool("isRun", false);
        transform.LookAt(transform.position + moveVec);

        /*rb.MovePosition(rb.position + transform.TransformDirection(move));

        float turn = moveInput.x * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);/*

        /*if (moveInput.y == 0 && moveInput.x == 0) currentSpeed = 0;
        else currentSpeed = moveInput.y * speed;

        Vector3 move = transform.forward * (currentSpeed * Time.fixedDeltaTime);
        
        rb.MovePosition(rb.position + move);

        float turn = moveInput.x * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);*/
    }
}
