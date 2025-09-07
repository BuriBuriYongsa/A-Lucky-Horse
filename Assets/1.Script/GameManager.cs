using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject gamePanel;
    public GameObject stagePanel;
    public GameObject gameOverPanel;
    public GameObject gameSuccesPanel;
    public GameObject gameTimer;
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
    public Text gameTimerText;

    public int finalStage;


    [Tooltip("디펜스 오브젝트")] public GameObject defenses;

    //플레이어 기본스탯
    float setSpeed;
    float setHp;
    float setArrowSpeed;
    int setDamage;
    int setAsisDamage;
    int setBowDamage;

    //"적 기본스탯
    float setEnemySpeed;
    float setBigEnemySpeed;
    float setEnemyDmg;
    float setBigEnemyDmg;
    float setEnemyHp;
    float setBigEnemyHp;
    float setSqawn;

    public bool gameStart = false;
    public bool stageClear;
    public int stageNum = 0;
    bool stageReady = false;

    public static int enemys; 

    public Player player;
    public EnemySpawn enemySpawn;
    public WeaponBack weaponBackBow;
    public WeaponBack weaponBackAssis;
    private void Awake()
    {
        stageClear = false;
        mainPanel.SetActive(true);
        gameSuccesPanel.SetActive(false);
        gamePanel.SetActive(false);
        stagePanel.SetActive(false);
        PlayerSetting();
    }
    public void GameStart()
    {
        mainPanel.SetActive(false);
        defenses.SetActive(true);
        gamePanel.SetActive(true);
        StartCoroutine(GameTimer());
        
    }
    public void GameReStart()
    {
        resetting();
        mainPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameSuccesPanel.SetActive(false);
        stagePanel.SetActive(false);
        gamePanel.SetActive(false);
        stageNum = 0;
    }
    public void GameSucces()
    {
        stageClear = false;
        gameStart = false;
        stageNum = 0;
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
        gameSuccesPanel.SetActive(true);
        stagePanel.SetActive(false);
        gamePanel.SetActive(false);
    }
    public void NextStage()
    {
        stageReady = true;
        stageClear = false;
        stagePanel.SetActive(false);
        StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        gameTimer.SetActive(true);
        gameTimerText.text = "3";
        yield return new WaitForSeconds(1f);
        gameTimerText.text = "2";
        yield return new WaitForSeconds(1f);
        gameTimerText.text = "1";
        yield return new WaitForSeconds(1f);
        gameTimerText.text = "Game Start!";
        gameTimer.SetActive(false);
        gameStart = true;
        stageNum++;
        enemySpawn.EnemyStage(stageNum);

    }

    void LateUpdate()
    {
        coinTxt.text = player.curCoin.ToString();
        enemys = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemys == 0 && gameStart && !stageReady)
        {
            if(stageNum >= finalStage && stageClear)
            {
                GameSucces();
                return;
            }
            defenses.SetActive(false);
            stageClear = true;
            maxHp.text = player.hpUpcnt.ToString();
            arrowSpeedUp.text = player.arrowSpeedUpcnt.ToString();
            arrowDamageUp.text = player.dmgUpcnt.ToString();
            moveSpeedUp.text = player.moveUpcnt.ToString();
            arrowPlus.text = player.arrowPlusCnt.ToString();
            assistPlus.text = player.assistCnt.ToString();
            enemyMaxHp.text = enemySpawn.maxHpCnt.ToString();
            enemySpeedUp.text = enemySpawn.speedCnt.ToString();
            enemyDmgUp.text = enemySpawn.damageCnt.ToString();
            enemySpawnUp.text = enemySpawn.spawnsCnt.ToString();
            if (player.isFloor && stageClear)stagePanel.SetActive(true);
        }
        enemyTxt.text = (enemys / 2).ToString();

        hpImg.localScale = new Vector3(player.curHp / player.maxHp, 1, 1);
        if (player.curHp <= 0)
        {
            GameOver();
        }
        
    }
    
    void GameOver()
    {
        resetting();
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemys)
        {
            Destroy(enemy);
        }
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
        stageClear = false;
        gameStart = false;
        gameOverPanel.SetActive(true);

    }
    void PlayerSetting()
    {
        setSpeed = player.speed;
        setHp = player.maxHp;
        setDamage = player.weapon.arrows.GetComponent<Arrow>().damage;
        setArrowSpeed = player.weapon.speed;

        weaponBackBow = player.bows[0].GetComponent<WeaponBack>();
        setBowDamage = weaponBackBow.arrows.GetComponent<Arrow>().damage;

        weaponBackAssis = player.assists[0].GetComponentInChildren<WeaponBack>();
        setAsisDamage = weaponBackAssis.arrows.GetComponent<Arrow>().damage;

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
            weaponBackAssis = player.assists[i].GetComponentInChildren<WeaponBack>();
            weaponBackAssis.arrows.GetComponent<Arrow>().damage = setAsisDamage;
        }
        for (int i = 0; i <= 1; i++)
        {
            player.bows[i].SetActive(false);
            weaponBackBow = player.bows[i].GetComponent<WeaponBack>();
            weaponBackBow.arrows.GetComponent<Arrow>().damage = setBowDamage;
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
