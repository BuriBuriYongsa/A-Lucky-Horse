using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject gamePanel;
    public GameObject stagePanel;
    public GameObject gameOverPanel;
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

    //플레이어 기본스탯
    float setSpeed;
    float setHp;
    float setArrowSpeed;
    int setDamage;
    int setAssistDamage;
    int setBowDamage;

    //"적 기본스탯
    float setEnemySpeed;
    float setBigEnemySpeed;
    float setEnemyDmg;
    float setBigEnemyDmg;
    float setEnemyHp;
    float setBigEnemyHp;
    float setSqawn;

    public bool gameStart;
    public bool stageStart;
    public bool gameOver;
    public int stageNum = 0;

    public static int enemys; 

    public Player player;
    public EnemySpawn enemySpawn;
    private void Awake()
    {
        mainPanel.SetActive(true);
        stagePanel.SetActive(false);
        gamePanel.SetActive(false);
        PlayerSetting();
    }
    public void GameStart()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(false);
            stageNum = 0;
            gameOver = false;
            resetting();
        }
        else
        {
            mainPanel.SetActive(false);
        }
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
        if (player.curHp <= 0)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
        }
        
    }
   
    void StageSetting()
    {
        stageStart = true;
        enemySpawn.EnemyStage(stageNum);
    }

    void PlayerSetting()
    {
        setSpeed = player.speed;
        setHp = player.maxHp;
        setDamage = player.weapon.arrows.GetComponent<Arrow>().damage;
        setArrowSpeed = player.weapon.speed;

    }
    void resetting()
    {
        player.curHp = setHp;
        player.speed = setSpeed;
        player.turnSpeed = setSpeed;
        player.weapon.arrows.GetComponent<Arrow>().damage = setDamage;
        player.weapon.speed = setArrowSpeed;
        player.arrowPlusCnt = 0;
        player.assistCnt = 0;
        player.arrowSpeedUpcnt = 0;
        player.dmgUpcnt = 0;
        player.hpUpcnt = 0;
        player.moveUpcnt = 0;
        for (int i = 0; i <= 3; i++)
        {
            player.assists[i].SetActive(false);
            player.weaponBackAssis = player.assists[i].GetComponentInChildren<WeaponBack>();
            player.weaponBackAssis.arrows.GetComponent<Arrow>().damage = player.setAsisDamage;
        }
        for (int i = 0; i <= 1; i++)
        {
            player.bows[i].SetActive(false);
            player.weaponBackBow = player.bows[i].GetComponent<WeaponBack>();
            player.weaponBackBow.arrows.GetComponent<Arrow>().damage = player.setBowDamage;
        }
        enemySpawn.bigspeed = 0;
        enemySpawn.speed = 0;
        enemySpawn.damage = 0;
        enemySpawn.bigdamage = 0;
        enemySpawn.bighp = 0;
        enemySpawn.hp = 0;
        enemySpawn.spawnsUp = 0;
        enemySpawn.damageCnt = 0;
        enemySpawn.maxHpCnt = 0;
        enemySpawn.speedCnt = 0;
        enemySpawn.spawnsCnt = 0;
    }
}
