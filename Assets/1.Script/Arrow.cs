using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" ||
            collision.gameObject.tag == "Wood" ||
            collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

    }
}
