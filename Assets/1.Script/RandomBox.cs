using UnityEngine;

public class RandomBox : MonoBehaviour
{
    [Header("Ä¸½¶")]
    public GameObject yellowBox;
    public GameObject whiteBox;
    public GameObject blackBox;
    public GameObject redBox;

    public string[] yellowBoxs = {"ArrowPlus", "Assist" };
    public string[] whiteBoxs = { "MaxHpUp", "DamageUp", "ArrowSpeedUp", "MoveUp" };
    public string[] blackBoxs = { "EnemyMoveUp", "EnemyDamageUp", "EnemyMaxHpUp"};
    public string redBoxs = "EnemySpownUp";

    public GameObject rotation;
    public GameObject ramdomPanel;
    public AudioSource gachaAudio;

    public bool isOpen = false;

    public Transform point;

    public Player player;
    public EnemySpawn enemySpawn;
    void Start()
    {
        ramdomPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Gacha()
    {
        isOpen = true;
        gachaAudio.Play();
        string boxName = "";
        Vector3 currentRotation = rotation.transform.eulerAngles;
        rotation.transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, currentRotation.z + 140f);
        GameObject ramdomBox = null;

        int rand = Random.Range(0, 100);

        if (rand <= 10)
        {
            int randYellow = Random.Range(0, yellowBoxs.Length);
            ramdomBox = yellowBox;
            boxName = yellowBoxs[randYellow];
            player.UpGrade(boxName);
        }
        else if (rand > 10 && rand <= 20)
        {
            ramdomBox = redBox;
            boxName = redBoxs;
            enemySpawn.UpGrade(boxName);
        }
        else if(rand > 20 && rand <= 70)
        {
            int randWhite = Random.Range(0, whiteBoxs.Length);
            ramdomBox = whiteBox;
            boxName = whiteBoxs[randWhite];
            player.UpGrade(boxName);
        }
        else if (rand > 70 && rand <= 100)
        {
            int randBlack = Random.Range(0, blackBoxs.Length);
            ramdomBox = blackBox;
            boxName = blackBoxs[randBlack];
            enemySpawn.UpGrade(boxName);
        }
        GameObject box = Instantiate(ramdomBox, point.position, Quaternion.identity);
        Destroy(box, 2);
        Invoke("TimeCheck", 1f);

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            ramdomPanel.SetActive(true);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            ramdomPanel.SetActive(false);
        }
    }
        void TimeCheck()
    {
        isOpen = false;
    }
}
