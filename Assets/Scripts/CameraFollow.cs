using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private CharacterStates _states;
    private Inputs _inputs;

    [SerializeField] private float cameraMoveSpeed = 120.0f;
    [SerializeField] private float clampAngle = 80.0f;
    [SerializeField] private float inputSensitivity = 150.0f;
    [SerializeField] private Transform cameraFollowObj;

    private float rotX = 0.0f;
    private float rotY = 0.0f;
    private Transform lockOnTarget;


    // Use this for initialization
    void Start () {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        _states = transform.parent.GetComponent<CharacterStates>();
        _inputs = transform.parent.GetComponent<Inputs>();
    }

    // Update is called once per frame
    void Update() {
        //Get Inputs
        float InputX = _inputs.camX;
        float InputZ = _inputs.camY;
        bool lockOn = _states.lockOn;

        //Camera behavour
        if (lockOn == false) {

            rotY += InputX * inputSensitivity * Time.deltaTime;
            rotX += InputZ * inputSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;

            lockOnTarget = GetComponentInParent<CharacterStates>().nearestTarget;
        } else {
            Vector3 targetDir = lockOnTarget.position - transform.position;
            targetDir.Normalize();
            //targetDir.y = 0;

            if (targetDir == Vector3.zero){
                targetDir = transform.forward;
            }
            Quaternion targetRot = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 9 * Time.deltaTime);

            Vector3 rot = transform.rotation.eulerAngles;
            rotY = rot.y;
            rotX = rot.x;
        }
    }

    void LateUpdate() {
        Transform target = cameraFollowObj;

        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
