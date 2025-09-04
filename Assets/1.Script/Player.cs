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
    [Header("플레이어 상태")]
    [Tooltip("체력")] public float curHp = 100;

    public float maxHp = 100;
    public int curCoin = 0;
  
    public bool isRunning = false;

    float enemydmg;
    float saveSpeed;
    bool OnDmg = false;

    Animator anim;
    MeshRenderer[] meshs;
    Scanner scanner;
    Weapon weapon;

    private Rigidbody rb;

    void Awake()
    {
        scanner = GetComponent<Scanner>();
        weapon = GetComponent<Weapon>();
        saveSpeed = speed;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        if (anim == null)
        {
            Debug.LogWarning("Animator component not found on " + gameObject.name);
        }
    }

    void Start()
    {
        
    }
    
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
    void Move()
    {
        Vector3 moveVec = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        transform.position += moveVec * saveSpeed * Time.fixedDeltaTime;

        anim.SetBool("isWalk", moveVec != Vector3.zero && !isRunning);
        anim.SetBool("isRun", isRunning && moveVec != Vector3.zero);

        if (moveVec != Vector3.zero) anim.SetBool("isWalk", true);
        else if (isRunning) anim.SetBool("isRun", false);
        
        if (scanner.enemyTarget != null && !isRunning) transform.LookAt(scanner.enemyTarget);
        else if (moveVec != Vector3.zero) transform.LookAt(transform.position + moveVec);

    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !OnDmg)
            {
                enemydmg = enemy.damage;
                StartCoroutine(OnDamage());
            }
            if (curHp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        if(other.CompareTag("Coin"))
        {
            Coin coin = other.GetComponent<Coin>();
            if (coin != null)
            {
                curCoin += coin.value;
            }
        }
    }
    IEnumerator OnDamage()
    {
        OnDmg = true;
        foreach(MeshRenderer mesh in meshs)
        {
            Material mat = mesh.material;
            mat.color = Color.red;
        }
        curHp -= enemydmg;
        Debug.Log(curHp);
        yield return new WaitForSeconds(0.1f);

        foreach (MeshRenderer mesh in meshs)
        {
            Material mat = mesh.material;
            mat.color = Color.white;
        }
        yield return new WaitForSeconds(1f);
        OnDmg = false;
    }
   
}

