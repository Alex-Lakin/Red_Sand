using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDetector : MonoBehaviour {

    Inputs _inputs;
    List<PickUp> allPickUps;
    [SerializeField] private float maxPickUpDist;

    // Use this for initialization
    void Start()
    {
        _inputs = transform.root.GetComponent<Inputs>();
        allPickUps = new List<PickUp>();

        findAndAddAllPickUps(allPickUps);
    }

    // Update is called once per frame
    void Update()
    {
        //Get Imput
        bool p = _inputs.pickUp;

        if (p) {
            PickUp closestPickUp = findClosestPickUp(allPickUps);

            if (closestPickUp != null)
            {
                float distanceToPickUp = findDistanceToClosestPickUp(closestPickUp);

                if (distanceToPickUp < maxPickUpDist)
                {
                    usePickUp(closestPickUp);
                }
            }
        }
    }

    void findAndAddAllPickUps(List<PickUp> listOfPickUps)
    {
        listOfPickUps.AddRange(GameObject.FindObjectsOfType<PickUp>());
    }

    void removePickUpFromList(PickUp pickUpToRemove, List<PickUp> listOfPickUps)
    {
        for (int i = 0; i < listOfPickUps.Count; i++)
        {
            if (listOfPickUps[i] == pickUpToRemove)
            {
                listOfPickUps.RemoveAt(i);
            }
        }
    }

    PickUp findClosestPickUp(List<PickUp> listOfPickUps)
    {
        float distanceToClosestPickUp = Mathf.Infinity;
        PickUp closestPickUp = null;

        foreach (PickUp currentPickUp in listOfPickUps)
        {
            //Find closest PickUp
            float distanceToCurrentPickUp = Vector3.Distance(currentPickUp.transform.position, this.transform.position);
            if (distanceToCurrentPickUp < distanceToClosestPickUp)
            {
                distanceToClosestPickUp = distanceToCurrentPickUp;
                closestPickUp = currentPickUp;
            }
        }
        return closestPickUp;
    }

    float findDistanceToClosestPickUp(PickUp closestPickUp)
    {
        float dist = Vector3.Distance(closestPickUp.transform.position, this.transform.position);
        return dist;
    }

    void usePickUp(PickUp closestPickUp)
    {
        closestPickUp.pickUpAction();
        removePickUpFromList(closestPickUp, allPickUps);
    }
}
