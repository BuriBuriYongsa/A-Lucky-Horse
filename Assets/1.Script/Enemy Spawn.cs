using System.Drawing;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject[] enemys;
    public GameObject bigEnemy;

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

    public int spawns;
    public int bigspawns;
    
    
    public GameManager gManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
    }
    
    
    public void EnemyStage(int stageNum)
    {  
        if (stageNum == 10) return;
        if (stageNum < 3)
        {
            spawns += 5;
            bigspawns += 2;
        }
        else if (stageNum < 7)
        {
            spawns += 10;
            bigspawns += 5;
        }
        else
        {
            spawns += 15;
            bigspawns += 10;
        }
        spawns += spawnsUp;
        bigspawns += bigspawnsUp;
        StartCoroutine(EnemySpawns(spawns, false));
        StartCoroutine(EnemySpawns(bigspawns, true));
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
                bigspeed += 0.3f;
                speed += 0.5f;
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
                spawnsUp += 10;
                break;


        }
    }
}
