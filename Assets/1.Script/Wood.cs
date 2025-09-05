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
            if(!gManager.stageStart)woodPanel.SetActive(true);
            if (player.curCoin >= 200 && player.isTouch && !gManager.stageStart)
            {
                player.curCoin -= 200;
                Destroy(gameObject);
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
