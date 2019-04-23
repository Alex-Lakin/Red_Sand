using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    private Animator _animator;
    private CharacterController _characterController;
    private CharacterStates _states;
    //Movement
    [SerializeField] private float Speed;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private float Gravity = 20f;
    private Vector3 _moveDir = Vector3.zero;
    private float sprint = 0f;
    private bool movementLock = false;
    //Lockon
    private Transform lockOnTarget;
    private float smoothLockOn = 0f;

    // Use this for initialization
    void Start() {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _states = GetComponent<CharacterStates>();
    }

    // Update is called once per frame
    void Update()
    {
        //get imput
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool lockOn = _states.lockOn;

        //calculate the forwards vector
        Camera _cam = GetComponentInChildren<Camera>();
        Vector3 camForward_Dir = Vector3.Scale(_cam.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = Vector3.zero;
        move = v * camForward_Dir + h * _cam.transform.right;

        if (move.magnitude > 1f) move.Normalize();

        if (lockOn == true) {
            if (smoothLockOn < 1) {
                smoothLockOn += 1 * Time.deltaTime;
            }
        } else {
            if (smoothLockOn > 0) {
                smoothLockOn -= 1 * Time.deltaTime;
            }
            lockOnTarget = GetComponentInParent<CharacterStates>().nearestTarget;
        }
        _animator.SetFloat("LockOn", smoothLockOn);

        // calculate the rotation for the player
        if (!lockOn) {
            move = transform.InverseTransformDirection(move);
        }

        float turnAmount = Mathf.Atan2(move.x, move.z);

        if (!lockOn) {
            transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);
        } else {
            Vector3 targetDir = lockOnTarget.position - transform.position;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero) {
                targetDir = transform.forward;
            }
            Quaternion targetRot = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, (RotationSpeed / 20) * Time.deltaTime);
        }

        //detect and set grounded
        _states.grounded = _characterController.isGrounded;

        //Apply Movement and Rotation
        if (_states.grounded) {

            _animator.SetBool("Grounded", true);

            if (!lockOn) {
                _animator.SetFloat("Move", move.magnitude);
                _animator.SetFloat("Turn", turnAmount);
                _moveDir = transform.forward * move.magnitude;
            } else {
                _animator.SetFloat("Move", v);
                _animator.SetFloat("Turn", h);
                _moveDir = move;
            }

            _moveDir *= Speed;

        } else {
            _animator.SetBool("Grounded", false);
        }


        //Apply Gravity
        _moveDir.y -= Gravity * Time.deltaTime;

        if (!movementLock) {
            _characterController.Move(_moveDir * Time.deltaTime);
        }
    }

    void lockMovement() {
        movementLock = true;
    }

    void unlockMovement() {
        movementLock = false;
    }
}