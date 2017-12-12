using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowCollider : MonoBehaviour {


	public Gaze gaze; 
	Collider gazeCollider;

	bool currentlyBowing = false;

	// Use this for initialization
	void Start () {
		gazeCollider = gaze.gameObject.GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c){
		//Debug.Log ("hi");
		if (c == gazeCollider)
			currentlyBowing = true;

	}

	void OnTriggerExit(Collider c){
		if (c == gazeCollider)
			currentlyBowing = false;

	}

	public bool getBowing(){
		//Debug.Log (currentlyBowing);
		return currentlyBowing;
	}
}
