using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Collider2D boxCol;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    int _speed = 5, _jumpSpeed = 5;
    float dirx;
    //bool IsGrounded = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCol = GetComponent<Collider2D>();
    }

    private void Update()
    {
        dirx = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirx * _speed, rb.velocity.y);


        //flip
        if(dirx > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(dirx < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        anim.SetBool("isGrounded", IsGrounded());
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }

        //animations
        anim.SetBool("isRunning", dirx != 0);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, _jumpSpeed);
        anim.SetTrigger("jump");
    }


    bool IsGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size,0f,Vector2.down,0.1f,groundLayer);
        return rayCastHit.collider != null;
    }

    public bool canAttack()
    {
        return dirx == 0 && IsGrounded();
    }


}
