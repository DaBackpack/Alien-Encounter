using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDetector : MonoBehaviour {

	OVRInput.Controller leftController = OVRInput.Controller.LTouch;
	OVRInput.Controller rightController = OVRInput.Controller.RTouch;

	public ArmExtendedCollider armCollider;



	public float tolerance = 8.0f;
	public float gestureTime = 0.0f;

	public float gestureDoneFor = 0.0f;
	public float gestureCompletionTime = 5.0f;

	bool leftArmDone = false;
	bool rightArmDone = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		updateGestureStatus ();
	}

	public bool playerIsRudeGiving(){

		if (leftArmDone != rightArmDone && (gestureTime >= tolerance)) {
			leftArmDone = false;
			rightArmDone = false;
			gestureTime = 0.0f;
			gestureDoneFor = 0.0f;
			return true;
		}

		return false;
	}

	public bool playerIsPoliteGiving(){
		return leftArmDone == true && rightArmDone == true && gestureDoneFor >= gestureCompletionTime; 
	}

	bool palmUp(OVRInput.Controller controller){
		Vector3 handPosition = OVRInput.GetLocalControllerPosition (controller);
		//Debug.Log (handPosition.magnitude);
	//	if (handPosition.magnitude >= armExtendThres) {
			Quaternion handLeftRotation = OVRInput.GetLocalControllerRotation (controller);
			Vector3 targetUp = handLeftRotation * Vector3.up;
			//Debug.Log (targetUp);



			return targetUp.y <= 0.7 && targetUp.y > 0.0;

	//	}

		//return false;
	}

	void updateGestureStatus(){

		if (palmUp (leftController) && gestureTime == 0.0f && armCollider.playerLeftHandInside ()) {
			leftArmDone = true;
			gestureTime += Time.deltaTime;
			gestureDoneFor += Time.deltaTime;

		} 


		if (palmUp (rightController) && gestureTime == 0.0f && armCollider.playerRightHandInside()) {
			rightArmDone = true;
			gestureTime += Time.deltaTime;
			gestureDoneFor += Time.deltaTime;
		}

		if (palmUp (rightController) && gestureTime != 0.0f && armCollider.playerRightHandInside ()) {
			rightArmDone = true;
			gestureTime += Time.deltaTime;
			gestureDoneFor += Time.deltaTime;
		} 

		if (palmUp (leftController) && gestureTime != 0.0f && armCollider.playerLeftHandInside()) {
			leftArmDone = true;
			gestureTime += Time.deltaTime;
			gestureDoneFor += Time.deltaTime;
		}

		if (!palmUp (leftController) || !armCollider.playerLeftHandInside () || !palmUp (rightController) || !armCollider.playerRightHandInside ()) {
			//gestureDoneFor = 0.0f;
			//print ("HERE");
		}

	}
		
}
