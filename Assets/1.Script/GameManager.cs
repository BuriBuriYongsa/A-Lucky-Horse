using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject gamePanel;
    public GameObject stagePanel;
    public Text coinTxt;
    public Text enemyTxt;
    public RectTransform hpImg;
    public Text maxHp;
    public Text arrowSpeedUp;
    public Text arrowDamageUp;
    public Text moveSpeedUp;
    public Text arrowPlus;
    public Text assistPlus;
    public Text enemyMaxHp;
    public Text enemySpeedUp;
    public Text enemyDmgUp;
    public Text enemySpawnUp;

    [Tooltip("디펜스 오브젝트")] public GameObject defenses;

    public bool gameStart;
    public bool stageStart;
    public int stageNum = 0;

    public static int enemys; 

    public Player player;
    public EnemySpawn enemySpawn;
    private void Awake()
    {
        mainPanel.SetActive(true);
        stagePanel.SetActive(false);
        gamePanel.SetActive(false);
    }
    public void GameStart()
    {
        defenses.SetActive(true);
        gameStart = true;
        mainPanel.SetActive(false);
        stagePanel.SetActive(false);
        gamePanel.SetActive(true);
        stageNum++;
        StageSetting();
    }
    void Start()
    {
        
    }

    void LateUpdate()
    {
        coinTxt.text = player.curCoin.ToString();
        enemys = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemys == 0)
        {
            defenses.SetActive(false);
            stageStart = false;
            maxHp.text = player.hpUpcnt.ToString();
            arrowSpeedUp.text = player.arrowSpeedUpcnt.ToString();
            arrowDamageUp.text = player.dmgUpcnt.ToString();
            moveSpeedUp.text = player.moveUpcnt.ToString();
            arrowPlus.text = player.arrowPlusCnt.ToString();
            assistPlus.text = player.assistCnt.ToString();
            enemyMaxHp.text = enemySpawn.maxHpCnt.ToString();
            enemySpeedUp.text = enemySpawn.speedCnt.ToString();
            enemyDmgUp.text = enemySpawn.damageCnt.ToString();
            enemyDmgUp.text = enemySpawn.spawnsCnt.ToString();
            if (player.isFloor)stagePanel.SetActive(true);
            else stagePanel.SetActive(false);
        }
        enemyTxt.text = (enemys / 2).ToString();

        hpImg.localScale = new Vector3(player.curHp / player.maxHp, 1, 1);
        
        
    }
    void StageSetting()
    {
        stageStart = true;
        enemySpawn.EnemyStage(stageNum);
    }

}
