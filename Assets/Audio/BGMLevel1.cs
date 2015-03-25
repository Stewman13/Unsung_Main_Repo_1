using UnityEngine;
using System.Collections;

public class BGMLevel1 : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		audio.Play();
		//print ("BGM Play");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDie()
	{
		audio.Stop();
		print ("BGM Stop");
	}
}
