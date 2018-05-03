using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlatformController : MonoBehaviour {

    public float speed = 1;
    public float distance;
    public int maxDistance;
    private bool isMoving = false;
    public Transform[] waypoints;
    public int nextIndex;
    private Vector3 targetPosition;
    private Vector3 lookAtTarget;
    public Quaternion playerRot;
    public float rotSpeed = 1;
    NavMeshAgent _navMeshAgent;
    public float minX, maxX;
    public float minZ, maxZ;
    public Vector3 newWaypoint;
    public GameObject wayPointGameObj;

    // Use this for initialization
    void Start () {
        //set player to first target
        transform.position = waypoints[0].position;
        nextIndex = 1;
        SetTargetPosition();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        minX = 0;

    }
    // Update is called once per frame
    void Update () {
        if (speed > 0) isMoving = true;
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
            distance = Vector3.Distance(transform.position, newWaypoint);
            //have we arrived??
            if (distance >= maxDistance) {
                // calculate the next move (step)
                float step = speed * Time.deltaTime;

                //move by step amount
                //transform.position = Vector3.MoveTowards(transform.position, targets[nextIndex].position, step);

                //transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
                //transform.LookAt(targets[nextIndex].position);
                //Get the vector between the transform and the target, calculate the rotation needed 
                //to "look at" it, and interpolate from the current rotation to the desired one
                /* Vector3 relativePos = targets[nextIndex].position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);
                transform.position = Vector3.MoveTowards(transform.position, targets[nextIndex].position, step);
                */

                _navMeshAgent.SetDestination(newWaypoint);
            }
            else {
                nextIndex += 1;
                if (nextIndex >= waypoints.Length) {
                    nextIndex = 0;
                }
                
                // int randWanypoint = new System.Random(0, waypoints.Length);
                // nextIndex  = UnityEngine.Random.Range(0, waypoints.Length);
                //SceneManager.LoadScene(Random.Range(0, SceneManager.sceneCount));
                //newWaypoint = new Vector3(UnityEngine.Random.Range(minX, maxX), 1, UnityEngine.Random.Range(minZ, maxZ));
                SetTargetPosition();
            }
        }
    }
    void SetTargetPosition() {
        //Ray ray = Camera.main.ScreenPointToRay(waypoints[nextIndex].position);
        newWaypoint = new Vector3(UnityEngine.Random.Range(minX, minX+100), 0, UnityEngine.Random.Range(minZ, maxZ));
        Instantiate(wayPointGameObj, newWaypoint, Quaternion.Euler(0, 180, 0));
        minX = newWaypoint.x;   // step the minx range forward
        Ray ray = Camera.main.ScreenPointToRay(newWaypoint);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000)) {
            targetPosition = hit.point;
            lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);
        }
    }
    public void triggerPressed(string callerName) {
        Debug.Log("Trigger pressed from " + callerName);
        if(callerName == "Hand2") {
            speed += 1;
            if(speed >= 10) {
                speed = 10;
            }
        }
        else if(callerName == "Hand1") {
            speed -= 1;
            if (speed < 0) {
                speed = 0;
                _navMeshAgent.speed = speed;
            }
        }
        _navMeshAgent.speed = speed;
    }
}
