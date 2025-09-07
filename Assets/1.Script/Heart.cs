using UnityEngine;

public class Heart : MonoBehaviour
{
    public Player player;
    public GameObject heartPanel;

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            heartPanel.SetActive(true);
            if (player.curCoin >= 50 && player.isTouch)
            {
                heartPanel.SetActive(true);
                player.curHp += 10;
                if(player.curHp >= 100)player.curHp = 100;
            }
           
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player") heartPanel.SetActive(false);
    }


}
