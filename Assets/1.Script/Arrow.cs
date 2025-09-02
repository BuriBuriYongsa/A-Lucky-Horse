using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        
        if(per > -1)
        {
            rigid.angularVelocity = dir;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") || per == -1)
            return;

        per--;

        if(per == -1)
        {
            rigid.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
