using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Tooltip("������")] public int damage;
    [Tooltip("�˹� ����")] public float knockB;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor") ||
            other.CompareTag("Wood"))
        {
            GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject,2);
        }
        else if(other.CompareTag("Enemy"))
        {
            GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject);
        }

    }
}
