using UnityEngine;
using System.Collections;

public class Game_Controler : MonoBehaviour {

	public string Stance = "Standard"; //others are, "stealth" and "Running"
	
	public GameObject Player;
	public GameObject Floor;

	public int AP = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MovePlayer(){
		AP -= 1;
	}
}
