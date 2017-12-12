using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class WaveDetector : MonoBehaviour {

	OVRInput.Controller leftController = OVRInput.Controller.LTouch;
	OVRInput.Controller rightController = OVRInput.Controller.RTouch;


	public WaveColliderScript leftWaveObject;
	public WaveColliderScript rightWaveObject; 

	public GameObject floor;
	public Gaze gaze;

	public Transform bodyPos;


	public GameObject alien; 
	public Animator anim;

	// WAVING VARIABLES
	int totalWaveEndpoints_left = 0;
	int totalWaveEndpoints_right = 0;
	public int minWaveEndpoints = 12; 

	public float waveExpireTime = 3.0f;
	public float waveRunOut = 1.5f;
	float waveStart_left = -1.0f;
	float waveStart_right = -1.0f;


	// 1 -> left wave, 2 -> right wave, -1 -> none
	int leftOrRightLastUsed_left = -1;
	int leftOrRightLastUsed_right = -1;




	// Use this for initialization
	void Start () {
		anim = alien.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public GameObject getWavedObject(OVRInput.Controller controller){
		

		if (detectWave (controller) == false)
			return null;

	
		//Debug.Log (gaze.gazed);
		if (gaze.gazed != null)
			//Debug.Log ("WAVING DETECTED");

		if (gaze.gazed != null && gaze.gazed == alien) {

			//Debug.Log ("Wave complete!");

			return gaze.gazed;

		}


		return null;
	}

	bool detectLeftWaveMotion_left(OVRInput.Controller controller){
		if (leftWaveObject.handInsideMe == true) {
			//Debug.Log ("LeftHand_Left");
			return true;

		}

		return false;

	}

	bool detectRightWaveMotion_left(OVRInput.Controller controller){
		if (leftWaveObject.handInsideMe == false) {
			//Debug.Log ("LeftHand_Right");
			return true;

		}

		return false;

	}


	bool detectLeftWaveMotion_right(OVRInput.Controller controller){
		if (rightWaveObject.handInsideMe == true) {
			//Debug.Log ("RightHand_Right");
			return true;

		}

		return false;

	}

	bool detectRightWaveMotion_right(OVRInput.Controller controller){
		if (rightWaveObject.handInsideMe == false) {
			//Debug.Log ("RightHand_Left");
			return true;

		}

		return false;

	}

	bool updateWaveStatus_left(OVRInput.Controller controller){

		detectWaveExpire_left (controller);


		if (detectRightWaveMotion_left (controller) && (leftOrRightLastUsed_left != 2)) {
			leftOrRightLastUsed_left = 2;
			totalWaveEndpoints_left += 1;

		}

		else if (detectLeftWaveMotion_left (controller) && (leftOrRightLastUsed_left != 1)) {
			leftOrRightLastUsed_left = 1;
			totalWaveEndpoints_left += 1;

		}

		// If wave is just starting, start the timer. 

		if (totalWaveEndpoints_left == 1) {
			waveStart_left = Time.time;

		}

		return true;

	}


	bool updateWaveStatus_right(OVRInput.Controller controller){

		detectWaveExpire_right (controller);


		if (detectRightWaveMotion_right (controller) && (leftOrRightLastUsed_right != 2)) {
			leftOrRightLastUsed_right = 2;
			totalWaveEndpoints_right += 1;

		}

		else if (detectLeftWaveMotion_right (controller) && (leftOrRightLastUsed_right != 1)) {
			leftOrRightLastUsed_right = 1;
			totalWaveEndpoints_right += 1;

		}

		// If wave is just starting, start the timer. 

		if (totalWaveEndpoints_right == 1) {
			waveStart_right = Time.time;

		}

		return true;

	}


	bool detectWaveExpire_left(OVRInput.Controller controller){
		//Debug.Log ("Start: " + waveStart + "  Now: " + Time.time);
		if (Time.time - waveStart_left > waveExpireTime) {
			//Debug.Log ("Wave not detected.");
			//floor.GetComponent<Renderer> ().material.color = white;
			totalWaveEndpoints_left = 0;
			leftOrRightLastUsed_left = -1;
			return true;
		}


		return false;
	}

	bool detectWaveExpire_right(OVRInput.Controller controller){
		//Debug.Log ("Start: " + waveStart + "  Now: " + Time.time);
		if (Time.time - waveStart_right > waveExpireTime) {
			//Debug.Log ("Wave not detected.");
			//floor.GetComponent<Renderer> ().material.color = white;
			totalWaveEndpoints_right = 0;
			leftOrRightLastUsed_right = -1;
			return true;
		}


		return false;
	}

	bool detectWave(OVRInput.Controller controller){
		updateWaveStatus_left (controller);
		updateWaveStatus_right (controller);



		return totalWaveEndpoints_left >= minWaveEndpoints || totalWaveEndpoints_right >= minWaveEndpoints;
	}


}
