using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
    GameObject player;
    [SerializeField] float speed = 2f;
    [SerializeField] float detectionRange = 10f;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] int health = 100;
    [SerializeField] Transform attackPoint;
    [SerializeField] float strongRange = 0.7f;
    [SerializeField] LayerMask PlayerLayers;

    float lastAttackTime;
    public Action OnDeath;

 public void Start()
 {
  player = GameObject.FindGameObjectWithTag("Player");

 }
    public void TakeDamage(int damage)
    {
        health -= damage;
        print("la vida del enemigo es: " + health);
        if (health <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }

    }
    void Update()
    {
        if (player == null)
        {
            print(player);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);

        // Detecta al jugador
        if (distance <= detectionRange)
        {
            // // Persigue si esta lejos
            // if (distance > strongRange)
            // {
            //     transform.position = Vector2.MoveTowards(
            //         transform.position,
            //         player.transform.position,
            //         speed * Time.deltaTime
            //     );
            //     print("Perseguir" + distance);
            // }
            // else
            // {
                // Ataca automaticamente
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackEnemy();
                    lastAttackTime = Time.time;
                   
                }
            // }
        }

        Flip();
    }
    public void Flip()
    {
        if (player.transform.position.x > transform.position.x)
            transform.localScale = new Vector2(1, 1);
        else
            transform.localScale = new Vector2(-1, 1);
    }
    public void AttackEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            strongRange,
            PlayerLayers
        );

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Attack>()?.EnemyDamage(20);
        }

        print("Ataque fuerte");
    }
    public int GetHealth()
    {
        return health;
    }

    public void ResetHealth()
    {
        health = 100;
    }



}
