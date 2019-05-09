using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeleeAttack
{
    public float force;
    public int hitReaction;
    public Transform weaponHolder;
}

public class MeleeAttacks : MonoBehaviour {

    private Animator _animator;
    private Inputs _inputs;
    private bool comboWindowOpen = false;
    [SerializeField] private MeleeAttack[] attackList;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
        _inputs = GetComponent<Inputs>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Get Imput
        bool a = _inputs.melee;
        bool b = _inputs.block;
        
        //float isLockedOn = _animator.GetFloat("LockOn");
        bool isBlocking = _animator.GetBool("Blocking");



        //Attacking
        if (a && !isBlocking) {
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
        if (b) {
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

    void DamageWindowOpen(int attacknumber) {
        Hitbox hitBox = attackList[attacknumber].weaponHolder.GetComponentInChildren<Hitbox>();
        hitBox.force = attackList[attacknumber].force;
        hitBox.hitReaction = attackList[attacknumber].hitReaction;
    }

    void DamageWindowClosed(int attacknumber) {
        Hitbox hitBox = attackList[attacknumber].weaponHolder.GetComponentInChildren<Hitbox>();
        hitBox.force = 0;
        hitBox.hitReaction = 0;
    }

    void ComboWindowOpen()  {
        comboWindowOpen = true;
    }

    void ComboWindowClose() {
        comboWindowOpen = false;
    }
}
