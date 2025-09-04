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

    private NavMeshAgent agent;
    private Animator anim;
    private float distance;
    private bool isDed;

    Arrow arrow;

    void Awake()
    {
       
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
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
                isDed = true;
                agent.isStopped = true;
                GetComponent<BoxCollider>().enabled = false;
                GameObject coinDrop = Instantiate(coinY, transform.position, Quaternion.identity);
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