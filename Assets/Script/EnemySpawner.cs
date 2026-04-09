using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;

    [Header("Rondas")]
    [SerializeField] int baseEnemies = 2; // enemigos en ronda 1
    int currentRound = 1;
    int enemiesToSpawn;
    int currentEnemiesAlive = 0;

    void Start()
    {
        StartRound();
    }

    // INICIAR RONDA
    public void StartRound()
    {
        enemiesToSpawn = baseEnemies + currentRound;
        currentEnemiesAlive = enemiesToSpawn;

        print("RONDA " + currentRound + " - Enemigos: " + enemiesToSpawn);

        StartCoroutine(SpawnWave());
    }

    // SPAWN DE LA RONDA
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Spawn();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Spawn()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.OnDeath += EnemyDied;
    }

    // CUANDO MUERE UN ENEMIGO
    void EnemyDied()
    {
        currentEnemiesAlive--;

        print("Enemigos restantes: " + currentEnemiesAlive);

        // Si ya no quedan → siguiente ronda
        if (currentEnemiesAlive <= 0)
        {
            currentRound++;
            Invoke(nameof(StartRound), 2f); // espera 2 segundos
        }
    }
}
