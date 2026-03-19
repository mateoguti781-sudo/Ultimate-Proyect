using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] float speed=5f;
    [SerializeField] float jumpForce=7f;
    private bool _isGrounded = false;
    private Rigidbody2D _rb;
    [SerializeField] LayerMask groundCheck;
    [SerializeField]SpriteRenderer sp;
    // [SerializeField] GameObject prefab, prefab2;
    GameObject build_prefab;
    //int step = 0, step2;
    //[SerializeField] GameObject step_block, step_block2, magnet;

    Vector2 Spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Spawnpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(moveHorizontal * speed, _rb.velocity.y);
        if(moveHorizontal > 0)
        {
            sp.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            sp.flipX = true;
        }

        Collider2D col = GetComponent<Collider2D>();
        _isGrounded = Physics2D.OverlapCircle(transform.position - transform.up * ((col.bounds.extents.y / transform.localScale.y - col.offset.y) * transform.localScale.y), 0.01f, groundCheck);
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
        // if(Input.GetKeyDown(KeyCode.D)&& _isGrounded)
        // {

        // }
        //------------------------------------------------------------------

        if(build_prefab)
        {
             Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             build_prefab.transform.position = new Vector3(mousePosition.x,mousePosition.y, transform.position.z);
             if(Input.GetMouseButtonDown(0))
             {
                build_prefab.GetComponent<Collider2D>().enabled = true;
                build_prefab = null;

            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("DeadPoint"))
        {
            transform.position = Spawnpoint;
        }
        if(collision.tag == "CheckPoint")
        {
            Spawnpoint = collision.transform.position;
            collision.GetComponent<Animator>().SetBool("Active", true);
        }
        // if(collision.tag == "Bloque")
        // {
        //     step++;
        //     Destroy(collision.gameObject);

        //     foreach(Transform child in step_block.transform)
        //     {
        //         if(!child.gameObject.activeInHierarchy)
        //         {
        //             child.gameObject.SetActive(true);
        //             return;
        //         }
        //     }
        // }
        //  if(collision.tag == "Bloque2")
        // {
        //     step2++;
        //     Destroy(collision.gameObject);

        //     foreach(Transform child in step_block2.transform)
        //     {
        //         if(!child.gameObject.activeInHierarchy)
        //         {
        //             child.gameObject.SetActive(true);
        //             return;
        //         }
        //     }
        // }
    }
    //    if(collision.tag == "Magnet")
    //     {
    //         magnet.SetActive(true);
    //         Destroy(collision.gameObject);
    //     }
    // }
    // public void builtClick()
    // {
    //     step--;
    //     foreach(Transform child in step_block.transform)
    //     {
    //         if(child.gameObject.activeInHierarchy)
    //         {
    //             child.gameObject.SetActive(false);
    //             build_prefab = Instantiate(prefab);
    //             return;
    //         }
    //     }
    // }

}
