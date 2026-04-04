using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] float strongRange = 0.7f;
    [SerializeField] float areaRange = 1.2f;
    [SerializeField]SpriteRenderer sp;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Enemy enemy;

    bool attackMode = false;
    [SerializeField] float power = 100;
    [SerializeField] float maxPower = 100;
    [SerializeField] float rechargeRate = 5;

    void Update()
    {
        // Activar / desactivar modo ataque
        if (sp.flipX)
            attackPoint.localPosition = new Vector2(-0.1f, 0);
        else
            attackPoint.localPosition = new Vector2(0.1f, 0);
        if (Input.GetKeyDown(KeyCode.F) && power >= 20)
        {
            attackMode = !attackMode;
            print("Modo de ataque :" + attackMode);
            print("tu poder es:" + power);
        }
        else
        {
            // RECARGA 
            power += rechargeRate * Time.deltaTime;

            if (power > maxPower)
                power = maxPower;
        }

        if (!attackMode) return; //  NO hace nada si no esta activo

        // Ataque normal
        if (Input.GetKeyDown(KeyCode.J))
        {
            NormalAttack();
            power -= 10;

            if (power <= 20)
            {
                power = 0;
                attackMode = false;
                print("Modo de ataque :" + attackMode);
            }
            print("tu poder es:" + power);
        }

        // Ataque fuerte
        if (Input.GetKeyDown(KeyCode.K) && power >= 70)
        {
            StrongAttack();
            power -= 30;

            if (power <= 20)
            {
                power = 0;
                attackMode = false;
                print("Modo de ataque :" + attackMode);
            }
           print("tu poder es:" + power);
        }

        //  Ataque en área
        if (Input.GetKeyDown(KeyCode.L))
        {
            AreaAttack();
            power -= 20;

            if (power <= 20)
            {
                power = 0;
                attackMode = false;
                print("Modo de ataque :" + attackMode);
            }
            print("tu poder es:" + power);
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
            enemy.GetComponent<Enemy>()?.TakeDamage(10);
        }

        print("Ataque normal");
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
            enemy.GetComponent<Enemy>()?.TakeDamage(50);
        }

        print("Ataque fuerte");
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
            enemy.GetComponent<Enemy>()?.TakeDamage(20);
        }

        print("Ataque en área");
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
