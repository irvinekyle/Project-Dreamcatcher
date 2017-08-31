using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAnimator : MonoBehaviour {

    Animator modelAnim;
    PlatformController vrPlatform;
    float animSpeed;
	// Use this for initialization
	void Start () {
        modelAnim = GetComponent<Animator>();
        vrPlatform = GetComponent<PlatformController>();
	}
	
	// Update is called once per frame
	void Update () {
        animSpeed = vrPlatform.speed;
        modelAnim.SetFloat("Speed", animSpeed);
	}
}
