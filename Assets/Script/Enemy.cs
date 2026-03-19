using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        print("la vida del enemigo es: " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
    }

}
