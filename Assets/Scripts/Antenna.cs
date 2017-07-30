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

    private bool triggered = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
        if (!triggered)
        {
            triggerableObject.Activate();
            spriteRenderer.sprite = triggeredSprite;

            triggered = true;

            StartCoroutine(WaitToDeactivate(triggeredTime));
        }
    }

    public void Deactivate()
    {
        if (permanentTrigger == false)
        {
            triggerableObject.Deactivate();
            spriteRenderer.sprite = notTriggeredSprite;

            triggered = false;
        }
    }

    private IEnumerator WaitToDeactivate(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Deactivate();
    }
}
