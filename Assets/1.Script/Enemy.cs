using UnityEngine;

public class Enemy : MonoBehaviour

{
    public float speed;
    public Rigidbody target;

    bool isLive;

    Rigidbody rigid;
    SpriteRenderer spriter;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        spriter = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Vector3 dirVec = target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.angularVelocity = Vector3.zero;
        transform.LookAt(target.position);
    }

    
}