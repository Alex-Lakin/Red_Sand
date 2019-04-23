using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacks : MonoBehaviour {

    private Animator _animator;
    private bool comboWindowOpen = false;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Get Imput
        bool a = Input.GetButtonDown("Melee");
        bool b = Input.GetButton("Block");
        

        float isRunning = _animator.GetFloat("Sprint");
        float isLockedOn = _animator.GetFloat("LockOn");
        bool isBlocking = _animator.GetBool("Blocking");



        //Attacking
        if (a && !isBlocking && isRunning == 0) {
            int currentAttack = _animator.GetInteger("Attack");

            if (currentAttack == 0) {
                _animator.SetInteger("Attack", 1);
            } else {
                if (comboWindowOpen) {
                    _animator.SetInteger("Attack", currentAttack+1);
                }
            }
        }

        //Blocking
        if (b && isLockedOn > 0) {
            _animator.SetBool("Blocking", true);
        } else {
            _animator.SetBool("Blocking", false);
        }
    }
    
    void AttackStart() {
        comboWindowOpen = false;
        _animator.applyRootMotion = true;
    }

    void AttackEnd() {
        _animator.SetInteger("Attack", 0);
        _animator.applyRootMotion = false;
        comboWindowOpen = false;
    }

    void ComboWindowOpen()  {
        comboWindowOpen = true;
    }

    void ComboWindowClose() {
        comboWindowOpen = false;
    }
}
