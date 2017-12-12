using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDetector : MonoBehaviour {

	public ArmExtendedCollider armCollider; 
	OVRInput.Controller leftController = OVRInput.Controller.LTouch;
	OVRInput.Controller rightController = OVRInput.Controller.RTouch;

	public GameObject leftHand;
	public GameObject rightHand; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public bool grabbingFruit(OVRInput.Controller controller){

		GameObject hand = leftHand; 

		if (controller == leftController) {
			hand = leftHand;
		} else
			hand = rightHand; 

		Vector3 handPosition = OVRInput.GetLocalControllerPosition (controller);
		//Debug.Log (handPosition.magnitude);
		if (hand == leftHand) {
			Quaternion handLeftRotation = OVRInput.GetLocalControllerRotation (controller);
			Vector3 targetUp = handLeftRotation * Vector3.up;
			//Debug.Log (targetUp);



			return targetUp.y <= 0.7 && targetUp.y > 0.0 && armCollider.playerLeftHandInside();
		}
		if (hand == rightHand) {
			Quaternion handLeftRotation = OVRInput.GetLocalControllerRotation (controller);
			Vector3 targetUp = handLeftRotation * Vector3.up;
			//Debug.Log (targetUp);



			return targetUp.y <= 0.7 && targetUp.y > 0.0 && armCollider.playerRightHandInside();
		}

		return false;
	}

	public bool grabbingNut(OVRInput.Controller controller){
		GameObject hand = leftHand; 

		if (controller == leftController) {
			hand = leftHand;
		} else
			hand = rightHand; 

		Vector3 handPosition = OVRInput.GetLocalControllerPosition (controller);
		//Debug.Log (handPosition.magnitude);
		if (hand == leftHand) {
			Quaternion handLeftRotation = OVRInput.GetLocalControllerRotation (controller);
			Vector3 targetUp = handLeftRotation * Vector3.up;
			//Debug.Log (targetUp);



			return targetUp.y <= 0.7 && targetUp.y > 0.0 && armCollider.playerLeftHandInside();
		}
		if (hand == rightHand) {
			Quaternion handLeftRotation = OVRInput.GetLocalControllerRotation (controller);
			Vector3 targetUp = handLeftRotation * Vector3.up;
			//Debug.Log (targetUp);



			return targetUp.y <= 0.7 && targetUp.y > 0.0 && armCollider.playerRightHandInside();
		}

		return false;

		return false;
	}
}
