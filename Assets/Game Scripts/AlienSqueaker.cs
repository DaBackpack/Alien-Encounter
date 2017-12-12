using UnityEngine;
using System.Collections;

public class AlienSqueaker : MonoBehaviour
{

	Animator anim;

	public AudioSource alienSource;

	AudioClip happySound;
	AudioClip sadSound; 

	float soundReset = 5.0f;
	float currTime = 0.0f; 

	bool soundReady = true;

	// Use this for initialization
	void Start ()
	{
		//alienSource = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		happySound = (AudioClip)Resources.Load ("alien_sound/Kitten Coon");
		sadSound = (AudioClip)Resources.Load ("alien_sound/sad_sound");

		print (sadSound);
	}
	
	// Update is called once per frame
	void Update ()

	{

		updateTimer ();

		AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo (0);
		if ((state.IsName("Sad_Alien")  || state.IsName("Sad_Alien 1")) && soundReady){
			alienSource.Stop ();
			alienSource.PlayOneShot (sadSound);

			soundReady = false;
			currTime = 0.0f;
	}

		if ((state.IsName("Happy_Alien") || state.IsName("Happy_End")) && soundReady){
			alienSource.Stop ();
			alienSource.PlayOneShot (happySound);

			soundReady = false;
			currTime = 0.0f;
		}
	}

	void updateTimer() {
		currTime += Time.deltaTime; 
		if (currTime >= soundReset) {
			soundReady = true; 
			currTime = 0.0f;
		}
	}

}

