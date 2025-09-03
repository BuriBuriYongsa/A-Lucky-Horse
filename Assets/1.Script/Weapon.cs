using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("화살 조정")]
    [Tooltip("데미지")]public int damage;
    [Tooltip("화살")] public GameObject arrows;
    [Tooltip("스피드")] public float speed;
    public Transform arrowTrans;

    float timer;

    public Player player;
    

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
     
    }
 

    public void Shot()
    {
        Debug.Log("Shot");
        GameObject intantArrow = Instantiate(arrows, arrowTrans.position, arrowTrans.rotation);
        Rigidbody arrowRigid = intantArrow.GetComponent<Rigidbody>();
        // 중력
        float g = Physics.gravity.magnitude;

        
        float targetDistance = 20f;

        
        Vector3 shootDir = arrowTrans.forward;

        // 포물선 공식: 초기 속도 계산
        float angle = 30f * Mathf.Deg2Rad; // optional, arrowTrans 방향 그대로 쓰고 싶으면 생략 가능
        float initialSpeed = Mathf.Sqrt(targetDistance * g / Mathf.Sin(2 * angle));

        // 방향에 초기 속도 적용
        arrowRigid.linearVelocity = shootDir.normalized * initialSpeed;
    }

   
    
}
