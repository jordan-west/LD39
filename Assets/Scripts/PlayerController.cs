using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Assume SpriteRenderer available?
 */
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
public class PlayerController : MonoBehaviour {

    private Rigidbody2D body;
    private Collider2D coll;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;

    private Transform antenna;

    [SerializeField]
    private LayerMask laserLayerMask;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();

        antenna = transform.Find("Antenna");

        lineRenderer.enabled = false;
    }

    private void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (horizontalDirection != 0)
        {
            animator.SetBool("initiatedRun", true);
            animator.SetBool("runStop", false);

            if (horizontalDirection > 0)
            {
                spriteRenderer.flipX = false;
            } else
            {
                spriteRenderer.flipX = true;
            }
        } else
        {
            animator.SetBool("initiatedRun", false);
            animator.SetBool("runStop", true);
        }

        lineRenderer.SetPosition(0, antenna.position);

        if (Input.GetButton("Jump") && CheckIfGrounded())
        {
            initiatedJump = true;
            animator.SetBool("initiatedJump", true);
        } else
        {
            initiatedJump = false;
            animator.SetBool("initiatedJump", false);
        }

        if (Input.GetMouseButtonDown(0) && body.velocity.y == 0)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);

            if ((mousePosition - antenna.position).x > 0 && horizontalDirection == 0)
            {
                spriteRenderer.flipX = false;
            } else if ((mousePosition - antenna.position).x < 0 && horizontalDirection == 0)
            {
                spriteRenderer.flipX = true;
            }

            RaycastHit2D hit = Physics2D.Raycast(antenna.position, mousePosition - antenna.position, Vector3.Distance(antenna.position, mousePosition) + 0.01f, laserLayerMask);

            if (hit)
            {
                StartCoroutine(FlashLaser(hit.point, 0.3f));

                if (hit.transform.gameObject.GetComponent<EnemyController>())
                {
                    hit.transform.gameObject.GetComponent<EnemyController>().Sleep(3f);
                }

                if (hit.transform.gameObject.GetComponent<Antenna>())
                {
                    hit.transform.gameObject.GetComponent<Antenna>().Activate();
                }
            } else
            {
                StartCoroutine(FlashLaser(mousePosition, 0.3f));
            }
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

        return Physics2D.Raycast(coll.bounds.center, Vector2.down, coll.bounds.extents.y + 0.1f, laserLayerMask);
    }

    private IEnumerator FlashLaser(Vector3 laserStopPosition, float waitTime)
    {
        lineRenderer.SetPosition(1, laserStopPosition);
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(waitTime);

        lineRenderer.enabled = false;
    }
}
