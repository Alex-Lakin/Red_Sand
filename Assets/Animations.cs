using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour {

    Animator _animator;
    [SerializeField] int blinkProbability;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        _animator.SetBool("Blink", false);

        //blinking aniamtions
        int blinkChance = Random.Range(0,blinkProbability);
        if (blinkChance == 12)
        {
            _animator.SetBool("Blink",true);
        }
    }
}
