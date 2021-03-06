﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game_Controler : MonoBehaviour {

	public int Score;
	public string Grade;
	public bool HeroAction = false;
	public int PlayersCurrentTilePoints = 0;
	public bool AddScore = false;

	public GameObject Player;
	public GameObject Floor;
	public GameObject Hero;
	public GameObject Grenade;
	public Texture APTexture;

	public int AP = 4;
    public int wireCutterCount = 0;

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
	public bool isPlayerOutOfCamera= false;

	//Interactable objects

	public bool P_InteractLight = false;
	public bool P_InteractCamera = false;
	public bool P_InteractLaser = false;
	public bool P_InteractPickup = false;

	//Interactive constraints
	public int DiceOne;
	public int DiceTwo;
	public int DiceTotal = 0;
	public GameObject TileUnderPlayer;
	public int PlayerInteractive;
	public int PickupTile;
	public int CameraTile;
	public int LaserTile;
	public int LightTile;
	public bool PickupGrenadeStart = false;
	public bool InteractLightStart = false;
	public bool InteractLaserStart = false;
	public bool PickedUpThisTile = false;
	public bool HighAlert = false;
	//Interactive Audio
	public AudioClip FailedLight;


	public int SneakMovementCost = 3;
	public int WalkMovementCost = 2;
	public int RunMovementCost = 1;
	public int CurrentMovementCost;
	public int GrenadeCount = 0;

	public GameObject walkDetection;
	public GameObject runDetection;

    public float buttonWidth;
    public float buttonHeight;

	public bool isLerping = false;

	public string Stance = "Standard"; //others are, "stealth" and "Running"

    public Text grenadeFeedback;
    public Text apFeedback;
    public Text wireCutterFeedback;

	//Just all the damn audio from Sam that I can implement at 6.20am 
	public AudioClip WalkButtonAud;
	public AudioClip RunButtonAud;
	public AudioClip SneakButtonAud;
	public AudioClip DiceRollAud001, DiceRollAud002, DiceRollAud003 ;
	public AudioClip LaserAlarm;
	public AudioClip EndTurnAud;


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

		//sanity check
		isPlayerOutOfCamera = false;

	}
	
	// Update is called once per frame
	void Update () {
        grenadeFeedback.text = " x " + GrenadeCount;
        apFeedback.text = "AP x " + AP;
        wireCutterFeedback.text = " x " + wireCutterCount;

		Stances();
		Turns ();
		HeroTurn();
		DiceRolling();
		scoring();
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P))
        {
            paused = PauseToggle();
        }   

		//runs when player is dead, out of camera view
		if (isPlayerOutOfCamera){
			PickupGrenadeStart = false;
			InteractLightStart = false;
			InteractLaserStart = false;
			PickedUpThisTile = false;
			GUIManager.Instance.PlayerOutOfCamera = true;
			print("Game_Controler: Player is dead");

		}

		//collecting the variables for interactive constraints
		TileUnderPlayer = Player.GetComponent<Player_Controller>().TileUnderPlayer;
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
	//void MovePlayer(){
	//	AP -= CurrentMovementCost;
	//}

	//This sections creates/controls the GUI Display for Stances
	void OnGUI()
	{
		/*if (isPlayerOutOfCamera == false){
			//WALK STANCE BUTTON
			if (GUI.Button(new Rect(160,Screen.height - 35, buttonWidth, buttonHeight), "Walk Stance") && isPlayersTurn == true && isPlayerOutOfCamera == false && isLerping == false) 
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
			if (GUI.Button(new Rect(270, Screen.height - 35, buttonWidth, buttonHeight), "Run Stance") && AP >= 1 && isPlayersTurn == true && isPlayerOutOfCamera == false && isLerping == false) 
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
			if (GUI.Button(new Rect(50, Screen.height - 35, buttonWidth, buttonHeight), "Sneak Stance") && AP >= 1 && isPlayersTurn == true && isPlayerOutOfCamera == false && isLerping == false) 
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
			}*/

		//player Interactive placeholders
		if (P_InteractPickup == true && isPlayersTurn == true){
			if (GUI.Button(new Rect (390, Screen.height - 35, buttonWidth+10, buttonHeight), "Interact: Pickup")&& AP >= 1 
			    && isPlayersTurn == true && PickupGrenadeStart == false && PickedUpThisTile == false){
					Debug.Log("Interacting With Item");
					PickupGrenadeStart = true;
				}
			}
		if (P_InteractCamera == true && isPlayersTurn == true){
			if (GUI.Button(new Rect (390, Screen.height - 35, buttonWidth+10, buttonHeight),"Interact: Camera")){
					Debug.Log("Interacting With Camera");
					//camera choices screen goes here
				}
			}
		if (P_InteractLaser == true && isPlayersTurn == true){
			if (GUI.Button(new Rect (390, Screen.height - 35, buttonWidth+10, buttonHeight),"Interact: Laser")){
					Debug.Log("Interacting With Lasrer");
					InteractLaserStart = true;
					//laser choices screen goes here
				}
			}
		if (P_InteractLight == true && isPlayersTurn == true){
				if (GUI.Button(new Rect (390, Screen.height - 35, buttonWidth+10, buttonHeight),"Interact: Light")&& AP >= 1 
				    && isPlayersTurn == true && PickupGrenadeStart == false && InteractLightStart == false){
					Debug.Log("Interacting With Light");
					InteractLightStart = true;
				}
			}

		//Interactive screens goes here
		//Grenade
		if (P_InteractPickup == true && isPlayersTurn == true && PickupGrenadeStart == true && AP >= 1){
			GUI.Box(new Rect(Screen.width/50f, Screen.height/100, 500, 400),"Interactive: Pickup Smoke Grenade");
			AudioSource.PlayClipAtPoint(DiceRollAud002, gameObject.transform.position);

			if (GUI.Button(new Rect (Screen.width/50 + 150, Screen.height/100 + 70, buttonWidth + 200, buttonHeight + 10),"90% Chance To Pickup, [-1 Action Points]")){
				AP --;
				GrenadeCount += 2;
				TileUnderPlayer.GetComponent<FloorTile_Controler>().GrenadesPickedUp = true;
				TileUnderPlayer.GetComponent<FloorTile_Controler>().PlayerInteractive = 0;
				TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractPickup = 0;
				PickupGrenadeStart = false;
				print("Player Recieved 2 Smoke Grenades");
			}
			if (GUI.Button(new Rect (Screen.width/40 + 150, Screen.height/100 + 100, buttonWidth + 200, buttonHeight + 10),"Exit Menu")){
				PickupGrenadeStart = false;
			}
		}
		else if (P_InteractPickup == true && isPlayersTurn == true && PickupGrenadeStart == true && AP == 0){
			PickupGrenadeStart = false;
		}

		//Light
		if (P_InteractLight == true && isPlayersTurn == true && InteractLightStart == true && AP >= 1){
				GUI.Box(new Rect(Screen.width/50f, Screen.height/100, 500, 400),"Interactive: Light Switch");
				if (GUI.Button(new Rect (Screen.width/40 + 150, Screen.height/100 + 70, buttonWidth + 200, buttonHeight + 10), "60% Chance To Turn Off Light, [-1 AP]")){
				AudioSource.PlayClipAtPoint(DiceRollAud003, gameObject.transform.position);
				DiceRoll();
					if (P_InteractLight == true && isPlayersTurn == true && InteractLightStart == true){
							if (DiceTotal >= 5){
								AP --;
								//send message to light to disable lightsource
								TileUnderPlayer.GetComponent<FloorTile_Controler>().PlayerInteractive = 0;
								TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractLight = 0;
								TileUnderPlayer.GetComponent<FloorTile_Controler>().SuccessfulLightDestroy = true;
								InteractLightStart = false;
								print("Player Deactivated Light");
							}
								if (DiceTotal <= 4 ){
								AP --;
								TileUnderPlayer.GetComponent<FloorTile_Controler>().PlayerInteractive = 0;
								TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractLight = 0;
								InteractLightStart = false;
								AudioSource.PlayClipAtPoint (FailedLight, Camera.main.camera.transform.position, 0.5f);
								print("Player Failed To Deactivated Light");
							}

					
					}
			}
				if (GUI.Button(new Rect (Screen.width/50 + 150, Screen.height/100 + 100, buttonWidth + 200, buttonHeight + 10),"Exit Menu")){
					InteractLightStart = false;
				}
			}
		else if (P_InteractLight == true && isPlayersTurn == true && InteractLightStart == true && AP == 0){
			InteractLightStart = false;
		}

		//Laser
		if (P_InteractLaser == true && isPlayersTurn == true && InteractLaserStart == true && AP >= 1){
			GUI.Box(new Rect(Screen.width/50f, Screen.height/100, 500, 400),"Interactive: Laser Switch");
			if (GUI.Button(new Rect (Screen.width/40 + 150, Screen.height/100 + 70, buttonWidth + 200, buttonHeight + 10), "70% Chance To Turn Off Laser, [-1 AP]")){
				DiceRoll();
				AudioSource.PlayClipAtPoint(DiceRollAud001, gameObject.transform.position);
				if (P_InteractLaser == true && isPlayersTurn == true && InteractLaserStart == true){
					if (DiceTotal >= 4){
						AP --;
						TileUnderPlayer.GetComponent<FloorTile_Controler>().PlayerInteractive = 0;
						TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractLaser = 0;
						TileUnderPlayer.GetComponent<FloorTile_Controler>().LaserDestroyed = true;
						InteractLaserStart = false;
						print("Player Deactivated Laser");
					}
					if (DiceTotal <= 3 ){
						AP --;
						TileUnderPlayer.GetComponent<FloorTile_Controler>().PlayerInteractive = 0;
						TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractLaser = 0;
						InteractLaserStart = false;
						//negative feedback here
						//negative result here
						print("Player Failed To Deactivated Laser");
					}
				}
			}
			if (GUI.Button(new Rect (Screen.width/40 + 150, Screen.height/100 + 100, buttonWidth + 200, buttonHeight + 10), "50% Chance To Set Off Alarm, [-1 AP]")){
			DiceRoll();
				AudioSource.PlayClipAtPoint(DiceRollAud001, gameObject.transform.position);
			if (P_InteractLaser == true && isPlayersTurn == true && InteractLaserStart == true){
				if (DiceTotal >= 4){
					AP --;
					TileUnderPlayer.GetComponent<FloorTile_Controler>().PlayerInteractive = 0;
					TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractLaser = 0;
					TileUnderPlayer.GetComponent<FloorTile_Controler>().LaserPlayAlarm = true;
					InteractLaserStart = false;
						AudioSource.PlayClipAtPoint(LaserAlarm, gameObject.transform.position);
					print("Player Set Off Alarm");
				}
				if (DiceTotal <= 3 ){
					AP --;
					TileUnderPlayer.GetComponent<FloorTile_Controler>().PlayerInteractive = 0;
					TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractLaser = 0;
					InteractLaserStart = false;
					//negative feedback here
					//negative result here
					print("Player Failed To Set Off Alarm");
				}
			}
		}
			if (GUI.Button(new Rect (Screen.width/50 + 150, Screen.height/100 + 130, buttonWidth + 200, buttonHeight + 10),"Exit Menu")){
				InteractLaserStart = false;
			}
		}
		else if (P_InteractLaser == true && isPlayersTurn == true && InteractLightStart == true && AP == 0){
			InteractLaserStart = false;
		}

		//is player dead?
		if (isPlayerOutOfCamera){
			if (GUI.Button(new Rect(Screen.width/2, Screen.height/2, buttonWidth +200, buttonHeight + 10), "The hero got too far away. Restart") && isPlayersTurn == true && isAiTurn == false)
			{
				print("__Game over: Restarted Scene");
				Application.LoadLevel(Application.loadedLevel);
			}
						
		}

		//END TURN BUTTON **MOVED TO FUNCTION BELOW**
		/*if (isPlayerOutOfCamera == false){

			if ( P_InteractCamera == false && P_InteractLaser == false && P_InteractLight == false && P_InteractPickup == false )
			{
				if (GUI.Button(new Rect(400, Screen.height - 35, buttonWidth, buttonHeight), "End Turn") && isPlayersTurn == true && isAiTurn == false  && isLerping == false)
				{
					Hero.GetComponent<HeroController>().herosMoves = Random.Range(1,3);
					currentTurn = PossibleTurns.AiTurn;
					Floor.BroadcastMessage("unselectTile");
					print ("Player: Turn ended successfully");
				}
			}
			else if ( P_InteractCamera == true && P_InteractLaser == false && P_InteractLight == false && P_InteractPickup == false||
			         P_InteractCamera == false && P_InteractLaser == true && P_InteractLight == false && P_InteractPickup == false ||
			         P_InteractCamera == false && P_InteractLaser == false && P_InteractLight == true && P_InteractPickup == false||
			         P_InteractCamera == false && P_InteractLaser == false && P_InteractLight == false && P_InteractPickup == true) 
			{
				if (GUI.Button(new Rect(510, Screen.height - 35, buttonWidth, buttonHeight), "End Turn") && isPlayersTurn == true && isAiTurn == false && isLerping == false) 
				{
					Hero.GetComponent<HeroController>().herosMoves = Random.Range(1,3);
					currentTurn = PossibleTurns.AiTurn;
					Floor.BroadcastMessage("unselectTile");
					print ("Ended turn on an interactive tile");
				}
			}
		}*/

		/*if(isPlayerOutOfCamera == false){
			//Player stance button feedback placeholder, for testing purposes
			if (currentStance == PlayerStances.Walk && currentTurn != PossibleTurns.AiTurn)		
				GUI.Box(new Rect(160, Screen.height- 30, buttonWidth, buttonHeight/1.3f), "");//Darken Walk button
			if (currentStance == PlayerStances.Run && currentTurn != PossibleTurns.AiTurn)
				GUI.Box(new Rect(270, Screen.height- 30, buttonWidth, buttonHeight/1.3f), "");//Darken Run button
			if (currentStance == PlayerStances.Sneak && currentTurn != PossibleTurns.AiTurn)
				GUI.Box(new Rect(50, Screen.height- 30, buttonWidth, buttonHeight/1.3f), "");//Darken Sneak button
		}*/
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

	void DiceRoll(){
		DiceTotal = 0;
		DiceOne = Random.Range(1,7);
		DiceTwo = Random.Range(1,7);
		DiceTotal = (DiceOne + DiceTwo);
		print ("TotalDice " + DiceTotal);
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

	void scoring(){

		if(HeroAction == true && AddScore == false){
			AddScore = true;
			Score += PlayersCurrentTilePoints;
			//Send this score to somewhere it can be retaind throughout the game.
		}

		if(Score == 0){
			Grade = "E";
		}
		if(Score >= 2 && Score < 5){
			Grade = "D";
		}
		if(Score >= 5 && Score < 9){
			Grade = "C";
		}
		if(Score >= 9 && Score < 12){
			Grade = "B";
		}
		if(Score == 12){
			Grade = "A";
		}
	}

    //Sneak Stance Button
    public void SneakButton()
    {
        if (isPlayerOutOfCamera == false)
        {
            if (AP >= 1 && isPlayersTurn == true && isPlayerOutOfCamera == false && isLerping == false)
            {
                if (currentStance == PlayerStances.Walk)
                {
					AudioSource.PlayClipAtPoint(SneakButtonAud, gameObject.transform.position,0.5f);
                    currentStance = PlayerStances.Sneak;
                    AP -= 1;
                }
                else if (currentStance == PlayerStances.Run)
                {
                    currentStance = PlayerStances.Sneak;
					AudioSource.PlayClipAtPoint(SneakButtonAud, gameObject.transform.position,0.5f);
                    AP -= 1;
                }
            }
        }
    }

    //Walk Stance button 
    public void WalkButton()
    {
        if (isPlayerOutOfCamera == false)
        {
            if (isPlayersTurn == true && isPlayerOutOfCamera == false && isLerping == false)
            {
                if (currentStance == PlayerStances.Sneak && AP >= 1)
                {
                    currentStance = PlayerStances.Walk;
					AudioSource.PlayClipAtPoint(WalkButtonAud, gameObject.transform.position,0.5f);
                }
                else if (currentStance == PlayerStances.Run && AP >= 1)
                {
                    currentStance = PlayerStances.Walk;
					AudioSource.PlayClipAtPoint(WalkButtonAud, gameObject.transform.position,0.5f);
                }
            }
        }
    }

    //Run Stance button
    public void RunButton()
    {
        if (isPlayerOutOfCamera == false)
        {
            if (AP >= 1 && isPlayersTurn == true && isPlayerOutOfCamera == false && isLerping == false)
            {
                if (currentStance == PlayerStances.Sneak)
                {
                    currentStance = PlayerStances.Run;
					AudioSource.PlayClipAtPoint(RunButtonAud, gameObject.transform.position,0.5f);
                    AP -= 1;
                }
                else if (currentStance == PlayerStances.Walk)
                {
                    currentStance = PlayerStances.Run;
					AudioSource.PlayClipAtPoint(RunButtonAud, gameObject.transform.position,0.5f);
                    AP -= 1;
                }
            }
        }
    }

    //End Turn Button
    public void EndTurnButton()
    {
        if (isPlayerOutOfCamera == false)
        {

            if (P_InteractCamera == false && P_InteractLaser == false && P_InteractLight == false && P_InteractPickup == false)
            {
                if (isPlayersTurn == true && isAiTurn == false && isLerping == false)
                {
					AudioSource.PlayClipAtPoint(EndTurnAud, gameObject.transform.position,0.5f);
                    Hero.GetComponent<HeroController>().herosMoves = Random.Range(1, 3);
                    currentTurn = PossibleTurns.AiTurn;
                    Floor.BroadcastMessage("unselectTile");
                    print("Player: Turn ended successfully");
                }
            }
            else if (P_InteractCamera == true && P_InteractLaser == false && P_InteractLight == false && P_InteractPickup == false ||
                     P_InteractCamera == false && P_InteractLaser == true && P_InteractLight == false && P_InteractPickup == false ||
                     P_InteractCamera == false && P_InteractLaser == false && P_InteractLight == true && P_InteractPickup == false ||
                     P_InteractCamera == false && P_InteractLaser == false && P_InteractLight == false && P_InteractPickup == true)
            {
                if (isPlayersTurn == true && isAiTurn == false && isLerping == false)
                {
					AudioSource.PlayClipAtPoint(EndTurnAud, gameObject.transform.position);
                    Hero.GetComponent<HeroController>().herosMoves = Random.Range(1, 3);
                    currentTurn = PossibleTurns.AiTurn;
                    Floor.BroadcastMessage("unselectTile");
                    print("Ended turn on an interactive tile");
                }
            }
        }
    }
}
