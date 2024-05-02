
using System.Collections;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float dirx;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        dirx = Input.GetAxis("Horizontal");

        //Flip player when moving left-right
        if (dirx > 0.01f)
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        else if (dirx < -0.01f)
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);

        //Set animator parameters
        anim.SetBool("isRunning", dirx != 0);
        anim.SetBool("isGrounded", isGrounded());

        //movement logic

        rb.velocity = new Vector2(dirx * speed, rb.velocity.y);

        if (Input.GetKey(KeyCode.Space))
            Jump();

    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return dirx == 0 && isGrounded();
    }
}
