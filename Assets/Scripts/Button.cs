using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    [SerializeField]
    private ATriggerable triggerableObject;

    [SerializeField]
    private bool permenantTrigger = false;

    private float debounceTime = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        triggerableObject.Activate();
        StartCoroutine(Debounce(debounceTime));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (permenantTrigger == false)
            triggerableObject.Deactivate();
    }

    private IEnumerator Debounce(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
