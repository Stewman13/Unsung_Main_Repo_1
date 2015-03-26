using UnityEngine;
using System.Collections;

public class Smoke_Destroy : MonoBehaviour {

	public int TurnsSmokeActive;
	private int turnCounter = 1;
	public bool Activated = false;
	private bool DiceRolled = false;
	
	public int DiceOne = 0;
	public int DiceTwo = 0;
	public int DiceTotal = 0;
	
	private int _buttonWidth = 200;
	private int _buttonHeight = 50;
	
	private Game_Controler _gameCon;
	
	// Use this for initialization
	void Start () {
		_gameCon = GameObject.Find("Main Camera").GetComponent<Game_Controler>();
	}
	
	// Update is called once per frame
	void Update () {
		SmokeDestroyer ();
		SmokeDuration ();
	}
	
	void OnGUI()
	{
		if (DiceRolled == false) {
			if (GUI.Button (new Rect (1000, Screen.height - 50, _buttonWidth, _buttonHeight), "Roll for Smoke Duration") && Activated == false && _gameCon.isPlayersTurn == true) {
				DiceOne = Random.Range (1, 7);
				DiceTwo = Random.Range (1, 7);
				DiceTotal = 0;
				DiceTotal = (DiceOne + DiceTwo);
				print ("TotalDice " + DiceTotal);
				if(DiceTotal >= 2 && DiceTotal < 6){
					TurnsSmokeActive = 1;
				}
				if(DiceTotal >= 6 && DiceTotal < 9){
					TurnsSmokeActive = 2;
				}
				if(DiceTotal >= 9 && DiceTotal <= 12){
					TurnsSmokeActive = 3;
				}
				turnCounter = 1;
				DiceRolled = true;
				Activated = true;
			}
		}
	}
	
	void SmokeDuration (){
		if (_gameCon.isAiTurn == true && turnCounter == 1 && Activated == true) {
			turnCounter = 0;
		}
		if (_gameCon.isPlayersTurn == true && turnCounter == 0 && Activated == true) {
			TurnsSmokeActive -= 1;
			turnCounter = 1;
		}
	}
	
	void SmokeDestroyer(){
		if (TurnsSmokeActive <= 0 && Activated == true) {
			Destroy(this.gameObject);
		}
	}
}