using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
public class PlayerController : MonoBehaviour {

    private Rigidbody2D body;
    private Collider2D coll;
    private Animator animator;

    [SerializeField]
    private float horizontalSpeed = 5f;

    [SerializeField]
    private float jumpVelocity = 10.5f;

    private float horizontalDirection = 0f;
    private bool initiatedJump = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") && CheckIfGrounded())
        {
            initiatedJump = true;
            animator.SetBool("initiatedJump", true);
        } else
        {
            initiatedJump = false;
            animator.SetBool("initiatedJump", false);
        }
    }

    private void FixedUpdate()
    {
        if (initiatedJump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpVelocity);
        }

        if (body.velocity.y < 0)
        {
            animator.SetBool("falling", true);
        } else
        {
            animator.SetBool("falling", false);
        }

        body.velocity = new Vector2(horizontalDirection * horizontalSpeed, body.velocity.y);
    }

    private bool CheckIfGrounded()
    {
        // Avoid performing raycast in most cases
        if (Mathf.Abs(body.velocity.y) > 0.1f)
            return false;

        return Physics2D.Raycast(transform.position - Vector3.up * (coll.bounds.extents.y + 0.01f), Vector2.down, 0.01f);
    }
}
