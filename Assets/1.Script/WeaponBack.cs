using UnityEngine;

public class WeaponBack : MonoBehaviour
{
    [Header("화살 조정")]
    [Tooltip("화살")] public GameObject arrows;
    [Tooltip("스피드")] public float speed;

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
