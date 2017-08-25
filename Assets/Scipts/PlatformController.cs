using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public float speed = 1;
    private bool isMoving = false;
    public Transform[] targets;
    private int nextIndex;

	// Use this for initialization
	void Start () {
        //set player to first target
        transform.position = targets[0].position;
        nextIndex = 1;

    }
	
	// Update is called once per frame
	void Update () {
        HandleMovement();
        HandleInput();
    }

    private void HandleInput() {
        //
        if (Input.GetButtonDown("Fire1")) {
            //Debug.Log("Fire1 triggered");
            isMoving = !isMoving;
        }
    }
    void HandleMovement() {

        if (!isMoving) {
            return;
        }
        else {
            //calculate distance from target
            float distance = Vector3.Distance(transform.position, targets[nextIndex].position);
            //have we arrived??
            if (distance > 0) {
                // calculate the next move (step)
                float step = speed * Time.deltaTime;

                //move by step amount
                transform.position = Vector3.MoveTowards(transform.position, targets[nextIndex].position, step);
            }
            else {
                nextIndex += 1;
                if (nextIndex >= targets.Length) {
                    nextIndex = 0;
                }
            }
        }
    }
}
