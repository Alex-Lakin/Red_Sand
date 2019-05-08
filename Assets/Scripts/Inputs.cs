using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour {

    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    [HideInInspector] public bool melee;
    [HideInInspector] public bool block;
    [HideInInspector] public bool lockOn;
    [HideInInspector] public bool pickUp;
    [HideInInspector] public float camX;
    [HideInInspector] public float camY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        melee = Input.GetButtonDown("Melee");
        block = Input.GetButton("Block");
        lockOn = Input.GetButton("LockOn");
        pickUp = Input.GetButtonDown("PickUp");
        camX = Input.GetAxis("Mouse X");
        camY = Input.GetAxis("Mouse Y");
    }
}
