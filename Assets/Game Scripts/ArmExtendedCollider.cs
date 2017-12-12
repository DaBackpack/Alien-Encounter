using UnityEngine;
using System.Collections;

public class ArmExtendedCollider : MonoBehaviour
{
	public GameObject leftHand;
	public GameObject rightHand;

	bool leftHandInside = false;
	bool rightHandInside = false;

	bool leftHandStay = false;
	bool rightHandStay = false;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (leftHandStay == true) {
			leftHandStay = false;
			leftHandInside = true;
		} else {
			leftHandStay = false;
			leftHandInside = false;
		}




		if (rightHandStay == true) {
			rightHandStay = false;
			rightHandInside = true;
		} else {
			rightHandStay = false;
			rightHandInside = false;
		}

	}

	public bool playerLeftHandInside(){
		return leftHandInside;
	}

	public bool playerRightHandInside(){
		return rightHandInside;
	}


	void OnTriggerStay(Collider c){


		if (c.gameObject == leftHand){
			leftHandStay = true;
		}

		if (c.gameObject == rightHand) {
			rightHandStay = true;
		}
	}
}

