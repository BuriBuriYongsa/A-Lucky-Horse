using UnityEngine;

public class Defense : MonoBehaviour
{
    public GameObject defPanel;
    bool isDefense = true;
    public Player player;
    public GameManager gameManager;
    Collider col;

     void Start()
    {
        col = GetComponent<Collider>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void OnCollisionStay(Collision other)
    {
        if (!gameManager.stageStart)
        {
            if (other.gameObject.CompareTag("Player"))
            {   
                if(isDefense)defPanel.SetActive(true);

                if (player.curCoin >= 500 && isDefense && player.isTouch)
                {
                    
                    player.curCoin -= 500;
                    isDefense = false;
                    col.enabled = false;
                }
                else if (!isDefense)
                {
                    col.enabled = false;
                }
            }
        }
    }
     void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            defPanel.SetActive(false);
        }
    }
    void Update()
   {
        if(gameManager.stageStart)
        {
            col.enabled = true;
        }
    }
}
