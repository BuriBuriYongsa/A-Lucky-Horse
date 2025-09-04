using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject gamePanel;
    public Text coinTxt;
    public Text enemyTxt;
    public RectTransform hpImg;

    public static int enemy; 

    public Player player;
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    void LateUpdate()
    {
        coinTxt.text = player.curCoin.ToString();

        enemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyTxt.text = (enemy/2).ToString();
        
        hpImg.localScale = new Vector3(player.curHp / player.maxHp, 1, 1);
    }
}
