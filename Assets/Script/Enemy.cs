using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] Transform attackPoint;
    [SerializeField] float strongRange = 0.7f;
    [SerializeField] LayerMask PlayerLayers;

    public void TakeDamage(int damage)
    {
        health -= damage;
        print("la vida del enemigo es: " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AttackEnemy();
            print("el enemigo te ataco");
        }
    }
    void AttackEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            strongRange,
            PlayerLayers
        );

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<CharacterControl>()?.Damage(50);
        }
    

        print("Ataque fuerte");
    }
        
    

}
