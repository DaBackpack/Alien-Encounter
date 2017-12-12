using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveColliderScript : MonoBehaviour {


	public GameObject controller;
	public bool handInsideMe = false;
	Collider cCollider;


	// Use this for initialization
	void Start () {
		cCollider = controller.GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (handInsideMe);
	}
		
	void OnTriggerEnter(Collider c){
		//Debug.Log ("hi");
		if (c == cCollider)
			handInsideMe = true;

	}

	void OnTriggerExit(Collider c){
		if (c == cCollider)
			handInsideMe = false;

	}
}
