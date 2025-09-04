using UnityEngine;
using System.Collections;

public class ScannerBackup : MonoBehaviour
{

    [Header("공격 설정")]
    public float attackRange;
    public LayerMask enemyLayer;
    public Collider[] enemys;
    public Transform enemyTarget;

    private Transform nextEnemyTarget;
    public bool attack;
    private Animator anim;

    Player player;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Update is called once per frame
    void Update()
    {
        enemys = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        enemyTarget = GetNearestEnemy();
        
        if (enemyTarget != null)
        {
            anim.SetBool("isAttack", true);
            transform.LookAt(enemyTarget);
            attack = true;
        }
        else
        {
            attack = false;
            anim.SetBool("isAttack", false);
        }
    }
    

    Transform GetNearestEnemy()
    {
        Transform result = null;
        float diff = 100f;

        foreach (Collider enemy in enemys)
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
