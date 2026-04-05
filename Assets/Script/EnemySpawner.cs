using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;

    [Header("Control")]
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] int maxEnemies = 3;

    int currentEnemies = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemies < maxEnemies)
            {
                Spawn();
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void Spawn()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);

        currentEnemies++;

        // Cuando el enemigo muera → restar contador
        enemy.GetComponent<Enemy>().OnDeath += EnemyDied;
    }

    void EnemyDied()
    {
        currentEnemies--;
    }
}
