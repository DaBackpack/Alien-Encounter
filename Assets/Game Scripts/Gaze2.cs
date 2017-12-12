using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gaze2 : MonoBehaviour {

	public GameObject gazed;
	GameObject[] pointableObjects;


	Dictionary<GameObject, bool> stay;
	Dictionary<GameObject, bool> isColliding;


	// Use this for initialization
	void Start () {
		gazed = null;
		pointableObjects = GameObject.FindGameObjectsWithTag ("Pointable");

		stay = new Dictionary<GameObject, bool> ();
		isColliding = new Dictionary<GameObject, bool> ();

		foreach (GameObject x in pointableObjects){
			stay.Add(x, false);
			isColliding.Add(x, false);
		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		gazed = null;

		List<GameObject> keys = stay.Keys.ToList();
		foreach (GameObject pointable in keys) {
			if (stay [pointable] == true) {
				stay [pointable] = false;
				isColliding [pointable] = true;
				//pointable.GetComponent<Renderer> ().material.color = new Color (255, 0, 255);
				gazed = pointable;
				//print (gazed);
			} else {
				isColliding [pointable] = false;
				//pointable.GetComponent<Renderer> ().material.color = new Color (0, 0, 0);
			}
		}
		//print (gazed);
	}

	void OnTriggerStay(Collider c){
		//print ("HERE");
		if (pointableObjects.Contains(c.gameObject)){
			stay [c.gameObject] = true;
		}

	}
}
