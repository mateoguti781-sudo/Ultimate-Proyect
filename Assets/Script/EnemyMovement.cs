using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] Transform player;
    [SerializeField] float detectionDistance = 5f;
    [SerializeField] Transform attackPoint;
    [SerializeField] SpriteRenderer sp;
    // Start is called before the first frame update
    void update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionDistance)
        {

            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );

            if (player.position.x < transform.position.x)
                sp.flipX = true;
            else
                sp.flipX = false;
        }
        if (sp.flipX)
        attackPoint.localPosition = new Vector2(-0.7f, 0);
        else
        attackPoint.localPosition = new Vector2(0.7f, 0);
    }

    // Update is called once per frame
    

}
