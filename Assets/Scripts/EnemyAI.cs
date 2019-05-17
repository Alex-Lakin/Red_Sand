using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    private Animator _animator;
    private CharacterController _characterController;
    private Transform target;

    [SerializeField] private float speed;
    private float moveSpeed = 0f;
    [SerializeField] private float attackDistance;
    [SerializeField] private float Gravity = 20f;
    private bool movementLock = false;

    private Vector3 move;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        move = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        float distToTarget = Vector3.Distance(transform.position, target.position);

        transform.LookAt(target);

        if (distToTarget > attackDistance) {
            if (moveSpeed < speed) {
                moveSpeed += 0.1f;
            } else if (moveSpeed > speed) {
                moveSpeed = speed;
            }
        } else {
            if (moveSpeed > 0f) {
                moveSpeed -= 0.1f;
            } else if (moveSpeed < 0f) {
                moveSpeed = 0f;
            }

            int randAttack = Random.Range(0,100);
            if (randAttack == 3)
            _animator.SetInteger("Attack", 1);
        }

        if (movementLock == false) {
            move = transform.forward * moveSpeed;
            _characterController.Move(move * Time.deltaTime);
        }

        if (move.magnitude > 1f) move.Normalize();
        _animator.SetFloat("Move",move.magnitude);
        
        //Apply Gravity
        move.y -= Gravity * Time.deltaTime;
    }

    void lockMovement() {
        movementLock = true;
    }

    void unlockMovement() {
        movementLock = false;
    }
}
