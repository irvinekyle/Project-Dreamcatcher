using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ViveInputController : MonoBehaviour {

    // 1
    //public new SteamVR_Controller.Device controller;   //Reference for first wand
    public PlatformController platformClassCaller;
    private Hand hand;

    // 2
   // private SteamVR_Controller.Device Controller {
        //get { return SteamVR_Controller.Input((int)controller.index); }
    //}

    void Awake() {
        // controller = GetComponent<SteamVR_TrackedObject>();
        // controller = Controller;
        hand = GetComponent<Hand>();
    }
    // Update is called once per frame
    void Update () {
        if (hand.controller != null) {
            // 1
            if (hand.controller.GetAxis() != Vector2.zero) {
               Debug.Log(gameObject.name + hand.controller.GetAxis());
            }

            // 2
            if (hand.controller.GetHairTriggerDown()) {
                Debug.Log(gameObject.name + " Trigger Press");
            }

            // 3
            if (hand.controller.GetHairTriggerUp()) {
                Debug.Log(gameObject.name + " Trigger Release");
            }

            // 4
            if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
                Debug.Log(gameObject.name + " Grip Press");
                platformClassCaller.triggerPressed(gameObject.name);
            }

            // 5
            if (hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
                Debug.Log(gameObject.name + " Grip Release");
            }
        }
    }
}
