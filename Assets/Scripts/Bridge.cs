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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public override void Activate()
    {
        spriteRenderer.sprite = triggeredSprite;
        spriteRenderer.size = new Vector3(length, 1);
        boxCollider.enabled = true;
        boxCollider.size = new Vector2(length, boxCollider.size.y);
        transform.position = new Vector3(transform.position.x + ((length - 1f) / 2), transform.position.y, transform.position.z);
    }

    public override void Deactivate()
    {
        spriteRenderer.sprite = notTriggeredSprite;
        spriteRenderer.size = new Vector3(1, 1);
        boxCollider.enabled = false;
        boxCollider.size = new Vector2(1, boxCollider.size.y);
        transform.position = new Vector3(transform.position.x - ((length - 1f) / 2), transform.position.y, transform.position.z);
    }
}
