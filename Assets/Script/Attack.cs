using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] float strongRange = 0.7f;
    [SerializeField] float areaRange = 1.2f;

    [SerializeField] LayerMask enemyLayers;

    bool attackMode = false;

    void Update()
    {
        // 🔁 Activar / desactivar modo ataque
        if (Input.GetKeyDown(KeyCode.F))
        {
            attackMode = !attackMode;
            Debug.Log("Modo ataque: " + attackMode);
        }

        if (!attackMode) return; // ❗ NO hace nada si no está activo

        // ⚔️ Ataque normal
        if (Input.GetKeyDown(KeyCode.J))
        {
            NormalAttack();
        }

        // 💥 Ataque fuerte
        if (Input.GetKeyDown(KeyCode.K))
        {
            StrongAttack();
        }

        // 🌪️ Ataque en área
        if (Input.GetKeyDown(KeyCode.L))
        {
            AreaAttack();
        }
    }

    void NormalAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayers
        );

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(1);
        }

        Debug.Log("Ataque normal");
    }

    void StrongAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            strongRange,
            enemyLayers
        );

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(3);
        }

        Debug.Log("Ataque fuerte");
    }

    void AreaAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position, // desde el jugador
            areaRange,
            enemyLayers
        );

        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(2);
        }

        Debug.Log("Ataque en área");
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, strongRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, areaRange);
    }
}
