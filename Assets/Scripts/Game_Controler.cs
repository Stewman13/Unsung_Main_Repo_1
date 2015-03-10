using UnityEngine;
using System.Collections;

public class Game_Controler : MonoBehaviour {

	public GameObject Player;
	public GameObject Floor;
	public Texture APTexture;

	public int AP = 4;

	//Used to control Player Stances
	private PlayerStances currentStance;
	private PossibleTurns currentTurn;

	public bool playerWalking = false;
	public bool playerRunning = false;
	public bool playerSneaking = false;
    public bool paused;
	public bool isPlayersTurn = false;
	public bool isAiTurn = false;

	public int SneakMovementCost = 3;
	public int WalkMovementCost = 2;
	public int RunMovementCost = 1;
	public int CurrentMovementCost;

	public GameObject walkDetection;
	public GameObject runDetection;


	public string Stance = "Standard"; //others are, "stealth" and "Running"

	public enum PlayerStances 
	{
		Walk,
		Run,
		Sneak
		
	}

	public enum PossibleTurns
	{
		PlayerTurn,
		AiTurn
	}


	// Use this for initialization
	void Start () {
		currentStance = PlayerStances.Walk;
		currentTurn = PossibleTurns.AiTurn;
	}
	
	// Update is called once per frame
	void Update () {
		Stances();
		Turns ();
		HeroTurn();
		ActivatePlayerTurn();

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P))
        {
            paused = PauseToggle();
        }   
	}

	//Tells the other stances to deactivate when one is activated
	void Stances(){
		//Debug.Log (currentStance);
		
		switch (currentStance) 
		{
		case (PlayerStances.Walk):
			playerWalking = true;
			playerRunning = false;
			playerSneaking = false;
			CurrentMovementCost = WalkMovementCost;
		//	walkDetection.SetActive(true);
		//	runDetection.SetActive(false);
			
			break;
			
		case (PlayerStances.Run):
			playerWalking = false;
			playerRunning = true;
			playerSneaking = false;
			CurrentMovementCost = RunMovementCost;
		//	walkDetection.SetActive(false);
		//	runDetection.SetActive(true);
			
			break;
			
		case (PlayerStances.Sneak):
			playerWalking = false;
			playerRunning = false;
			playerSneaking = true;
			CurrentMovementCost = SneakMovementCost;
		//	walkDetection.SetActive(false);
		//	runDetection.SetActive(false);
			
			break;
			
		}
	}

	//Tells us that when ones turn is running, another is not.
	void Turns(){

		switch (currentTurn)
		{
		case (PossibleTurns.AiTurn):
			isAiTurn = true;
			isPlayersTurn = false;

			break;

		case (PossibleTurns.PlayerTurn):
			isAiTurn = false;
			isPlayersTurn = true;
	
			break;
		}
	}

	// Recives message and takes AP when player moves
	void MovePlayer(){
		AP -= CurrentMovementCost;
	}

	//This sections creates/controls the GUI Display for Stances
	void OnGUI()
	{
		//WALK STANCE BUTTON
		if (GUILayout.Button ("Walk Stance")) 
		{
			if(currentStance == PlayerStances.Sneak && AP >= 1)
			{
				currentStance = PlayerStances.Walk;
			}
			else if(currentStance == PlayerStances.Run && AP >= 1)
			{
				currentStance = PlayerStances.Walk;
			}
		}
		//RUN STANCE BUTTON
		if (GUILayout.Button ("Run Stance") && AP >= 1) 
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
		//SNEAK STANCE BUTTON
		if (GUILayout.Button ("Sneak Stance") && AP >= 1) 
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
		//BLUE AP BAR
		if (!APTexture) 
		{
			Debug.LogError ("Assign a texture");
			return;
		}
		GUI.DrawTexture(new Rect(100, 80, 30, AP * -20), APTexture);

		//END TURN BUTTON
		if (GUILayout.Button ("End Turn") && isPlayersTurn == true) 
		{
			currentTurn = PossibleTurns.AiTurn;
		}
	}

	//Pasue on 'esc' menu
    bool PauseToggle()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            return (false);
        }
        else
        {
            Time.timeScale = 0;
            return (true);
        }
    }
	// tells the player to start their turn.
	void ActivatePlayerTurn(){
		if(isPlayersTurn == true){

		}
	}

	void HeroTurn(){
		if(isAiTurn == true){
			print ("AI has had their turn");
			currentTurn = PossibleTurns.PlayerTurn;
			AP = 4;
		}
	}
}
