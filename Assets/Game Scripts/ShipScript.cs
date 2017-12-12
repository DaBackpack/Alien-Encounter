using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;

public class ShipScript : MonoBehaviour {


	float timeSpent = 0.0f;
	public float particleTime = 1.0f;
	public float teleTime = 1.0f;
	bool count = true;
	bool teleTrigger = false;
	public GameObject particles; 

	public AudioSource radioSource;
	public AudioSource teleSource;


	public GameObject cylinder; 
	public float teleportTiming = 4.0f;
	float elapsedTime = 0.0f;

	bool stopLoading = false;

	// Use this for initialization
	void Start () {
		particles.SetActive (false);
		cylinder.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {

		if (!radioSource.isPlaying) {
			timeSpent += Time.deltaTime;
		}





		if (timeSpent >= teleTime && count) {
			count = false;
			teleTrigger = true;
			teleSource.Play ();
		}

		if (timeSpent >= particleTime) {
			particles.SetActive (true);
		}

		if (teleTrigger) {
			cylinder.SetActive (true);
			//Initiate.Fade ("Main", new Color(126.0f/255, 196.0f/255, 255.0f/255), 0.8f);
			Material mat = cylinder.GetComponent<Renderer>().material;
			elapsedTime += Time.deltaTime;	
			mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, elapsedTime / teleportTiming);


			if (!teleSource.isPlaying && !radioSource.isPlaying && !stopLoading) {
				stopLoading = true; 
				StartCoroutine (LoadYourAsyncScene());

			}


		}
	}

	IEnumerator LoadYourAsyncScene(){
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");

		while (!asyncLoad.isDone) {
			yield return null;
		}

	}
}
