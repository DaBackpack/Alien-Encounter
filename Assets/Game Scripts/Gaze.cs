using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gaze : MonoBehaviour {

	public GameObject gazed;
	GameObject[] pointableObjects;

	// Use this for initialization
	void Start () {
		gazed = null;
		pointableObjects = GameObject.FindGameObjectsWithTag ("Alien");

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (gazed);
	}

	void OnTriggerEnter(Collider c){
		//Debug.Log ("hi");

		if (pointableObjects.Contains(c.gameObject)){
		//c.gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 255);
		gazed = c.gameObject;

		}
	}

	void onTriggerExit(Collider c){
		if (c.gameObject == gazed) {
			gazed = null;
			//c.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
		}
	}
}
