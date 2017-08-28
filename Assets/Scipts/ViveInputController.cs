using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveInputController : MonoBehaviour {

    // 1
    public SteamVR_Controller.Device controller;   //Reference for first wand
    public PlatformController platformClassCaller;
    public Valve.VR.InteractionSystem.Hand otherHand;
    public Valve.VR.InteractionSystem.Hand.HandType startingHandType;

    // 2
   // private SteamVR_Controller.Device Controller {
        //get { return SteamVR_Controller.Input((int)controller.index); }
    //}

    void Awake() {
        // controller = GetComponent<SteamVR_TrackedObject>();
        // controller = Controller;
    }
    // Update is called once per frame
    void Update () {
        if (controller != null) {
            // 1
            if (controller.GetAxis() != Vector2.zero) {
               // Debug.Log(gameObject.name + Controller.GetAxis());
            }

            // 2
            if (controller.GetHairTriggerDown()) {
                Debug.Log(gameObject.name + " Trigger Press");
            }

            // 3
            if (controller.GetHairTriggerUp()) {
                Debug.Log(gameObject.name + " Trigger Release");
            }

            // 4
            if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
                Debug.Log(gameObject.name + " Grip Press");
                platformClassCaller.triggerPressed();
            }

            // 5
            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
                Debug.Log(gameObject.name + " Grip Release");
            }
        }
    }

    IEnumerator Start() {


        //Debug.Log( "Hand - initializing connection routine" );

        // Acquire the correct device index for the hand we want to be
        // Also for the other hand if we get there first
        while (true) {
            // Don't need to run this every frame
            yield return new WaitForSeconds(1.0f);

            // We have a controller now, break out of the loop!
            if (controller != null)
                break;

            //Debug.Log( "Hand - checking controllers..." );

            // Initialize both hands simultaneously
            if (startingHandType == Valve.VR.InteractionSystem.Hand.HandType.Left || startingHandType == Valve.VR.InteractionSystem.Hand.HandType.Right) {
                // Left/right relationship.
                // Wait until we have a clear unique left-right relationship to initialize.
                int leftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
                int rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
                if (leftIndex == -1 || rightIndex == -1 || leftIndex == rightIndex) {
                    //Debug.Log( string.Format( "...Left/right hand relationship not yet established: leftIndex={0}, rightIndex={1}", leftIndex, rightIndex ) );
                    continue;
                }

                int myIndex = (startingHandType == Valve.VR.InteractionSystem.Hand.HandType.Right) ? rightIndex : leftIndex;
                int otherIndex = (startingHandType == Valve.VR.InteractionSystem.Hand.HandType.Right) ? leftIndex : rightIndex;

                InitController(myIndex);
                if (otherHand) {
                    this.InitController(otherIndex);
                }
            }
            else {
                // No left/right relationship. Just wait for a connection

                var vr = SteamVR.instance;
                for (int i = 0; i < Valve.VR.OpenVR.k_unMaxTrackedDeviceCount; i++) {
                    if (vr.hmd.GetTrackedDeviceClass((uint)i) != Valve.VR.ETrackedDeviceClass.Controller) {
                        //Debug.Log( string.Format( "Hand - device {0} is not a controller", i ) );
                        continue;
                    }

                    var device = SteamVR_Controller.Input(i);
                    if (!device.valid) {
                        //Debug.Log( string.Format( "Hand - device {0} is not valid", i ) );
                        continue;
                    }

                    if ((otherHand != null) && (otherHand.controller != null)) {
                        // Other hand is using this index, so we cannot use it.
                        if (i == (int)otherHand.controller.index) {
                            //Debug.Log( string.Format( "Hand - device {0} is owned by the other hand", i ) );
                            continue;
                        }
                    }

                    InitController(i);
                }
            }
        }
    }

    private void InitController(int myIndex) {
        if (controller == null) {
            controller = SteamVR_Controller.Input(myIndex);
        }
    }
}
