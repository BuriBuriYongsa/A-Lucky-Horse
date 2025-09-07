using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject[] enemys;
    public GameObject bigEnemy;
    public GameObject exEnemyPanel;
    public Text exEnemy;

    public float speed;
    public float bigspeed;
    public float hp;
    public float bighp;
    public float damage;
    public float bigdamage;
    public int spawnsUp = 0;
    public int bigspawnsUp = 0;

    public int speedCnt = 0;
    public int maxHpCnt = 0;
    public int damageCnt = 0;
    public int spawnsCnt = 0;

    
    public int basicSpawns = 0;
    public int basicBigspawns = 0;

    int exTotal = 6;
    int exEneB = 2;
    int exEne = 1;
    bool exCheck = false;
    public GameManager gManager;
    void Start()
    {
        exEnemyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!exCheck && gManager.stageClear)
        {
            exCheck = true;
            exEnemyPanel.SetActive(true);
            UpdateNextEnemy(false);
        }else if(!gManager.stageClear)
        {
            exCheck=false;
            exEnemyPanel.SetActive(false);
        }
    }
    
    
    public void EnemyStage(int stageNum)
    {
        int spawns = 0;
        int bigspawns = 0;
       
        if (stageNum == 10 && !gManager.gameStart) return;
        if(stageNum == 1)
        {
            basicSpawns = 0;
            basicBigspawns = 0;
        }
        if (stageNum < 3)
        {
            spawns = 2;
            bigspawns = 1;
        }
        else if (stageNum < 7)
        {
            spawns = 3;
            bigspawns = 2;
        }
        else
        {
            spawns = 3;
            bigspawns = 3;
        }
        basicSpawns += spawns;
        basicBigspawns += bigspawns;

        int totalSpawns = basicSpawns + spawnsUp;
        int totalBigspawns = basicBigspawns + bigspawnsUp;
        
        StartCoroutine(EnemySpawns(totalSpawns, false));
        StartCoroutine(EnemySpawns(totalBigspawns, true));
    }
    public IEnumerator EnemySpawns(int spawns, bool big)
    {
        for (int i = 0; i < spawns; i++) 
        {
            if (!big)
            {for (int j = 0; j < 2; j++)
                {
                    int spawnNum = Random.Range(0, enemys.Length);
                    GameObject Enemy = Instantiate(enemys[spawnNum], spawnPoint[j].position, Quaternion.identity);

                    Enemy ene = Enemy.GetComponent<Enemy>();
                    ene.damage += damage;
                    ene.enemySpear.damage += damage;
                    ene.speed += speed;
                    ene.hp += hp;
                }
            }
            else
            {for (int j = 0; j < 2; j++)
                {
                    GameObject Enemy = Instantiate(bigEnemy, spawnPoint[j].position, Quaternion.identity);

                    Enemy ene = Enemy.GetComponent<Enemy>();
                    ene.damage += bigdamage;
                    ene.enemySpear.damage += bigdamage;
                    ene.speed += bigspeed;
                    ene.hp += bighp;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void UpGrade(string str)
    {
        switch (str)
        {
            case "EnemyMoveUp":
                bigspeed += 0.1f;
                speed += 0.2f;
                speedCnt++;
                break;
            case "EnemyDamageUp":
                bigdamage += 10f;
                damage += 5f;
                damageCnt++;
                break;
            case "EnemyMaxHpUp":
                bighp += 10f;
                hp += 5f;
                maxHpCnt++;
                break;
            case "EnemySpownUp":
                spawnsCnt++;
                spawnsUp += 2;
                bigspawnsUp += 1;
                UpdateNextEnemy(true);
                break;
        }
    }
    public void UpdateNextEnemy(bool Gacha)
    {
        if (!Gacha)
        {
            if (gManager.stageNum+1 < 3)
            {
                exEne += 2;
                exEneB += 1;
            }
            else if (gManager.stageNum+1 < 7)
            {
                exEne += 3;
                exEneB += 2;
            }
            else
            {
                exEne += 3;
                exEneB += 3;
            }
        }
        exEne += spawnsUp;
        exEneB += bigspawnsUp;
        exTotal = (exEne + exEneB) * 2;

        exEnemy.text = "Next Enemy " + exTotal;
    }
}
