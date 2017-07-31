using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    private static LevelController instance;

    public static LevelController Instance {
        get {
            if (instance == null)
            {
                // Note this can fail...should do proper checking
                instance = FindObjectOfType<LevelController>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Vector3 respawnPoint;
	
    public Vector3 RespawnPoint {
        get {
            return respawnPoint;
        }
        set {
            respawnPoint = value;
        }
    }
}
