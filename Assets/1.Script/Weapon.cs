using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("ȭ�� ����")]
    [Tooltip("������")]public int damage;
    [Tooltip("ȭ��")] public GameObject arrows;
    [Tooltip("���ǵ�")] public float speed;
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
        // �߷�
        float g = Physics.gravity.magnitude;

        
        float targetDistance = 20f;

        
        Vector3 shootDir = arrowTrans.forward;

        // ������ ����: �ʱ� �ӵ� ���
        float angle = 30f * Mathf.Deg2Rad; // optional, arrowTrans ���� �״�� ���� ������ ���� ����
        float initialSpeed = Mathf.Sqrt(targetDistance * g / Mathf.Sin(2 * angle));

        // ���⿡ �ʱ� �ӵ� ����
        arrowRigid.linearVelocity = shootDir.normalized * initialSpeed;
    }

   
    
}
