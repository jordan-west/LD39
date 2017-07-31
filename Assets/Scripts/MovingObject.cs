using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : ATriggerable {

    private Vector3 startPosition;

    [SerializeField]
    private Vector3 endPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    public override void Activate()
    {
        if (numberOfTimesActivated == 0)
        {
            activated = true;

            transform.position = endPosition;
        }

        numberOfTimesActivated++;
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
