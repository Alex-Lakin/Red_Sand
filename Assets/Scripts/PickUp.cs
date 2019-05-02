using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private Transform transformToParentInstanceTo;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pickUpAction()
    {
        Instantiate(prefabToInstantiate, transformToParentInstanceTo);
        Destroy(gameObject);
    }
}
