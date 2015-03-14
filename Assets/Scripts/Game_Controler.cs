using UnityEngine;
using System.Collections;

public class Game_Controler : MonoBehaviour {

	public GameObject Player;
	public GameObject Floor;
	public GameObject Hero;
	public Texture APTexture;

	public int AP = 4;

	//Used when the random chance is happening
	public GameObject DiceIconPre;
	public bool DiceIcon = false;

	//Used to control Player Stances
	private PlayerStances currentStance;
	private PossibleTurns currentTurn;

	public bool playerWalking = false;
	public bool playerRunning = false;
	public bool playerSneaking = false;
    public bool paused;
	public bool isPlayersTurn = false;
	public bool isAiTurn = false;

	//Interactable objects

	public bool P_InteractLight = false;
	public bool P_InteractCamera = false;
	public bool P_InteractLaser = false;
	public bool P_InteractPickup = false;

	public string infotext = "";


	public int SneakMovementCost = 3;
	public int WalkMovementCost = 2;
	public int RunMovementCost = 1;
	public int CurrentMovementCost;

	public GameObject walkDetection;
	public GameObject runDetection;

    public float buttonWidth;
    public float buttonHeight;

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
		DiceRolling();
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
			walkDetection.SetActive(true);
			runDetection.SetActive(false);
			
			break;
			
		case (PlayerStances.Run):
			playerWalking = false;
			playerRunning = true;
			playerSneaking = false;
			CurrentMovementCost = RunMovementCost;
			walkDetection.SetActive(false);
			runDetection.SetActive(true);
			
			break;
			
		case (PlayerStances.Sneak):
			playerWalking = false;
			playerRunning = false;
			playerSneaking = true;
			CurrentMovementCost = SneakMovementCost;
			walkDetection.SetActive(false);
			runDetection.SetActive(false);
			
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

	//attempt at tooltips
	void OnMouseEnter(){

		infotext = "tooltop Button";
	}

	//This sections creates/controls the GUI Display for Stances
	void OnGUI()
	{
		//tooltips
//		GUI.Label (new Rect (Input.mousePosition.x,Screen.height-Input.mousePosition.y, infotext.Length * 100, 200),infotext);
//		GUI.Label (Rect (Input.mousePosition.x,Screen.height-Input.mousePosition.y, infotext.Length * 10, 20), infotext);
//		GUI.Label (Rect (Input.mousePosition.x,Screen.height-Input.mousePosition.y, infotext.Length * 10, 20), infotext);
//		GUI.Label (Rect (Input.mousePosition.x,Screen.height-Input.mousePosition.y, infotext.Length * 10, 20), infotext);


		//WALK STANCE BUTTON
		if (GUI.Button(new Rect(160,Screen.height - 35, buttonWidth, buttonHeight), "Walk Stance") && isPlayersTurn == true) 
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
        if (GUI.Button(new Rect(270, Screen.height - 35, buttonWidth, buttonHeight), "Run Stance") && AP >= 1 && isPlayersTurn == true) 
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
        if (GUI.Button(new Rect(50, Screen.height - 35, buttonWidth, buttonHeight), "Sneak Stance") && AP >= 1 && isPlayersTurn == true) 
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
		GUI.DrawTexture(new Rect(50, Screen.height - 60, 30, AP * -63), APTexture);
        GUI.Label(new Rect(55, Screen.height - 60, 30, 30), "AP");

		//player Interactive placeholders
		if (P_InteractPickup == true && isPlayersTurn == true){
			if (GUILayout.Button ("Interact: Pickup")){
				Debug.Log("Interacting With Item");
			}
		}
		if (P_InteractCamera == true && isPlayersTurn == true){
			if (GUILayout.Button ("Interact: Camera")){
				Debug.Log("Interacting With Camera");
			}
		}
		if (P_InteractLaser == true && isPlayersTurn == true){
			if (GUILayout.Button ("Interact: Laser")){
				Debug.Log("Interacting With Lasrer");
			}
		}
		if (P_InteractLight == true && isPlayersTurn == true){
			if (GUILayout.Button ("Interact: Light")){
				Debug.Log("Interacting With Light");
			}
		}
	
		//END TURN BUTTON
        if (GUI.Button(new Rect(400, Screen.height - 35, buttonWidth, buttonHeight), "End Turn") && isPlayersTurn == true && isAiTurn == false) 
		{
			Hero.GetComponent<HeroController>().herosMoves = 2;
			currentTurn = PossibleTurns.AiTurn;
		}

		//Player stance button feedback placeholder, for testing purposes
		if (currentStance == PlayerStances.Walk && currentTurn != PossibleTurns.AiTurn)
			GUI.Box(new Rect(5,50,80,13), "");//Darken Walk button
		if (currentStance == PlayerStances.Run && currentTurn != PossibleTurns.AiTurn)
            GUI.Box(new Rect(5,30, 80, 13), "");//Darken Run button
		if (currentStance == PlayerStances.Sneak && currentTurn != PossibleTurns.AiTurn)
            GUI.Box(new Rect(5,80, 83, 13), "");//Darken Sneak button
		//if (currentTurn == PossibleTurns.AiTurn)
		//	GUI.Box(new Rect(5,80,80,13), "");//Darken End Turn button
	



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
		currentTurn = PossibleTurns.PlayerTurn;
	}

	void HeroTurn(){
		if(isAiTurn == true){
			AP = 4;
		}
	}

	void DiceRolling(){
		if (DiceIcon == true ){
			DiceIconPre.SetActive(true);
		}
		else{
			DiceIconPre.SetActive(false);
		}
	}
}
