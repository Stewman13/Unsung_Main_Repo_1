using UnityEngine;
using System.Collections;

public class Game_Controler : MonoBehaviour {

	public GameObject Player;
	public GameObject Floor;

	public int AP = 4;

	//Used to control Player Stances
	private PlayerStances currentStance;
	
	public bool playerWalking = false;
	public bool playerRunning = false;
	public bool playerSneaking = false;

	public string Stance = "Standard"; //others are, "stealth" and "Running"

	public enum PlayerStances 
	{
		Walk,
		Run,
		Sneak
		
	}

	// Use this for initialization
	void Start () {
		currentStance = PlayerStances.Walk;
	}
	
	// Update is called once per frame
	void Update () {
		Stances();
	}

	//Tells the other stances to deactivate when one is activated
	void Stances(){
		Debug.Log (currentStance);
		
		switch (currentStance) 
		{
		case (PlayerStances.Walk):
			playerWalking = true;
			playerRunning = false;
			playerSneaking = false;
			
			break;
			
		case (PlayerStances.Run):
			playerWalking = false;
			playerRunning = true;
			playerSneaking = false;
			
			break;
			
		case (PlayerStances.Sneak):
			playerWalking = false;
			playerRunning = false;
			playerSneaking = true;
			
			break;
			
		}
	}

	// Recives message and takes AP when player moves
	void MovePlayer(){
		if(currentStance == PlayerStances.Walk){
			AP -= 2;
		}
		if(currentStance == PlayerStances.Sneak){
			AP -= 3;
		}
		if(currentStance == PlayerStances.Run){
			AP -= 1;
		}
	}

	//This sections creates/controls the GUI Display for Stances
	void OnGUI()
	{
		if (GUILayout.Button ("Walk Stance")) 
		{
			if(currentStance == PlayerStances.Sneak)
			{
				currentStance = PlayerStances.Walk;
			}
			else if(currentStance == PlayerStances.Run)
			{
				currentStance = PlayerStances.Walk;
			}
		}
		
		if (GUILayout.Button ("Run Stance") && AP >= 5) 
		{
			if(currentStance == PlayerStances.Sneak)
			{
				currentStance = PlayerStances.Run;
				AP -= 1;
			}
			else if(currentStance == PlayerStances.Walk)
			{
				currentStance = PlayerStances.Run;
				AP -= 1;
			}
		}
		
		if (GUILayout.Button ("Sneak Stance") && AP >= 5) 
		{
			if(currentStance == PlayerStances.Walk)
			{
				currentStance = PlayerStances.Sneak;
				AP -= 1;
			}
			else if(currentStance == PlayerStances.Run)
			{
				currentStance = PlayerStances.Sneak;
				AP -= 1;
			}
		}
	}
}
