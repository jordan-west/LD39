using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Antenna : MonoBehaviour
{
    [SerializeField]
    private ATriggerable triggerableObject;

    [SerializeField]
    private Sprite triggeredSprite;

    [SerializeField]
    private Sprite notTriggeredSprite;

    [SerializeField]
    private bool permanentTrigger = false;

    [SerializeField]
    private float triggeredTime = 1f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
        if (triggerableObject != null)
        {
            triggerableObject.Activate();
        }

        spriteRenderer.sprite = triggeredSprite;

        StartCoroutine(WaitToDeactivate(triggeredTime));
    }

    public void Deactivate()
    {
        if (permanentTrigger == false)
        {
            if (triggerableObject != null)
            {
                triggerableObject.Deactivate();
            }

            spriteRenderer.sprite = notTriggeredSprite;
        }
    }

    private IEnumerator WaitToDeactivate(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Deactivate();
    }
}
