using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PointDetector : MonoBehaviour {

	OVRInput.Controller leftController = OVRInput.Controller.LTouch;
	OVRInput.Controller rightController = OVRInput.Controller.RTouch;
	GameObject[] pointableObjects;

	public WaveColliderScript leftWaveObject;
	public WaveColliderScript rightWaveObject; 

	public GameObject floor;
	public Gaze2 gaze2;


	public Transform bodyPos;

	public GameObject alien; 
	public Animator anim;


	public float armExtendThres = 0.0f;


	// Use this for initialization
	void Start () {
		pointableObjects = GameObject.FindGameObjectsWithTag ("Pointable");
		print (pointableObjects.Length);
		anim = alien.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		//print (gaze2.gazed);

	}





	public GameObject getPointedObject(){
		if (!OVRInput.Get (OVRInput.Touch.PrimaryIndexTrigger, leftController)) {
			//print ("Left finger extended!");

			Vector3 handLeftPosition = OVRInput.GetLocalControllerPosition (OVRInput.Controller.LTouch) + bodyPos.position;
			Quaternion handLeftRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.LTouch);
			Vector3 targetForward = handLeftRotation * Vector3.forward;
			//Debug.Log (handLeftPosition);
			RaycastHit hitinfo;
			//Debug.Log (gaze2.gazed);
			if (Physics.Raycast (handLeftPosition, targetForward, out hitinfo)) {
				GameObject hit = hitinfo.collider.gameObject;
				//print (hit);
				if (pointableObjects.Contains (hit) && hit == gaze2.gazed) {

					//hit.GetComponent<Renderer> ().material.color = new Color (255, 0, 0);
					return hit;
	}


			}






		}

		if (!OVRInput.Get (OVRInput.Touch.PrimaryIndexTrigger, rightController)) {
			//print ("Right finger extended!");


			Vector3 handLeftPosition = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch) + bodyPos.position;
			Quaternion handLeftRotation = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTouch);
			Vector3 targetForward = handLeftRotation * Vector3.forward;




			RaycastHit hitinfo;

			if (Physics.Raycast (handLeftPosition, targetForward, out hitinfo)) {
				GameObject hit = hitinfo.collider.gameObject;
				if (pointableObjects.Contains (hit)  && hit == gaze2.gazed) {

					//hit.GetComponent<Renderer> ().material.color = new Color (0, 0, 255);
					return hit;
				}


			}
		}

		return null;
	}




	bool armExtended(OVRInput.Controller controller){
		Vector3 handPosition = OVRInput.GetLocalControllerPosition (controller);
		//Debug.Log (handPosition.magnitude);
		if (handPosition.magnitude >= armExtendThres)
			return true;

		return false;
	}


}