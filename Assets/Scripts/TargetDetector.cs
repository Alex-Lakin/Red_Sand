using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour {

    List<Targetable> allTargets;
    [SerializeField] private float maxTargetDist;

    // Use this for initialization
    void Start()
    {
        allTargets = new List<Targetable>();
        
        findAndAddAllTargets(allTargets);
        
        removeTargetFromList(transform.parent.GetComponent<Targetable>(), allTargets);
    }

    // Update is called once per frame
    void Update()
    {
        Targetable closestTarget = findClosestTarget(allTargets);

        if (closestTarget == null){
            setClosestTargetInStates(this.transform);
        } else {
            float distanceToTarget = findDistanceToClosestTarget(closestTarget);

            if (distanceToTarget < maxTargetDist) {
                setClosestTargetInStates(closestTarget.transform);
            } else {
                setClosestTargetInStates(this.transform);
            }
        }
    }

    void findAndAddAllTargets(List<Targetable> listOfTargetables) {
        listOfTargetables.AddRange(GameObject.FindObjectsOfType<Targetable>());
    }

    void removeTargetFromList(Targetable targetToRemove, List<Targetable> listOfTargetables) {
        for (int i = 0; i < listOfTargetables.Count; i++)
        {
            if (listOfTargetables[i] == targetToRemove)
            {
                listOfTargetables.RemoveAt(i);
            }
        }
    }

    Targetable findClosestTarget(List<Targetable> listOfTargetables)
    {
        float distanceToClosestTarget = Mathf.Infinity;
        Targetable closestTarget = null;

        foreach (Targetable currentTarget in listOfTargetables) {
            //Find closest target
            float distanceToCurrentTarget = Vector3.Distance(currentTarget.transform.position, this.transform.position);
            if (distanceToCurrentTarget < distanceToClosestTarget) {
                distanceToClosestTarget = distanceToCurrentTarget;
                closestTarget = currentTarget;
            }
        }
        return closestTarget;
    }

    float findDistanceToClosestTarget(Targetable closestTarget) {
        float dist = Vector3.Distance(closestTarget.transform.position, this.transform.position);
        return dist;
    }

    void setClosestTargetInStates(Transform closestTarget) {
        if (closestTarget != null) {
            transform.parent.GetComponent<CharacterStates>().nearestTarget = closestTarget;
        }
    }
}
