using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Button : MonoBehaviour {

    [SerializeField]
    private ATriggerable triggerableObject;

    [SerializeField]
    private Sprite triggeredSprite;

    [SerializeField]
    private Sprite notTriggeredSprite;

    [SerializeField]
    private bool permanentTrigger = false;

    private SpriteRenderer spriteRenderer;

    private int numberOfTriggers = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (numberOfTriggers == 0)
        {
            if (triggerableObject != null)
            {
                triggerableObject.Activate();
            }

            spriteRenderer.sprite = triggeredSprite;
        }

        numberOfTriggers++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (permanentTrigger == false)
        {
            if (numberOfTriggers == 1)
            {
                if (triggerableObject != null)
                {
                    triggerableObject.Deactivate();
                }
                
                spriteRenderer.sprite = notTriggeredSprite;
            }

            numberOfTriggers--;
        }
        
    }
}
