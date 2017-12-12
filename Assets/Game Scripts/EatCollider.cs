using UnityEngine;
using System.Collections;

public class EatCollider : MonoBehaviour
{

	public GameObject fruit;
	public GameObject nut;

	bool hasEaten = false;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerStay(Collider c){
		if (c.gameObject == fruit || c.gameObject == nut) {
			hasEaten = true;
		}
	}

	public bool playerHasEaten(){
		return hasEaten;
	}
}

