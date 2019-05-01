using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    
    private HitboxManager myManager;
    public float force = 0;
    [SerializeField] public int hitReaction = 1;

    // Use this for initialization
    void Start () {
        myManager = transform.root.gameObject.GetComponent<HitboxManager>();
    }

    private void OnTriggerEnter(Collider collider) {
        HitboxManager colHitBox = collider.GetComponent<HitboxManager>();
        Debug.Log(colHitBox);
        if (colHitBox != null) {
            if (colHitBox != myManager) {
                if (force > 0) {
                    colHitBox.TakeDamage(force, hitReaction);
                }
            }
        }
    }
}
