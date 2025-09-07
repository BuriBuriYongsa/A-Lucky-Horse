using UnityEngine;

public class WeaponBack : MonoBehaviour
{
    [Header("ȭ�� ����")]
    [Tooltip("ȭ��")] public GameObject arrows;
    [Tooltip("���ǵ�")] public float speed;

    public Transform arrowTrans;

    Scanner scanner;



    void Awake()
    {
       scanner = GetComponentInParent<Scanner>();
    }


    public void Shot()
    {
        GameObject arrow = Instantiate(arrows, arrowTrans.position, arrowTrans.rotation);
        Rigidbody arrowRigid = arrow.GetComponent<Rigidbody>();

        arrowRigid.useGravity = true;
        arrowRigid.linearVelocity = arrowTrans.forward * speed;
    }

}
