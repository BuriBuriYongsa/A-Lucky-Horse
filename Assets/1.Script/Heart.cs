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
                if(player.curHp >= 100)return;
                
                heartPanel.SetActive(true);
                int upHp = player.maxHp / 100 * 10;
                StartCoroutine(HpUp(upHp));
            }
           
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player") heartPanel.SetActive(false);
    }

    IEnumerator HpUp(int upHp)
    {
        player.curCoin -= 50;
        player.curHp += upHp;
        if(player.curHp > 100) player.curHp = 100; 
        yield WaitForSeconds(0.5f);
    }

}
