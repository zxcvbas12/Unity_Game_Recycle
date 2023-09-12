using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;


    private float[] arrPosx = {-2.2f, -0.8f, 0.2f, 1.4f, 2.4f};

    [SerializeField]
    private float spawnInterval =1.5f;

    void Start()
    {
       StartEnemyRoutine();
    }

    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }
    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        int enmeyIndex = 0;
        int spawnCount = 0;
        float moveSpeed =5f;

        while(true)
        {

            foreach(float posx in arrPosx)
            {
                SpawnEnemy(posx, enmeyIndex, moveSpeed);
            }

            spawnCount++;
            if(spawnCount %5 == 0)
            {
                enmeyIndex++;
                moveSpeed +=2;
            }

            if(enmeyIndex >= enemies.Length)
            {
                SpawnBoss();
                enmeyIndex = 0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if(Random.Range(0,5) == 0)
        {
            index++;
        }

        if(index >= enemies.Length)
        {
            index = enemies.Length - 1;
        }

        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
