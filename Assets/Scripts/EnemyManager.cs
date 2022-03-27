using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int level = 0;
    public GameObject target;
    public GameObject[] enemys;
    public GameObject[] drops;
    private List<GameObject> enemyTrack = new List<GameObject>();
    private bool summonEnemys = false;
    private int enemysSummoned = 0;
    private float enemySummonCooldown = 0f;
    private AudioSettings audioSettings;
    
    void Start()
    {
        GameObject globalGameSettings = GameObject.Find("GameSettings");
        this.audioSettings = globalGameSettings.GetComponent<AudioSettings>();
        InvokeRepeating("TrackEnemys", 0f, 1f);
    }
    
    void Update()
    {

        if (summonEnemys) {

            enemySummonCooldown -= Time.deltaTime;
            if (enemySummonCooldown <= 0) {
                SummonEnemy();
                enemysSummoned += 1;
                enemySummonCooldown = 1f;
            }

            if (enemysSummoned >= (int) Mathf.Floor(level/3) + 1) {
                summonEnemys = false;
                enemysSummoned = 0;
                enemySummonCooldown = 0f;
            }
        }
    }

    private void TrackEnemys() {

        enemyTrack = enemyTrack.FindAll(el => el != null);

        if (enemyTrack.Count == 0) {
            level += 1;

            if (level > 1) {
                this.audioSettings.PlaySound("LevelUp");
            }
            int enemyNumber = (int) Mathf.Floor(level/3) + 1;
            summonEnemys = true;
        }
    }

    private void SummonEnemy() {
        int enemyIndex = (int) Random.Range(0, this.enemys.Length);
        GameObject enemy = Instantiate(enemys[enemyIndex], gameObject.transform.position, gameObject.transform.rotation);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.target = target.transform;

        if (drops.Length > 0 && Random.Range(0f, 1f) > 0.5f) {

            int skill = Random.Range(0, drops.Length);

            for (int i = 0; i < 2; i+=1) {
                if (skill == drops.Length-1) {
                    skill = Random.Range(0, drops.Length);
                }
                else
                    break;
            }

            GameObject enemyDrop = drops[skill];
            enemyScript.drop = enemyDrop;
        }

        enemyTrack.Add(enemy);
    }
}
