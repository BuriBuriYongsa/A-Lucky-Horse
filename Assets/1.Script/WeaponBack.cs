using UnityEngine;

public class WeaponBack : MonoBehaviour
{
    [Header("화살 조정")]
    [Tooltip("화살")] public GameObject arrows;
    [Tooltip("스피드")] public float speed;
    [Tooltip("타이머")] public float shotTimer;
    public Transform arrowTrans;


    private float timer = 0;
    Scanner scanner;



    void Awake()
    {
       scanner = GetComponentInParent<Scanner>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (scanner.attack)
        {
            transform.LookAt(scanner.enemyTarget);
            if (timer >= shotTimer)
            {
                Shot();
                timer = 0;
            }
            timer += Time.deltaTime;
        }
        else
        {
            timer = shotTimer;
        }
    }


    public void Shot()
    {
        GameObject arrow = Instantiate(arrows, arrowTrans.position, arrowTrans.rotation);
        Rigidbody arrowRigid = arrow.GetComponent<Rigidbody>();

        arrowRigid.useGravity = true;
        arrowRigid.linearVelocity = arrowTrans.forward * speed;
    }

}
