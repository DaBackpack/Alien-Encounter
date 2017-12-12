using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowDetector : MonoBehaviour {

	OVRInput.Controller leftController = OVRInput.Controller.LTouch;
	OVRInput.Controller rightController = OVRInput.Controller.RTouch;
	GameObject[] pointableObjects;

	public BowCollider bowColliderObject;

	public GameObject floor;

	public GameObject alien; 




	float bowStart = -1;
	float bowEnd = -1; 

	bool inBow;
	public float bowLength = 1.3f; 


	// Use this for initialization
	void Start () {
		pointableObjects = GameObject.FindGameObjectsWithTag ("Alien");
		inBow = false;

	}
	
	// Update is called once per frame
	void Update () {
		updateBow ();
	}


	void updateBow(){
		if (!inBow && bowColliderObject.getBowing()) {
			inBow = true; 
			bowStart = Time.time;
		}

		if (inBow && !bowColliderObject.getBowing ()) {
			inBow = false;
			bowEnd = Time.time;
		}
	}


	// Player not currently in bow, but bow completed. 
	public bool playerCompletedBow(){
		if (!inBow) {
			if (bowEnd - bowStart >= bowLength){
				bowStart = -1;
				bowEnd = -1;
				Debug.Log ("BOW DETECTED");
				return true;
			}
		}

		return false;

	}


}
