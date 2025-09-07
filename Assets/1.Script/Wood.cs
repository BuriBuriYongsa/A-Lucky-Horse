using UnityEngine;

public class Wood : MonoBehaviour
{
    public GameObject woodPanel;

    public Player player;
    public GameManager gManager;
    void Awake()
    {
        woodPanel.SetActive(false);
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(gManager.stageClear)woodPanel.SetActive(true);
            if (player.curCoin >= 200 && player.isTouch && gManager.stageClear)
            {
                player.curCoin -= 200;
                Destroy(gameObject);
                woodPanel.SetActive(false);
            }
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            woodPanel.SetActive(false);
        }
    }
}
