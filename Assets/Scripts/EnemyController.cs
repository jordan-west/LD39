using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D body;
    private Animator animator;
    private Animator antennaAnimator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private float waitTime = 2f;

    [SerializeField]
    private float wakeTime = 1f;

    [SerializeField]
    private float sleepTime = 3f;

    [SerializeField]
    private Vector3 endPosition;

    [SerializeField]
    private bool loop = true;

    [SerializeField]
    private ATriggerable trigger;

    [SerializeField]
    private LayerMask groundLayerMask;

    private Vector3 startPosition;

    private bool isWalking = false;
    private bool isAngry = false;
    private bool isAsleep = true;

    private float currentTime = 0f;
    private float timeToTake;
    private bool reversed = false;

    private bool hasBeenTriggered = false;

    private Bridge stoodOnBridge;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        antennaAnimator = transform.Find("Antenna").GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        startPosition = transform.position;
        timeToTake = Vector3.Distance(startPosition, endPosition) / speed;

        if ((endPosition - startPosition).x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (trigger == null)
        {
            hasBeenTriggered = true;
            StartCoroutine(WakeUp(wakeTime));
        }
    }

    private void Update()
    {
        animator.SetBool("isRunning", isWalking);
        animator.SetBool("isAngry", isAngry);
        animator.SetBool("isAsleep", isAsleep);
        antennaAnimator.SetBool("isAngry", isAngry);
        antennaAnimator.SetBool("isAsleep", isAsleep);

        if (trigger != null)
        {
            if (trigger.Activated && hasBeenTriggered == false)
            {
                hasBeenTriggered = true;
                StartCoroutine(WakeUp(wakeTime));
            }
        }

        if (isWalking && !isAsleep)
        {
            if (reversed)
            {
                currentTime -= Time.deltaTime;
            } else
            {
                currentTime += Time.deltaTime;
            }
        }

        if (isWalking && !isAsleep && CheckIfGroundedAhead() == false)
        {
            reversed = !reversed;

            StartCoroutine(StandStill(waitTime));
        }

        if ((currentTime >= timeToTake && !reversed) || (currentTime <= 0 && reversed))
        {
            if (loop)
            {
                if (reversed)
                {
                    currentTime = 0;
                } else
                {
                    currentTime = timeToTake;
                }

                reversed = !reversed;

                StartCoroutine(StandStill(waitTime));
            } else
            {
                isWalking = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isAsleep && isWalking)
        {
            body.MovePosition(Vector3.Lerp(startPosition, endPosition, currentTime / timeToTake));
        }
    }

    public void Sleep()
    {
        if (isAsleep == false)
        {
            StartCoroutine(SleepCo(sleepTime));
        }   
    }

    private IEnumerator StandStill(float seconds)
    {
        isWalking = false;

        yield return new WaitForSeconds(seconds);

        spriteRenderer.flipX = !spriteRenderer.flipX;

        isWalking = true;
    }

    private IEnumerator SleepCo(float seconds)
    {
        isAsleep = true;

        yield return new WaitForSeconds(seconds);

        isAsleep = false;
    }

    private IEnumerator WakeUp(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        isAsleep = false;
        isWalking = true;
    }

    private bool CheckIfGroundedAhead()
    {
        int flip = (spriteRenderer.flipX) ? -1 : 1;

        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center + Vector3.right * flip * 0.5f, Vector2.down, boxCollider.bounds.extents.y + 0.1f, groundLayerMask);

        if (hit)
        {
            if (hit.collider.gameObject.GetComponent<Bridge>())
            {
                if (stoodOnBridge == null)
                {
                    stoodOnBridge = hit.collider.gameObject.GetComponent<Bridge>();
                    stoodOnBridge.NotifyStoodOn(true);
                }
            } else
            {
                if (stoodOnBridge != null)
                {
                    stoodOnBridge.NotifyStoodOn(false);
                    stoodOnBridge = null;
                }
            }
        }

        return hit;
    }
}
