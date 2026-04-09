using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    GameObject player;
    [SerializeField] float detectionDistance = 5f;
    [SerializeField] Transform attackPoint;
    [SerializeField] SpriteRenderer sp;
    [SerializeField] LayerMask groundCheck;
    private bool _isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < detectionDistance)
        {

            transform.position = Vector2.MoveTowards(
                transform.position,
                player.transform.position,
                speed * Time.deltaTime
            );

            if (player.transform.position.x < transform.position.x)
                sp.flipX = true;
            else
                sp.flipX = false;
        }
          Collider2D col = GetComponent<Collider2D>();
        _isGrounded = Physics2D.OverlapCircle(transform.position - transform.up * ((col.bounds.extents.y / transform.localScale.y - col.offset.y) * transform.localScale.y), 0.01f, groundCheck);
        if (sp.flipX)
        attackPoint.localPosition = new Vector2(-0.7f, 0);
        else
        attackPoint.localPosition = new Vector2(0.7f, 0);
    }


    // Update is called once per frame
    

}
