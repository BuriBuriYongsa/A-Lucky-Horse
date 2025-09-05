using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("ȭ�� ����")]
    [Tooltip("ȭ��")] public GameObject arrows;
    [Tooltip("���ǵ�")] public float speed;
    public Transform arrowTrans;

    float timer;

    public WeaponBack[] back;
    void Awake()
    {
       
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
        GameObject arrow = Instantiate(arrows, arrowTrans.position, arrowTrans.rotation);
        Rigidbody arrowRigid = arrow.GetComponent<Rigidbody>();

        arrowRigid.useGravity = true;               
        arrowRigid.linearVelocity = arrowTrans.forward * speed;
        if (back != null)
        {
            for(int i = 0; i< back.Length; i++)
            {
                if (back[i].gameObject.activeSelf)
                {
                    back[i].Shot();
                }
            }
        }
    }

   
    
}
