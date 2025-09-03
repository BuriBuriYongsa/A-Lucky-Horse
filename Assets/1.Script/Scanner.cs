using UnityEngine;
using System.Collections;

public class Scanner : MonoBehaviour
{

    [Header("공격 설정")]
    public Transform bullet;
    public float attackRange;
    public LayerMask enemyLayer;
    public RaycastHit[] enemysHit;
    public Transform enemyTarget;

    public bool attack;
    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemysHit = Physics.SphereCastAll(transform.position, attackRange, Vector3.forward, 0, enemyLayer);
        enemyTarget = GetNearestEnemy();
        if (enemyTarget != null)
        {
            anim.SetBool("isAttack", true);
            attack = true;
        }
        else
        {
            attack = false;
            anim.SetBool("isAttack", false);
        }
    }
    public void Hit()
    {
        // 공격 타이밍에 맞춰 데미지 주기, 투사체 발사 등 원하는 동작
        Debug.Log("Hit! " + enemyTarget);

        if (!enemyTarget) return;

        bullet.position = transform.position;
        bullet.LookAt(enemyTarget);
    }
    

    Transform GetNearestEnemy()
    {
        Transform result = null;
        float diff = 100f;

        foreach (RaycastHit enemy in enemysHit)
        {
            Vector3 myPos = transform.position;
            Vector3 enemyPos = enemy.transform.position;
            float curDiff = Vector3.Distance(myPos, enemyPos);
            if(curDiff < diff)
            {
                diff = curDiff;
                result = enemy.transform;
            }
        }
        return result;
    }
}
