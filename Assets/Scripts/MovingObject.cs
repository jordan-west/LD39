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
        transform.position = endPosition;
    }

    public override void Deactivate()
    {
        transform.position = startPosition;
    }
}
