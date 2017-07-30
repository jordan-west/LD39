using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D body;
    private Animator animator;
    private Animator antennaAnimator;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private float waitTime = 2f;

    [SerializeField]
    private Vector3 startPosition;

    [SerializeField]
    private Vector3 endPosition;

    [SerializeField]
    private bool loop = true;

    private bool isWalking = false;
    private bool isAngry = true;
    private bool isAsleep = false;

    private float currentTime = 0f;
    private float timeToTake;
    private bool reversed = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        antennaAnimator = transform.Find("Antenna").GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.flipX = true;

        timeToTake = Vector3.Distance(startPosition, endPosition) / speed;

        isWalking = true;
    }

    private void Update()
    {
        animator.SetBool("isRunning", isWalking);
        animator.SetBool("isAngry", isAngry);
        animator.SetBool("isAsleep", isAsleep);
        antennaAnimator.SetBool("isAngry", isAngry);
        antennaAnimator.SetBool("isAsleep", isAsleep);

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

    public void Sleep(float seconds)
    {
        if (isAsleep == false)
        {
            StartCoroutine(SleepCo(seconds));
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
}
