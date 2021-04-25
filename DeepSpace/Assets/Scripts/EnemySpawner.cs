using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour
{
    public int targetDifficulty = 6;
    public float spawnMinCoolDown = 1f;
    public float spawnMaxCoolDown = 15f;
    public List<Enemy> spawnableEnemies;
    public float minSpawnDistance = 20f;
    public float maxSpawnDistance = 30f;

    List<Enemy> enemies = new List<Enemy>();
    int curDifficulty = 0;
    float spawnCoolDownTimer = 0f;
    float curCoolDown = 0f;

    void calcNewCoolDown() {
        spawnCoolDownTimer = 0;
        curCoolDown = Random.Range(spawnMinCoolDown, spawnMaxCoolDown);
    }

    void Start()
    {
        Assert.IsFalse(spawnableEnemies.Count == 0);
        calcNewCoolDown();
    }

    void registerEnemy(Enemy e) {
        curDifficulty += e.difficulty;
        enemies.Add(e);
    }

    void deregisterEnemy(Enemy e) {
        curDifficulty -= e.difficulty;
        enemies.Remove(e);
    }

    Enemy getSpawnableEnemy() {
        //Get random Enemy
        int index = Random.Range(0, spawnableEnemies.Count - 1);
        return spawnableEnemies[index];
    }

    void Update()
    {
        spawnCoolDownTimer += Time.deltaTime;
        if((curDifficulty <= targetDifficulty) && (spawnCoolDownTimer >= curCoolDown)) {
            //Get a Random Direction
            var dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            //Apply a Random magnitute
            dir = dir.normalized * Random.Range(minSpawnDistance, maxSpawnDistance);
            //Spawn random Enemy and register it
            var e = Instantiate(getSpawnableEnemy(), dir, Quaternion.identity, transform);
            registerEnemy(e);
            calcNewCoolDown();
        }
    }
}
