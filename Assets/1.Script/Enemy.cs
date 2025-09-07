using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour

{
    [Header("적 세팅")]
    public float speed;
    public float hp;
    public float damage;
    public Transform target;
    public float attackDis;
    [Tooltip("Yellow 코인")] public GameObject coinY;
    [Tooltip("Silver 코인")] public GameObject coinS;

    [Tooltip(" 몬스터별 Coin 확률")] public int coinDrop;
    [Tooltip(" YellowCoin 확률")]public int ramdomPro;

    public int speedCnt = 0;
    public int damageCnt = 0;
    public int maxHpCnt = 0;

    private NavMeshAgent agent;
    private Animator anim;
    private float distance;
    private bool isDed;


    Arrow arrow;
    public EnemySpear enemySpear;

    void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemySpear = GetComponent<EnemySpear>();
    }

    private void OnTriggerEnter(Collider other)
    {
   
        arrow = other.GetComponent<Arrow>();
        Vector3 kcockBack = transform.position - other.transform.position;
        if (other.CompareTag("Arrow"))
        {
            hp -= arrow.damage;
            StartCoroutine(KnockbackCoroutine(kcockBack, arrow.knockB, 0.1f));
            Debug.Log("Enemy HP: " + hp);
            if (hp <= 0)
            {
                int cRand = Random.Range(1, coinDrop);
                for (int i=1; i<=cRand; i++)
                {
                    int yRand = Random.Range(1, 100);
                    Vector3 randomVector = new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
                    if (yRand <= ramdomPro)
                    {
                        GameObject coin = Instantiate(coinY, transform.position + randomVector, Quaternion.identity);
                    }
                    else
                    {
                        GameObject coin = Instantiate(coinS, transform.position + randomVector, Quaternion.identity);
                    }
                }
                isDed = true;
                agent.isStopped = true;
                GetComponent<BoxCollider>().enabled = false;
                anim.SetBool("isLive", false);
                Destroy(gameObject , 1);     
            }
        }
    }

    void Update()
    {
        if (isDed) return;
        
        distance = Vector3.Distance(agent.transform.position, target.position);
        if((distance <= attackDis))
        {
            agent.isStopped = true;
            anim.SetBool("isAttack", true);
            transform.LookAt(target);
        }
        else
        {
            agent.isStopped = false;
            anim.SetBool("isAttack", false);
            agent.destination = target.position;
            agent.speed = speed;
        }
    }
    IEnumerator KnockbackCoroutine(Vector3 dir, float distance, float duration)
    {         
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = startPos + dir.normalized * distance;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 nextPos = Vector3.Lerp(startPos, endPos, elapsed / duration);
            Vector3 moveDelta = nextPos - agent.transform.position;

            agent.Move(moveDelta);  // NavMeshAgent 이동
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    

}