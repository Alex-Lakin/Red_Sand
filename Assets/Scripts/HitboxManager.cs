using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour {
    
    private Animator _animator;
    private CharacterStates _states;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
        _states = GetComponent<CharacterStates>();
    }

    public void TakeDamage(float damage, int hitReact) {
        _states.hp -= damage;
        _animator.SetInteger("Hit Reaction", hitReact);
    }

    public void AttackBlocked(int blockReact) {
        _animator.SetInteger("Block Reaction", blockReact);
    }

    void HitReactionStart() {
        _animator.SetInteger("Attack", 0);
        _animator.SetInteger("Hit Reaction", 0);
        _animator.applyRootMotion = true;
    }

    void HitReactionEnd() {
        _animator.SetInteger("Hit Reaction", 0);
        _animator.SetInteger("Attack", 0);
        _animator.applyRootMotion = false;
    }
}
