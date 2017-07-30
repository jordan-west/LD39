using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : ATriggerable {

    [SerializeField]
    private Vector3 startPosition;

    [SerializeField]
    private Vector3 endPosition;

    public override void Activate()
    {
        activated = true;
        numberOfTimesActivated++;

        transform.position = endPosition;
    }

    public override void Deactivate()
    {
        numberOfTimesActivated--;

        if (numberOfTimesActivated <= 0)
        {
            activated = false;
            numberOfTimesActivated = 0;

            transform.position = startPosition;
        }
    }
}
