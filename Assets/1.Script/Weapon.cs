using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && collision.gameObject.tag == "Wood")
        {
            Destroy(gameObject, 2);
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
