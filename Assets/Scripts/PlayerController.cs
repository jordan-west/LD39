using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour {

    private Rigidbody2D body;
    private Collider2D coll;

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
    }

    private void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") && CheckIfGrounded())
        {
            initiatedJump = true;
        } else
        {
            initiatedJump = false;
        }
    }

    private void FixedUpdate()
    {
        if (initiatedJump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpVelocity);
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
