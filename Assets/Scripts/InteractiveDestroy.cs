using UnityEngine;
using System.Collections;

public class InteractiveDestroy : MonoBehaviour {

	//change depending on what object this script is attached to
	public bool IsLightSource;
	public bool IsCameraSource;
	public bool IsLaserSource;

	public bool IsSmokeGrenade;
	public bool IsWireCutters;

	public AudioClip SuccessLight;
	public AudioClip SuccessSmoke;
	public AudioClip FailInteractive;
	public bool OnePlayAudio;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SuccessfulDiceRoll(){
		if (IsLightSource == true){
			StartCoroutine (DestroyObject());
		}



		if (IsSmokeGrenade == true){
			StartCoroutine (DestroyObject());
			print ("removed pickup from scene");
		}

		if (IsLaserSource == true){
			StartCoroutine (DestroyObject());
			print ("removed laser");
		}
	}

	void SetOffAlarm(){
		if (IsLaserSource == true){
			StartCoroutine (SetAlarm());
			print ("Laser Alarm Running");
		}
	}

	public IEnumerator DestroyObject(){
		if (IsLightSource == true && OnePlayAudio == false){
			AudioSource.PlayClipAtPoint(SuccessLight, gameObject.transform.position,0.5f);
		}
		OnePlayAudio =true;
		float delayTime = 2.0f;
		//add audio
		//add particle effect
		yield return new WaitForSeconds (delayTime);
		Destroy (gameObject);

	}

	public IEnumerator SetAlarm(){
		float delayTime = 1.0f;
		//add audio
		//add particle
		//send enemy 'high alert mode'
		yield return new WaitForSeconds (delayTime);

	}



}
