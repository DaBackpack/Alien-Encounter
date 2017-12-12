using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceDirector : MonoBehaviour {

    public GameObject alien; 
    Animator anim;
    public AudioSource radioSource;
    public float timeLength = 10;
    float currTime = 0;
    


	bool refreshSound = false;


    Dictionary<string, AudioClip> audioClips;
	Dictionary<AudioClip, bool> clipPlayed;

	// Use this for initialization
	void Start () {
        anim = alien.GetComponent<Animator>();

        // Populate the dictionary with the proper audio clips. 

		audioClips = new Dictionary<string, AudioClip> ();
		audioClips.Add ("2-1waving initial", (AudioClip) Resources.Load("voice/2-1waving initial"));
		audioClips.Add ("3-1point initial", (AudioClip) Resources.Load("voice/3-1point initial"));
		audioClips.Add ("4-1pickup initial", (AudioClip) Resources.Load("voice/4-1pickup initial"));
		audioClips.Add ("5-1eating initial", (AudioClip) Resources.Load("voice/5-1eating initial"));
		audioClips.Add ("6-1extend initial", (AudioClip) Resources.Load("voice/6-1extend initial"));
		audioClips.Add ("7-1bowing initial", (AudioClip) Resources.Load("voice/7-1bowing initial"));
		audioClips.Add ("beamUp", (AudioClip) Resources.Load("tng_transporter7"));



		audioClips.Add ("2-2waving failed1", (AudioClip) Resources.Load("voice/2-2waving failed1"));
		audioClips.Add ("2-3waving failed 2", (AudioClip) Resources.Load("voice/2-3waving failed 2"));



		clipPlayed = new Dictionary<AudioClip, bool> ();
		foreach (string name in audioClips.Keys) {
			print (audioClips[name]);
			clipPlayed.Add(audioClips[name], false);
		}

	}
	
	// Update is called once per frame
	void Update () {
		playSoundInitially ();

	}

	bool playSoundInitially(){

		AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo (0);
		AudioClip clip = audioClips["2-1waving initial"];


		if (state.IsName("Waving") && !clipPlayed[audioClips ["2-1waving initial"]]){
			radioSource.Stop ();
			clip = audioClips ["2-1waving initial"];
			clipPlayed [clip] = true;
			radioSource.PlayOneShot (clip);
		}

		if (state.IsName("Show_Fruit") && !clipPlayed[audioClips ["3-1point initial"]]){
			radioSource.Stop ();
			clip = audioClips ["3-1point initial"];
			clipPlayed [clip] = true;
			radioSource.PlayOneShot (clip);
		}

		if (state.IsName("Giving_Gift") && !clipPlayed[audioClips ["4-1pickup initial"]]){
			radioSource.Stop ();
			clip = audioClips ["4-1pickup initial"];
			clipPlayed [clip] = true;
			radioSource.PlayOneShot (clip);
		}

		if (state.IsName("Eat_Fruit") && !clipPlayed[audioClips ["5-1eating initial"]]){
			radioSource.Stop ();
			clip = audioClips ["5-1eating initial"];
			clipPlayed [clip] = true;
			radioSource.PlayOneShot (clip);
		}

		if (state.IsName("Expect_Gift") && !clipPlayed[audioClips ["6-1extend initial"]]){
			radioSource.Stop ();
			clip = audioClips ["6-1extend initial"];
			clipPlayed [clip] = true;
			radioSource.PlayOneShot (clip);
		}

		if (state.IsName("Bow_Middle") && !clipPlayed[audioClips ["7-1bowing initial"]]){
			radioSource.Stop ();
			clip = audioClips ["7-1bowing initial"];
			clipPlayed [clip] = true;
			radioSource.PlayOneShot (clip);
		}

		if (state.IsName("Happy_End") && !clipPlayed[audioClips ["beamUp"]]){
			radioSource.Stop ();
			clip = audioClips ["beamUp"];
			clipPlayed [clip] = true;
			radioSource.PlayOneShot (clip);
		}

	

		//StartCoroutine (PlayAndWait(clip));


	
		return true;
    }

	IEnumerator PlayAndWait(AudioClip clip){


		radioSource.PlayOneShot (clip);
		yield return new WaitForSeconds (clip.length);
	}
		
}
