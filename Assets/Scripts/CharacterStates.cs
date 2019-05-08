using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStates : MonoBehaviour {

    public float hp;
    [HideInInspector] public bool grounded = false;
    [HideInInspector] public bool lockOn = false;
    [HideInInspector] public Transform nearestTarget;

    private Inputs _inputs;

    private void Start() {
        _inputs = GetComponent<Inputs>();
    }

    // Update is called once per frame
    void Update () {
        bool l = _inputs.lockOn;
        
        //enter/exit lockon mode
        if (l) {
            if (!lockOn) { lockOn = true; }
        } else {
            lockOn = false;
        }
    }
}
