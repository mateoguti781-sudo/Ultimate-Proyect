using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Attack player;
    [SerializeField] Enemy enemy;

    [Header("Tiempo")]
    [SerializeField] float roundTime = 60f;
    float currentTime;

    [Header("Rondas")]
    int playerWins = 0;
    int enemyWins = 0;
    [SerializeField] int roundsToWin = 2;

    bool roundEnded = false;

    void Start()
    {
        StartRound();
    }

    void Update()
    {
        if (roundEnded) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            EndRoundByTime();
        }

        CheckKO();
    }

    // INICIAR RONDA
    void StartRound()
    {
        currentTime = roundTime;
        roundEnded = false;

        print("NUEVA RONDA");
    }

    // VERIFICAR KO
    void CheckKO()
    {
        if (player == null || enemy == null) return;

        if (playerHealth() <= 0)
        {
            EnemyWinsRound();
        }
        else if (enemyHealth() <= 0)
        {
            PlayerWinsRound();
        }
    }

    // TIEMPO TERMINADO
    void EndRoundByTime()
    {
        roundEnded = true;

        if (playerHealth() > enemyHealth())
        {
            PlayerWinsRound();
        }
        else if (enemyHealth() > playerHealth())
        {
            EnemyWinsRound();
        }
        else
        {
            print("Empate");
            RestartRound();
        }
    }

    // GANADOR
    void PlayerWinsRound()
    {
        roundEnded = true;
        playerWins++;
        print("PLAYER GANA ROUND");

        CheckGameWinner();
    }

    void EnemyWinsRound()
    {
        roundEnded = true;
        enemyWins++;
        print("ENEMY GANA ROUND");

        CheckGameWinner();
    }

    // GANADOR FINAL
    void CheckGameWinner()
    {
        if (playerWins >= roundsToWin)
        {
            print("PLAYER GANA EL JUEGO");
            Time.timeScale = 0;
        }
        else if (enemyWins >= roundsToWin)
        {
            print("ENEMY GANA EL JUEGO");
            Time.timeScale = 0;
        }
        else
        {
            Invoke(nameof(RestartRound), 2f);
        }
    }

    void RestartRound()
    {
        print("REINICIANDO RONDA");

        // Reiniciar vida
        player.ResetHealth();
        enemy.ResetHealth();

        // Reiniciar posiciones
        player.transform.position = new Vector2(-2, 0);
        enemy.transform.position = new Vector2(2, 0);

        StartRound();
    }

    int playerHealth()
    {
        return player.GetHealth();
    }

    int enemyHealth()
    {
        return enemy.GetHealth();
    }

}

