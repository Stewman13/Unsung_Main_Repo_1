using UnityEngine;
using System.Collections;

public class SpotLightDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SuccessfulDiceRoll(){
		StartCoroutine (DestroyObject());
	}

	public IEnumerator DestroyObject(){
		float delayTime = 2.0f;
		//add audio
		//add particle effect
		yield return new WaitForSeconds (delayTime);
		Destroy (gameObject);

	}

}
