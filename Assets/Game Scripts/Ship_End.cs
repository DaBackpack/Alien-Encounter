using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ship_End : MonoBehaviour {

	public GameObject cylinder;
	public GameObject particles; 
	public AudioSource teleSource;
	public AudioSource radioSource; 

	public GameObject root; 

	public float loadInTime = 4.0f;
	float entryTime = 0.0f;

	float exitTime = 0.0f;
	float endGameTime = 4.0f;

	bool entryDone = false; 
	bool soundPlayed = false;
	bool sceneTrans =false;


	bool warpSoundPlayed = false;
	AudioClip warp;

	// Use this for initialization
	void Start () {
		cylinder.SetActive (true);
		particles.SetActive (true);

		teleSource.Play ();

		warp = (AudioClip) Resources.Load ("tmp_warp_clean");

	}
	
	// Update is called once per frame
	void Update () {



		if (entryTime < loadInTime){
			Material mat = cylinder.GetComponent<Renderer>().material;
			entryTime += Time.deltaTime;
			mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1.0f - entryTime / loadInTime);


		}



		if (entryTime >= loadInTime && !entryDone) {
			cylinder.SetActive (false);
			particles.SetActive (false);
			entryDone = true;


		}

		if (entryDone && !soundPlayed) {

			radioSource.Play ();
			soundPlayed = true;
		}

		if (soundPlayed && !radioSource.isPlaying && !sceneTrans) {
			exitTime += Time.deltaTime;

			cylinder.SetActive (true);

			if (exitTime < endGameTime){
				Material mat = cylinder.GetComponent<Renderer>().material;
				entryTime += Time.deltaTime;
				mat.color = new Color(0, 0, 0, exitTime / endGameTime);


			}

			if (!warpSoundPlayed) {
				teleSource.PlayOneShot (warp);
				warpSoundPlayed = true;

			}



			if (exitTime >= endGameTime && !sceneTrans) {

				Material mat = cylinder.GetComponent<Renderer>().material;
				sceneTrans = true;
				mat.color = new Color(0, 0, 0, 1.0f);
				StartCoroutine (LoadYourAsyncScene ());

			}


		}


	}


	IEnumerator LoadYourAsyncScene(){
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BlackScene");
		DontDestroyOnLoad (root);
		while (!asyncLoad.isDone) {
			yield return null;
		}

	}
}
