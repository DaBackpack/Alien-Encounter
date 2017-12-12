using UnityEngine;
using System.Collections;

public class BadgeGrabDetector : MonoBehaviour
{


	bool lefthandgrabbed = false;
	bool righthandgrabbed = false;
	public GameObject lefthand;
	public GameObject righthand;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerStay(Collider c){
		
		if (c.gameObject == lefthand) {
			lefthandgrabbed = true;
			righthandgrabbed = false;
		}

		if (c.gameObject == righthand) {
			lefthandgrabbed = false;
			righthandgrabbed = true;
		}
	}

	public bool leftHandGrabbed(){
		return lefthandgrabbed;
	}

	public bool rightHandGrabbed(){
		return righthandgrabbed;
	}


}

