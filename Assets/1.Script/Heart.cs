using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Heart : MonoBehaviour
{
    public Player player;
    public GameObject heartPanel;
    public bool hpUping = false;

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(hpUping)return;
            heartPanel.SetActive(true);
            if (player.curCoin >= 50 && player.isTouch)
            {
                if(player.curHp >= 100)return;
                
                heartPanel.SetActive(true);
                float upHp = player.maxHp / 100 * 10;
                StartCoroutine(HpUp(upHp));
            }
           
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player") heartPanel.SetActive(false);
    }

    IEnumerator HpUp(float upHp)
    {
        player.curCoin -= 50;
        player.curHp += upHp;
        if(player.curHp > 100) player.curHp = 100;
        hpUping = true;
        yield return new WaitForSeconds(1f);
        hpUping = false;
    }

}
