using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Scene_Script : MonoBehaviour {

	OVRInput.Controller leftController = OVRInput.Controller.LTouch;
	OVRInput.Controller rightController = OVRInput.Controller.RTouch;

	public BowDetector bowDetector; 
	public PointDetector pointDetector;
	public WaveDetector waveDetector;
	public GiveDetector giveDetector;
	public ReceiveDetector receiveDetector; 
	public EatDetector eatDetector; 
	public Gaze2 gaze2;

	public GameObject alien; 
	public GameObject nut;
	public GameObject fruit;
	public ParticleSystem nutSparkle;
	public ParticleSystem fruitSparkle; 


	Animator anim; 

	bool loadScene = false;

	public ParticleSystem particle;

	/*
	 * PLAYER ACTION SENSOR VARIABLES
	 */

	bool isWaving = false;

	bool isPointingFruit = false;
	bool isPointingNut = false;
	bool pickedFruit = false;
	bool pickedNut = false;

	bool isBowing = false;

	bool isPoliteGiving = false;
	bool isPoliteReceiving = false;

	bool isRudeGiving = false;
	bool isRudeReceiving = false;

	bool droppedFruit = false; 
	bool isEating = true;

	// Fuck this
	public GameObject grabNut;
	public GameObject grabFruit;
	bool fruitIsChild = false;
	bool nutIsChild = false;


	/*
	 * GAME STATE VARIABLES. 
	*/

	bool alienExpectWave = false;
	bool alienExpectPoint = false;
	bool alienExpectBow = false;
	bool alienExpectGiving = false;
	bool alienExpectReceiving = false; 
	bool alienExpectEat = false; 
	bool alienExpectGrabBadge = false;

	int numberOfFailures = 0;

	public GameObject leftAlienHand;
	public GameObject rightAlienHand;
	public GameObject leftHumanHand; 
	public GameObject rightHumanHand; 

	public GameObject transponder; 
	public GameObject innerTransponder;
	public GameObject outerTransponder; 

	public Material outerMaterialFade;
	public Material innerMaterialFade;
	public Material outerMaterialOpaque;
	public Material innerMaterialOpaque;


	public AudioSource teleSource;

	public GameObject cylinder;
	bool cylinderFading = true;
	float cylinderElapsedTime = 0.0f;
	float cylinderDisappearTime = 4.0f;

	float cylinderReappearTime = 0.0f;
	float cylinderAppearTime = 4.0f;



	public BadgeGrabDetector badgeCollider;
	bool badgeGrabbed = false;

	public GameObject beam; 
	public AudioSource beamSource;
	bool warpSoundPlayed = false;


	public float beamVelocity = 1.0f;
	bool beamComing = false;

	public float transponderFadeInTime = 7.0f; 
	float transponderLoadTime = 0.0f; 

	// Use this for initialization
	void Start () {
		anim = alien.GetComponent<Animator> ();
		transponder.SetActive (false);
		badgeCollider.gameObject.SetActive (false);
		beam.SetActive (false);


		teleSource.Play ();


	}


	// Update is called once per frame
	void Update () {
		
		//Debug.Log (transform.TransformPoint (new Vector3 (hand.transform.position.x, hand.transform.position.y, 0)));

		AnimatorStateInfo info  = anim.GetCurrentAnimatorStateInfo (0);

		// Reset game if "R" is hit. 
		/*
		if (Input.GetKeyDown (KeyCode.R)) {
			Debug.Log ("Reset!");
			anim.SetTrigger ("Reset");
			pickedNut = false;
			pickedFruit = false;
		}



*/

		if (cylinderFading) {
			cylinder.SetActive (true);
			//Initiate.Fade ("Main", new Color(126.0f/255, 196.0f/255, 255.0f/255), 0.8f);
			Material mat = cylinder.GetComponent<Renderer>().material;
			cylinderElapsedTime += Time.deltaTime;	
			mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1.0f - cylinderElapsedTime / cylinderDisappearTime);

			if (cylinderElapsedTime / cylinderDisappearTime >= 1.0f){
				cylinderFading = false;
				cylinder.SetActive (false);
			}


		}

		if (!cylinderFading && info.IsName ("Idle")) {
			//Debug.Log ("Code start!");
			anim.SetTrigger ("Alien_Start_Wave");
			alienExpectWave = true;

			//anim.SetTrigger ("Player_Polite_Give");
			//alienExpectBow = true;
		}



		// Sensor data... is player waving? Bowing? And update PLAYER variables accordingly.
		updateSensorData ();

		// Update game state according to new sensor data. Set new animations accordingly. 
		updateGameState ();


		// Reset values after each loop.
		resetState();

	}

	void updateSensorData(){

		GameObject leftWaved = waveDetector.getWavedObject (leftController);
		GameObject rightWaved = waveDetector.getWavedObject (rightController);


	

		if (leftWaved == alien || rightWaved == alien) {
			isWaving = true;

		}

		GameObject pointedObject = pointDetector.getPointedObject ();
		//print (pointedObject);

		if (pointedObject == nut) {
			isPointingNut = true;
		}

		if (pointedObject == fruit) {
			isPointingFruit = true;
		}

		if (bowDetector.playerCompletedBow () == true) {
			isBowing = true;
		}


		if (receiveDetector.grabbingNut (leftController) && alienExpectReceiving && pickedNut) {

			grabNut.transform.parent = leftHumanHand.gameObject.transform;

			grabNut.transform.localPosition = new Vector3 (0.000f, -0.000f, -0.010f);
			grabNut.transform.localScale = new Vector3 (4.0f, 4.0f, 4.0f);
			isPoliteReceiving = true;
			alienExpectReceiving = false;
			pickedNut = false;


		}

		if (receiveDetector.grabbingFruit (leftController) && alienExpectReceiving && pickedFruit) {
			grabFruit.transform.parent = leftHumanHand.gameObject.transform;

			grabFruit.transform.localPosition = new Vector3 (0.0100f, -0.0200f, -0.0100f);
			grabFruit.transform.localScale = new Vector3 (.25f, .25f, .25f);
			isPoliteReceiving = true;
			alienExpectReceiving = false;
			pickedFruit = false;

		}


		if (receiveDetector.grabbingNut (rightController) && alienExpectReceiving && pickedNut) {

			grabNut.transform.parent = rightHumanHand.gameObject.transform;

			grabNut.transform.localPosition = new Vector3 (0.000f, -0.000f, -0.010f);
			grabNut.transform.localScale = new Vector3 (4.0f, 4.0f, 4.0f);
			isPoliteReceiving = true;
			alienExpectReceiving = false;
			pickedNut = false;

		}

		if (receiveDetector.grabbingFruit (rightController) && alienExpectReceiving && pickedFruit) {
			grabFruit.transform.parent = rightHumanHand.gameObject.transform;

			grabFruit.transform.localPosition = new Vector3 (0.0100f, -0.0200f, -0.0100f);
			grabFruit.transform.localScale = new Vector3 (.25f, .25f, .25f);
			isPoliteReceiving = true;
			alienExpectReceiving = false;
			pickedFruit = false;

		}

		if (eatDetector.detectEat ()) {
			isEating = true;
		}

		if (giveDetector.playerIsPoliteGiving () && alienExpectGiving) {
			
			isPoliteGiving = true;
		}

		if (giveDetector.playerIsRudeGiving () && alienExpectGiving) {
			isRudeGiving = true; 
		}


	}

	void updateGameState(){



		// Starting animations if conditions are met. 
		if (isWaving && alienExpectWave) {
			alienExpectWave = false;
			anim.SetTrigger ("Player_Wave_Success");
		}

		if (isPointingFruit && alienExpectPoint) {
			//ALIEN GRAB FRUIT
			//Debug.Log("POINTED FRUTI");
			alienExpectPoint = false;
			pickedFruit = true;
			pickedNut = false;
			anim.SetTrigger ("Player_Point_Fruit");
			anim.SetBool ("Player_Pointed_Success", true);
		}

		if (isPointingNut && alienExpectPoint) {
			//print ("POINTED NUT");
			//ALIEN GRAB NUT 
			alienExpectPoint = false;
			pickedNut = true;
			pickedFruit = false;
			anim.SetTrigger ("Player_Point_Nut");
			anim.SetBool ("Player_Pointed_Success", true);
		}

		if (isBowing && alienExpectBow) {
			alienExpectBow = false;
			anim.SetTrigger ("Player_Bow_Success");
		}

		if (isRudeGiving && alienExpectGiving) {
			isRudeGiving = false;
			anim.SetTrigger ("Player_Rude_Give");
		}
	

		if (isRudeReceiving && alienExpectReceiving) {
			isRudeReceiving = false;
			fruitIsChild = false;
			nutIsChild = false;

			anim.SetTrigger ("Player_Rude_Accept");
		}


		if (isPoliteReceiving) {
			isPoliteReceiving = false;
			fruitIsChild = false;
			nutIsChild = false;

			alienExpectReceiving = false;

			anim.SetTrigger ("Player_Polite_Accept");
		}

		if (isPoliteGiving && alienExpectGiving) {
			isPoliteGiving = false;
			transponder.transform.parent = rightAlienHand.transform;
			transponder.transform.localPosition = new Vector3(0.00015f, 0.001585f, -0.000596f);
			transponder.transform.localEulerAngles = new Vector3 (184.97f, 0.0f, 0.0f);
			transponder.transform.localScale = new Vector3 (8.913981e-06f,8.913981e-06f,8.913981e-06f);
			anim.SetTrigger ("Player_Polite_Give");
		}

		if (droppedFruit) {
			droppedFruit = false;
			anim.SetTrigger ("Player_Drop_Fruit");
		}

		if (isEating && alienExpectEat) {
			isEating = false; 
			anim.SetTrigger ("Player_Eat_Fruit");
			grabFruit.SetActive (false);
			grabNut.SetActive (false);
		}

		if (fruitIsChild) {
			grabFruit.transform.parent = leftAlienHand.transform;



			grabFruit.transform.localPosition = new Vector3(-0.00025f, 0.0017f, -0.00039f);

		}

		if (nutIsChild) {
			grabNut.transform.parent = rightAlienHand.transform;



			grabNut.transform.localPosition = new Vector3(-0.0005f, 0.002f, -0.0005f);

		}

		if (alienExpectGrabBadge){
			beam.SetActive (true);
			Vector3 pos = beam.gameObject.transform.position;

			float newY = pos.y - beamVelocity * Time.deltaTime <= 5.665979f ? 5.665979f : pos.y - beamVelocity * Time.deltaTime;

			beam.gameObject.transform.position = new Vector3 (pos.x, newY, pos.z);

			if (!warpSoundPlayed){
				warpSoundPlayed = true;
				beamSource.Play ();
			}




		}

		if (alienExpectGrabBadge && badgeCollider.leftHandGrabbed()){
			alienExpectGrabBadge = false;
			transponder.transform.parent = leftHumanHand.transform;
			transponder.transform.localPosition = new Vector3 (-0.01f, -0.05f, 0.02f);
			transponder.transform.eulerAngles = new Vector3 (0f, 90f, 90f);
			anim.SetTrigger ("Player_Grab_Badge");
			beam.gameObject.SetActive (false);

			beamSource.Stop ();



		}

		if (alienExpectGrabBadge && badgeCollider.rightHandGrabbed()){
			alienExpectGrabBadge = false;

			transponder.transform.parent = rightHumanHand.transform;
			transponder.transform.localPosition = new Vector3 (-0.01f, -0.05f, 0.02f);
			transponder.transform.eulerAngles = new Vector3 (0f, 90f, 90f);
			anim.SetTrigger ("Player_Grab_Badge");
			beam.gameObject.SetActive (false);


			beamSource.Stop ();
		}
			


		// Once animations are done, set "new expecting" flags appropriately.

		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo (0);

		if (info.IsName("Waving")) {
			alienExpectWave = true;
		}

		if (info.IsName ("Bow_Middle")) {
			alienExpectBow = true; 
		}

		if (info.IsName("Show_Fruit") || info.IsName("Show_Nut")){
			alienExpectPoint = true;
		}

		if (info.IsName ("Giving_Gift")) {
			alienExpectReceiving = true; 

			if (pickedNut) {
				nutIsChild = true;
			}
			if (pickedFruit) {
				fruitIsChild = true;
			}
		}

		if (info.IsName ("Eat_Fruit")) {
			alienExpectEat = true;



		}


		if (info.IsName ("Idle(1)")) {
			badgeCollider.gameObject.SetActive (true);
			alienExpectGrabBadge = true;
			transponder.SetActive (true);
			//renderTransponder ();
		}


		if (info.IsName ("Expect_Gift")) {
			alienExpectGiving = true; 
	
		}

		if (info.IsName ("Happy_End")) {
			particle.gameObject.SetActive (true);
			cylinder.SetActive (true);
			Material mat = cylinder.GetComponent<Renderer>().material;
			cylinderReappearTime += Time.deltaTime;	
			mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, cylinderReappearTime / cylinderAppearTime);

			//change scene
			if (cylinderReappearTime >= cylinderAppearTime && !loadScene) {
				loadScene = true;
				StartCoroutine (LoadYourAsyncScene());
				print ("HERE");
			}

		}

		if (info.IsName ("Show_Fruit") || info.IsName ("Show_Nut")) {
			if (gaze2.gazed == fruit) {
				fruitSparkle.gameObject.SetActive (true);
				nutSparkle.gameObject.SetActive (false);
			}

			if (gaze2.gazed == nut) {
				fruitSparkle.gameObject.SetActive (false);
				nutSparkle.gameObject.SetActive (true);
			}

			if (gaze2.gazed == null) {
				fruitSparkle.gameObject.SetActive (false);
				nutSparkle.gameObject.SetActive (false);
			}


		} else {
			fruitSparkle.gameObject.SetActive (false);
			nutSparkle.gameObject.SetActive (false);
		}
			



	}

	void resetState(){

		isWaving = false;

		isPointingFruit = false;
		isPointingNut = false;

		isBowing = false;

		isPoliteGiving = false;
		isPoliteReceiving = false;

		isRudeGiving = false;
		isRudeReceiving = false;

		droppedFruit = false; 
		isEating = false;

	}

	void renderTransponder(){

		transponder.SetActive (true);
		if (transponderLoadTime >= transponderFadeInTime) {
			innerTransponder.GetComponent<Renderer> ().material = innerMaterialOpaque;
			outerTransponder.GetComponent<Renderer> ().material = outerMaterialOpaque;
			return;
		}

		if (transponderLoadTime < transponderFadeInTime) {
			outerMaterialFade.color = new Color (outerMaterialFade.color.r, outerMaterialFade.color.g, outerMaterialFade.color.b, transponderLoadTime / transponderFadeInTime);
			innerMaterialFade.color = new Color (innerMaterialFade.color.r, innerMaterialFade.color.g, innerMaterialFade.color.b, transponderLoadTime / transponderFadeInTime);
		}

		transponderLoadTime += Time.deltaTime;


	}

	IEnumerator LoadYourAsyncScene(){
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Ship_End");

		while (!asyncLoad.isDone) {
			yield return null;
		}

	}
}
