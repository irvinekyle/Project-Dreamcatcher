using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ModelMoveController : MonoBehaviour {

    [SerializeField]
    Transform _destination;

    NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Start () {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if(_navMeshAgent == null) {
            Debug.LogError("The nav mesh agent is not attached to " + gameObject.name);
        }
        else {
            setDestination();
        }
	}

    private void setDestination() {
        if(_destination != null) {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    // Update is called once per frame
    void Update () {
        setDestination();
    }
}
