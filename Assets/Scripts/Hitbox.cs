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
        HitboxManager colHitBox = collider.GetComponent<HitboxManager>();
        if (colHitBox != null) {
            if (colHitBox != myManager) {
                if (force > 0) {
                    colHitBox.TakeDamage(force, hitReaction);
                }
            }
        } else {
            Hitbox hitBox = collider.GetComponent<Hitbox>();
            if (hitBox.isBlocking == true) {
                if (collider.transform.root != transform.root) {
                    if (force > 0) {
                        myManager.AttackBlocked(hitReaction);
                    }
                }
            }
        }
    }
}
