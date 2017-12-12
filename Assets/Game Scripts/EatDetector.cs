using UnityEngine;
using System.Collections;

public class EatDetector : MonoBehaviour
{

	public EatCollider collider;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public bool detectEat(){
		//if (leftHand.transform.position.x > 1 || rightHand.transform.position.x > 1)
		//	return true;

		return collider.playerHasEaten();
	}
}

