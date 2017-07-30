using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATriggerable : MonoBehaviour {

    public abstract void Activate();

    public abstract void Deactivate();

    protected bool activated = false;
    protected int numberOfTimesActivated = 0;

    public bool Activated {
        get {
            return activated;
        }
    }
}
