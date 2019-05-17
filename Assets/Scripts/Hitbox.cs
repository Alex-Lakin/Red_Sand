using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    
    private HitboxManager myManager;
    public float force = 0;
    public int hitReaction = 0;
    public bool isBlocking = false;

    // Use this for initialization
    void Start () {
        myManager = transform.root.gameObject.GetComponent<HitboxManager>();
    }

    private void OnTriggerEnter(Collider collider) {
        Hitbox hitBox = collider.GetComponent<Hitbox>();
        if (hitBox != null) {
            if (hitBox.myManager != myManager) {
                if (force > 0) {
                    if (!hitBox.isBlocking) {
                        hitBox.myManager.TakeDamage(force, hitReaction);
                    } else {
                        myManager.AttackBlocked(hitReaction);
                    }
                }
            }
        }
    }
}
