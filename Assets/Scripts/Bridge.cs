using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : ATriggerable {

    [SerializeField]
    private Sprite notTriggeredSprite;

    [SerializeField]
    private Sprite triggeredSprite;

    [SerializeField]
    private float length = 1;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private int flip = 1;

    private bool stoodOn = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        flip = (spriteRenderer.flipX) ? -1 : 1;
    }

    public override void Activate()
    {
        if (numberOfTimesActivated == 0)
        {
            activated = true;

            spriteRenderer.sprite = triggeredSprite;
            spriteRenderer.size = new Vector3(length, 1);
            boxCollider.enabled = true;
            boxCollider.size = new Vector2(length, boxCollider.size.y);
            transform.position = new Vector3(transform.position.x + ((length - 1f) / 2) * flip, transform.position.y, transform.position.z);
        }

        numberOfTimesActivated++;
    }

    public override void Deactivate()
    {
        if (numberOfTimesActivated > 0)
        {
            numberOfTimesActivated--;
        }
        
        if (numberOfTimesActivated <= 0)
        {
            if (stoodOn)
            {
                StartCoroutine(WaitToBeStoodOff());
            } else
            {
                activated = false;
                numberOfTimesActivated = 0;

                spriteRenderer.sprite = notTriggeredSprite;
                spriteRenderer.size = new Vector3(1, 1);
                boxCollider.enabled = false;
                boxCollider.size = new Vector2(1, boxCollider.size.y);
                transform.position = new Vector3(transform.position.x - ((length - 1f) / 2) * flip, transform.position.y, transform.position.z);
            }
        }
    }

    public void NotifyStoodOn(bool isStoodOn)
    {
        stoodOn = isStoodOn;
    }

    private IEnumerator WaitToBeStoodOff()
    {
        while (stoodOn)
        {
            yield return null;
        }

        Deactivate();
    }
}
