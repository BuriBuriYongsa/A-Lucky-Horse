using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [Header("이동 설정")]
    private Vector2 moveInput;
    [Tooltip("이동 속도")] public float speed;
    [Tooltip("회전 속도")] public float turnSpeed;
    [Header("플레이어 상태")]
    [Tooltip("체력")] public float curHp = 100;
    [Tooltip("최대체력")]public float maxHp = 100;
    [Header("아이템")]
    [Tooltip("현재 코인")]public int curCoin = 0;
    [Tooltip("분신 화살")] public GameObject[] bows;
    [Tooltip("분신 어시스트")] public GameObject[] assists;

    public bool isRunning = false;
    public bool isTouch = false;
    public bool isFloor;

    public int hpUpcnt = 0;
    public int dmgUpcnt = 0;
    public int arrowSpeedUpcnt = 0;
    public int moveUpcnt = 0;
    public int assistCnt = 0;
    public int arrowPlusCnt = 0;

    float enemydmg;
    float saveSpeed;
    public int setBowDamage;
    public int setAsisDamage;

    bool OnDmg = false;

    public AudioSource dmgAudio;
    public RandomBox randomBox;
    public GameManager gManager;
    public WeaponBack weaponBack;
    Animator anim;
    MeshRenderer[] meshs;
    Scanner scanner;
    public Weapon weapon;

    private Rigidbody rb;

    void Awake()
    {
        scanner = GetComponent<Scanner>();
        weapon = GetComponent<Weapon>();
        saveSpeed = speed;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (gManager.gameStart) Move();
        else transform.position = new Vector3(0, 1, -1);
        if (scanner.attack)
        {
            saveSpeed = speed;
            isRunning = false;
        }

    }
    public void GetInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
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
    public void OnTouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isTouch = true;
        }
        else if (context.canceled)
        {
            isTouch = false;
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

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Coin"))
        {
            Coin coin = other.GetComponent<Coin>();
            if (coin != null)
            {
                curCoin += coin.value;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            EnemySpear enemySpear = collision.gameObject.GetComponent<EnemySpear>();
   

            if (enemy != null && !OnDmg)
            {
                enemydmg = enemy.damage;
                StartCoroutine(OnDamage());
            }else if (enemySpear != null && !OnDmg)
            {
                enemydmg = enemySpear.damage;
                StartCoroutine(OnDamage());
            }
            if (curHp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.CompareTag("RandomBox") && isTouch && curCoin >= 500 && !randomBox.isOpen)
        {
            curCoin -= 500;
            randomBox.Gacha();
        }
        if (collision.gameObject.CompareTag("Floor")) isFloor = true;
        else isFloor = false;
    }
    IEnumerator OnDamage()
    {
        OnDmg = true;
        dmgAudio.Play();
        foreach (MeshRenderer mesh in meshs)
        {
            Material mat = mesh.material;
            mat.color = Color.red;
        }
        curHp -= enemydmg;
        yield return new WaitForSeconds(0.1f);

        foreach (MeshRenderer mesh in meshs)
        {
            Material mat = mesh.material;
            mat.color = Color.white;
        }
        yield return new WaitForSeconds(0.5f);
        OnDmg = false;
    }
    public void UpGrade(string str)
    {
        switch (str)
        {
            case "ArrowPlus": //활 개수 업
                if (arrowPlusCnt == 2)
                {
                    for (int i = 0; i < arrowPlusCnt; i++)
                    {
                        weaponBack = bows[i].GetComponent<WeaponBack>();
                        weaponBack.arrows.GetComponent<Arrow>().damage += 1;
                    }
                   
                }
                else
                {
                    arrowPlusCnt++;
                    bows[arrowPlusCnt].SetActive(true);
                }
                break;
            case "Assist": //어시스트 멤버 추가
                if(assistCnt == 4)
                {
                    for (int i = 0; i < assistCnt; i++)
                    {
                        weaponBack = assists[i].GetComponent<WeaponBack>();
                        weaponBack.arrows.GetComponent<Arrow>().damage += 1;
                    }
                }
                else
                {
                    assistCnt++;
                    assists[assistCnt].SetActive(true);
                }
                break;
            case "MaxHpUp"://체력 업
                if (maxHp > 500) return;
                else
                {
                    hpUpcnt++;
                    maxHp += 20f;
                }
                break;
            case "DamageUp"://데미지 업
                weapon.arrows.GetComponent<Arrow>().damage += 1;
                dmgUpcnt++;
                break;
            case "ArrowSpeedUp"://화살 속도 업
                if (weapon.speed > 50f) return;
                else
                {
                    weapon.speed += 5f;
                    arrowSpeedUpcnt++;
                }
                break;
            case "MoveUp"://이동 속도 업
                if (speed >= 5f) return;
                else
                {
                    moveUpcnt++;
                    speed += 0.3f;
                    turnSpeed += 0.3f;
                }
                break;
        }
    }
}

