using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    [Header("이동 설정")]
    private Vector2 moveInput;
    [Tooltip("이동 속도")] public float speed;
    [Tooltip("회전 속도")] public float turnSpeed;
    public int per;

    private float saveSpeed;
    private bool isRunning = false;
    private Animator anim;

    public Scanner scanner;
    public Weapon weapon;

    private Rigidbody rb;

    void Awake()
    {
        scanner = GetComponent<Scanner>();
        weapon = GetComponent<Weapon>();
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
        if(scanner.attack)
        {
            saveSpeed = speed;
            isRunning = false;
        }

    }
    public void GetInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("moveInput: " + moveInput);
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if(scanner.attack) return;

        if (context.started)
        {
            saveSpeed = speed * 1.5f;
            isRunning = true;
           
        }
        else if (context.canceled)
        {
            saveSpeed = speed;
            isRunning = false;
            
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
        
        if (scanner.enemyTarget != null) transform.LookAt(scanner.enemyTarget);
        else if (moveVec != Vector3.zero && scanner.enemyTarget == null) transform.LookAt(transform.position + moveVec);

       
        
    }
}
