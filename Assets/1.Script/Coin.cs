using UnityEngine;

public class Coin : MonoBehaviour
{
    [Tooltip("���� ��ġ")]public int value;
    
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
