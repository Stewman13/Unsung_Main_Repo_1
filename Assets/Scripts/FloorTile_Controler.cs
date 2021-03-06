﻿using UnityEngine;
using System.Collections;


//I added a comment
public class FloorTile_Controler : MonoBehaviour {
	
	public float TileGapDistance = 1.0f;
	public GameObject Player;
	public GameObject Node;
	public GameObject Floor;
	public GameObject ThisTile;
	public GameObject Controller;

	public GameObject TileForward;
	public GameObject TileBack;
	public GameObject TileLeft;
	public GameObject TileRight;

	//Movement Avalability list
	public bool ForwardAvailable = false;
	public bool BackAvailable = false;
	public bool LeftAvailable = false;
	public bool RightAvailable = false;

	public bool PlayerIsOnThisBlock = false;
	public bool HeroIsOnThisBlock = false;
	public bool AiIsOnThisBlock = false;
	public bool BoxIsOnThisBlock = false;
	public bool MouseOverTile = false;

	public bool IsAThreat = false;
	public bool CanMoveHere = false;
	public bool isSmokeable = false;

	public bool NextToPlayersTile = false;
	private int ap;
	private int PlayerMoveNum;
    private Game_Controler _gameCon;

	public int AIPathChannel = 0;
	public int HerosPath = 0;
	public int PlayerInteractive = 0;
	public int LevelEnd = 0;

	public int ScoreWorth = 0;
	public bool WorthPointsIfOn = false;
	public bool HerosActionMomentHere = false;

	public RaycastHit HitBack;
	public RaycastHit HitForward;
	public RaycastHit HitLeft;
	public RaycastHit HitRight;

	private Color newColour;

	//Stuff For Lerping
	private float timeStartedLerping;
	public bool isLerping;
	public float journeyLength;
	private Vector3 startPosition;
	private Vector3 endPosition;
	public float timeTakenDuringLerp;

	public int DeductAP;

	//Tile can be interacted with player
	//private bool colourDelay = false;
	public int Tile_InteractLight = 0; 
	public int Tile_InteractCamera = 0;
	public int Tile_InteractLaser = 0;
	public int Tile_InteractPickup = 0;

	//Interactive Message constraints
	public bool LightDetectingThisTile = false;
	public bool CameraDetectingThisTile = false;
	public bool SuccessfulLightDestroy = false;
	public bool GrenadesPickedUp = false;
	public bool LaserDestroyed = false;
	public bool LaserPlayAlarm = false;

	public bool Set = false;

	void Start(){
		//set bools to false, so there's no bugs
		ForwardAvailable = false;
		BackAvailable = false;
		LeftAvailable = false;
		RightAvailable = false;
		PlayerIsOnThisBlock = false;
		HeroIsOnThisBlock = false;
		AiIsOnThisBlock = false;
		BoxIsOnThisBlock = false;
		MouseOverTile = false;
//		Tile_InteractLight = false; 
//		Tile_InteractCamera = false;
//		Tile_InteractLaser = false;
//		Tile_InteractPickup = false;
		NextToPlayersTile = false;
        _gameCon = GameObject.Find("Main Camera").GetComponent<Game_Controler>();
		Run();
	}

	// Run every update
	void Update () {
        if (_gameCon.paused != true)
        {
            TileDetector();
            AvailabilityChecker();
            MouseUp();
			StanceMoveSpeed();
			RayFromMouse();
			NextStage();
			HeroAction();
			//checks to send message, will destroy light source
			MessageCheck();
			DeMapped();
		}
	}

	void Run(){
		newColour = ThisTile.renderer.material.color;
		newColour.a = 0.0f;
		ThisTile.renderer.material.color = newColour;
		ThisTile.renderer.material.shader = Shader.Find("Transparent/Diffuse");
	}

	//sends message to destroy light source
	void MessageCheck(){
		if (SuccessfulLightDestroy == true || GrenadesPickedUp == true || LaserDestroyed == true){
			BroadcastMessage("SuccessfulDiceRoll",SendMessageOptions.DontRequireReceiver);
		}

		if (LaserPlayAlarm == true){
			BroadcastMessage("SetOffAlarm",SendMessageOptions.DontRequireReceiver);
			LaserPlayAlarm = false;
		}
		//Detects player if they are in a camera or light
		if (CameraDetectingThisTile == true && PlayerIsOnThisBlock) {
			Player.SendMessage ("Alert");
		}
		if (LightDetectingThisTile == true && PlayerIsOnThisBlock) {
			_gameCon.HighAlert = true;
		}
		if (LightDetectingThisTile == false && PlayerIsOnThisBlock) {
			_gameCon.HighAlert = false;
		}
	}

	void StanceMoveSpeed(){
		journeyLength = Vector3.Distance(Player.transform.position, Node.transform.position);

		if(_gameCon.playerWalking == true){
			if(journeyLength >= 0.9 && journeyLength < 1.9 && isLerping == true && Set == false){
				timeTakenDuringLerp = 1.75f;
				DeductAP = 2;
				if(Set == false){
					_gameCon.AP -= DeductAP;
				}
				Set = true;
			}
			if(journeyLength >= 1.9 && journeyLength < 2.9 && isLerping == true && Set == false){
				timeTakenDuringLerp = 2.25f;
				DeductAP = 4;
				if(Set == false){
					_gameCon.AP -= DeductAP;
				}
				Set = true;
			}
		}
		if(_gameCon.playerRunning == true){
			if(journeyLength >= 0.9 && journeyLength < 1.9 && isLerping == true && Set == false){
				timeTakenDuringLerp = 0.5f;
				print ("length 1-2");
				DeductAP = 1;
				if(Set == false){
					_gameCon.AP -= DeductAP;
				}
				Set = true;
			}
			if(journeyLength >= 1.9 && journeyLength < 2.9 && isLerping == true && Set == false){
				timeTakenDuringLerp = 1.0f;
				print ("length 2-3");
				DeductAP = 2;
				if(Set == false){
					_gameCon.AP -= DeductAP;
				}
				Set = true;
			}
			if(journeyLength >= 2.9 && journeyLength < 3.9 && isLerping == true && Set == false){
				timeTakenDuringLerp = 1.5f;
				print ("length 3-4");
				DeductAP = 3;
				if(Set == false){
					_gameCon.AP -= DeductAP;
				}
				Set = true;
			}
			if(journeyLength >= 3.9 && journeyLength < 5 && isLerping == true && Set == false){
				timeTakenDuringLerp = 2.0f;
				print ("length 4-5");
				DeductAP = 4;
				if(Set == false){
					_gameCon.AP -= DeductAP;
				}
				Set = true;
			}
		}
		if(_gameCon.playerSneaking == true && Set == false && isLerping == true){
			timeTakenDuringLerp = 1.5f;
			DeductAP = 3;
			if(Set == false){
				_gameCon.AP -= DeductAP;
			}
			Set = true;
		}
	}

	//this makes sure all tiles are detected at all times.
	//START. Raycasts, to detect tiles in all directions
	//This allows us to check all tile variables using tags! 
	void TileDetector(){
		RaycastHit HitUp;
			
		Ray TileCheckUp = new Ray(transform.position, Vector3.up);	
		Ray TileCheckForward = new Ray(transform.position, Vector3.forward);
		Ray TileCheckBack = new Ray(transform.position, Vector3.back);
		Ray TileCheckLeft = new Ray(transform.position, Vector3.left);
		Ray TileCheckRight = new Ray(transform.position, Vector3.right);
			
		Debug.DrawRay(transform.position, Vector3.up * TileGapDistance);
		Debug.DrawRay(transform.position, Vector3.left * TileGapDistance);
		Debug.DrawRay(transform.position, Vector3.right * TileGapDistance);
		Debug.DrawRay(transform.position, Vector3.forward * TileGapDistance);
		Debug.DrawRay(transform.position, Vector3.back * TileGapDistance);

		//Checks if Available
		if(Physics.Raycast(TileCheckBack, out HitBack, TileGapDistance)){ 
			if(HitBack.collider.tag == "Available"){
				BackAvailable = true;
				TileBack = HitBack.transform.gameObject;
				//this section will be repeated for all rays.
				//it tells the adjacent block the the player is next to it or not.
				if(PlayerIsOnThisBlock == true){
					HitBack.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		if(Physics.Raycast(TileCheckForward, out HitForward, TileGapDistance)){ 
			if(HitForward.collider.tag == "Available"){
				ForwardAvailable = true;
				TileForward = HitForward.transform.gameObject;
				if(PlayerIsOnThisBlock == true){
					HitForward.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		if(Physics.Raycast(TileCheckLeft, out HitLeft, TileGapDistance)){ 
			if(HitLeft.collider.tag == "Available"){
				LeftAvailable = true;
				TileLeft = HitLeft.transform.gameObject;
				if(PlayerIsOnThisBlock == true){
					HitLeft.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		if(Physics.Raycast(TileCheckRight, out HitRight, TileGapDistance)){ 
			if(HitRight.collider.tag == "Available"){
				RightAvailable = true;
				TileRight = HitRight.transform.gameObject;
				if(PlayerIsOnThisBlock == true){
					HitRight.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		//Checks if UnAvailable
		if(Physics.Raycast(TileCheckBack, out HitBack, TileGapDistance)){ 
			if(HitBack.collider.tag == "UnAvailable"){
				BackAvailable = false;
				TileBack = HitBack.transform.gameObject;
				if(PlayerIsOnThisBlock == true){
					HitBack.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		if(Physics.Raycast(TileCheckForward, out HitForward, TileGapDistance)){ 
			if(HitForward.collider.tag == "UnAvailable"){
				ForwardAvailable = false;
				TileForward = HitForward.transform.gameObject;
				if(PlayerIsOnThisBlock == true){
					HitForward.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		if(Physics.Raycast(TileCheckLeft, out HitLeft, TileGapDistance)){ 
			if(HitLeft.collider.tag == "UnAvailable"){
				LeftAvailable = false;
				TileLeft = HitLeft.transform.gameObject;
				if(PlayerIsOnThisBlock == true){
					HitLeft.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		if(Physics.Raycast(TileCheckRight, out HitRight, TileGapDistance)){ 
			if(HitRight.collider.tag == "UnAvailable"){
				RightAvailable = false;
				TileRight = HitRight.transform.gameObject;
				if(PlayerIsOnThisBlock == true){
					HitRight.collider.SendMessage ("NextToPlayer");
				}
			}
		}

		//A fix for the 'player on 2 tiles' bug. 
		//Works with other bug fixer further down.
		//Has to be here in the script! else it doesn't work.
		if(PlayerIsOnThisBlock == false && HeroIsOnThisBlock == false && 
		   AiIsOnThisBlock == false && BoxIsOnThisBlock == false){
			gameObject.tag = "Available";
		}

		//Checks for any form of player or Ai.  
		//*IMPORTANT* it is possible to have the player RayDown, then send Messagse.
		//Keep this in mind for future reference!! could be usefull.
		if(Physics.Raycast(TileCheckUp, out HitUp, TileGapDistance)){ 
			if(HitUp.collider.tag == "Player"){
				PlayerIsOnThisBlock = true;
				gameObject.tag = "UnAvailable";
				NextToPlayersTile = false;
				if(PlayerMoveNum == 1){
					Floor.BroadcastMessage ("PlayerNotHere");
				}
				PlayerMoveNum = 0;
			}
			if(HitUp.collider.tag == "Hero"){
				HeroIsOnThisBlock = true;
				gameObject.tag = "UnAvailable";
				NextToPlayersTile = false;
			}
			if(HitUp.collider.tag == "Ai"){
				AiIsOnThisBlock = true;
				gameObject.tag = "UnAvailable";
				NextToPlayersTile = false;
			}
			if(HitUp.collider.tag == "Box"){
				BoxIsOnThisBlock = true;
				gameObject.tag = "UnAvailable";
				NextToPlayersTile = false;
			}
		}

		//sets everythign to false if the ray missed.
		else{
			PlayerIsOnThisBlock = false;
			HeroIsOnThisBlock = false;
			AiIsOnThisBlock = false;
			BoxIsOnThisBlock = false;
			gameObject.tag = "Available";
			PlayerMoveNum = 1;
		}
	}
	//END of RayCasting


	//checks the tag of this tile, then sets colour accordingly
	void AvailabilityChecker(){
		if(gameObject.tag == "UnAvailable" ){
			newColour.a = 1.0f;
			ThisTile.renderer.material.color = newColour;
			ThisTile.renderer.material.shader = Shader.Find("Diffuse");
			gameObject.renderer.material.color = Color.grey;
		}
		if(gameObject.tag == "Available" && NextToPlayersTile == false && MouseOverTile == false && IsAThreat == false && CanMoveHere == false){
			newColour.a = 0.0f;
			ThisTile.renderer.material.color = newColour;
			ThisTile.renderer.material.shader = Shader.Find("Transparent/Diffuse");
		}
//		if(gameObject.tag == "Available" && NextToPlayersTile == true && MouseOverTile == false && IsAThreat == false && PlayerInteractive == 0 && HerosPath == 0){
//			if (colourDelay == false){
//			//StartCoroutine(NextToPlayerColourDelay());
//			}


	}

	//This is for the green colour delay placeholder
//	public IEnumerator NextToPlayerColourDelay(){
//		colourDelay = true;
//		float greenDelay = 1.5f;
//		float whiteDelay = 1.5f;
//		gameObject.renderer.material.color = Color.green;
//		yield return new WaitForSeconds (whiteDelay);
//		gameObject.renderer.material.color = Color.white;
//		yield return new WaitForSeconds(greenDelay);
//		colourDelay = false;




	//Uses Raycast collison with layer 8 (Tiles) then moves player to clicked tile.
	//Note, this also only allows movement to an adjacent tile.
	void MouseUp(){
		ap = Controller.GetComponent<Game_Controler>().AP;
		RaycastHit HitMe;
		int layerMask = 1 << 8;

		if (Input.GetButtonDown("Fire1") && _gameCon.isPlayersTurn == true) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out HitMe,Mathf.Infinity,layerMask)){
				if(HitMe.collider.gameObject == ThisTile && CanMoveHere == true && ap >= _gameCon.CurrentMovementCost){
					Set = false;
					StartLerping();
					//Controller.SendMessage ("MovePlayer");
					StartCoroutine(WaitAndGo(0.01F));
				}
			}
		}
		if (Input.GetButtonDown("Fire2") && _gameCon.isPlayersTurn == true && _gameCon.GrenadeCount > 0) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out HitMe,Mathf.Infinity,layerMask)){
				if(HitMe.collider.gameObject == ThisTile && isSmokeable == true && ap >= 1){
					_gameCon.GrenadeCount -= 1;
					GameObject SmokeGrenade = Instantiate (_gameCon.Grenade, Node.transform.position, Node.transform.rotation) as GameObject;
				}
			}
		}
	}
	//This gathers the required information to move, before the fixd update begins.
	void StartLerping(){
			__Animation.inst.anim.SetBool("WalkAnimIdle",false);
			__Animation.inst.anim.SetBool("WalkAnim",true);
			__Animation.inst.anim.SetBool("RunAnim",false);
			__Animation.inst.anim.SetBool("RunAnimIdle",false);
			__Animation.inst.anim.SetBool("CrouchAnim",false);
			__Animation.inst.anim.SetBool("CrouchAnimIdle",false);
			__Animation.inst.anim.SetBool("Interact",false);
			print ("walk anim");



		_gameCon.isLerping = true;
		isLerping = true;
		timeStartedLerping = Time.time;

		startPosition = Player.transform.position;
		endPosition = Node.transform.position;
	}

	//this is a set update, that moves the player from tile to tile.
	void FixedUpdate()
	{
		if(isLerping)
		{
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

			Player.transform.position = Vector3.Lerp (startPosition, endPosition, percentageComplete);
			//This Stops the player from being able to glitch across tiles.
			if(percentageComplete >= 0.3f)
			{
				Floor.BroadcastMessage ("NotNextToPlayer");
			}
			//this tells the PlayerCharacter that it has reched it's destination, and to stop moving.
			if(percentageComplete >= 1.0f)
			{
				isLerping = false;
				_gameCon.isLerping = false;
			}
		}
	}

	//player feedback for hovering cursor over a tile
	void RayFromMouse(){

		RaycastHit OverMe;
		int layerMask = 1 << 8;

		if (_gameCon.isPlayersTurn == true){
			Ray Cast = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(Cast, out OverMe,Mathf.Infinity,layerMask)){
				if(OverMe.collider.gameObject == ThisTile && CanMoveHere == true && ap >= _gameCon.CurrentMovementCost){

					Floor.BroadcastMessage("unselectTile");
					MouseOverTile = true;
					if(gameObject.tag == "Available" && PlayerInteractive == 0 && CanMoveHere == false){
						newColour.a = 0.0f;
						ThisTile.renderer.material.color = newColour;
						ThisTile.renderer.material.shader = Shader.Find("Transparent/Diffuse");
						gameObject.renderer.material.color = Color.white;
					//	print("mouse over available empty tile");
					}
					else{
						unselectTile();
					}

					//tell player they can move to this tile
					if(gameObject.tag == "Available" && PlayerInteractive == 0 && CanMoveHere == true && _gameCon.AP >=0 ){
						newColour.a = 1.0f;
						ThisTile.renderer.material.color = newColour;
						ThisTile.renderer.material.shader = Shader.Find("Diffuse");
						gameObject.renderer.material.color = Color.green;
					//	print("mouse over available empty tile");
					}
					else{
						unselectTile();
					}

					//tell player this tile is interactive
					if(gameObject.tag == "Available" && PlayerInteractive == 1 && CanMoveHere == true && _gameCon.AP >=1 ){
						newColour.a = 1.0f;
						ThisTile.renderer.material.color = newColour;
						ThisTile.renderer.material.shader = Shader.Find("Diffuse");
						gameObject.renderer.material.color = Color.blue;
					//	print("mouse over interactive tile");
					}
					else{
						unselectTile();
					}

					//tell player an enemy patrols this route
					if(gameObject.tag == "Available" && AIPathChannel == 1 ||gameObject.tag == "Available" && HerosPath == 1){
						newColour.a = 1.0f;
						ThisTile.renderer.material.color = newColour;
						ThisTile.renderer.material.shader = Shader.Find("Diffuse");
						gameObject.renderer.material.color = Color.red;
					//	print ("enemy path tile");
					}
					else{
						unselectTile();
					}
				}
			}
		}
	}

	void unselectTile(){
		MouseOverTile = false;
	}

	//fixes a bug with selectable tiles
	IEnumerator WaitAndGo(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		Floor.BroadcastMessage ("NotNextToPlayer");
	}
	//message reciver
	void NextToPlayer(){
		NextToPlayersTile = true;
	}
	//message reciver
	void NotNextToPlayer(){
		NextToPlayersTile = false;
	}
	//message reciever, to fix 'player on 2 tiles' bug
	void PlayerNotHere(){
		PlayerIsOnThisBlock = false;
	}
	void AiWasHere(){
		//change Channel number to 0
	}
	void HeroNotHere(){
		HeroIsOnThisBlock = false;
	}
	void ThreatMapped(){
		IsAThreat = true;
		newColour.a = 1.0f;
		ThisTile.renderer.material.color = newColour;
		ThisTile.renderer.material.shader = Shader.Find("Diffuse");
		ThisTile.renderer.material.color = Color.red;
	}
	void MovementMapped(){
		CanMoveHere = true;
		if (IsAThreat == false) {
			newColour.a = 1.0f;
			ThisTile.renderer.material.color = newColour;
			ThisTile.renderer.material.shader = Shader.Find("Diffuse");
			ThisTile.renderer.material.color = Color.green;
		}
	}
	void Smokable(){
		isSmokeable = true;
	}
	void HeroOnMe(){
		HeroIsOnThisBlock = true;
	}
	void DeMapped(){
		IsAThreat = false;
		CanMoveHere = false;
		//isSmokeable = false;
	}
	void LightOff(){
		LightDetectingThisTile = false;
	}

	void NextStage(){
		if(LevelEnd == 1 && HeroIsOnThisBlock == true){
			Application.LoadLevel (4);
		}
		if(LevelEnd == 2 && HeroIsOnThisBlock == true){
			Application.LoadLevel (5);
		}
		if(LevelEnd == 3 && HeroIsOnThisBlock == true){
			Application.LoadLevel (6);
		}
	}

	//This tells the hero he is in action.
	void HeroAction(){

		if(HeroIsOnThisBlock == true && HerosActionMomentHere == true){
			_gameCon.HeroAction = true;
		}
		if(HeroIsOnThisBlock == true && HerosActionMomentHere == false){
			_gameCon.HeroAction = false;
		}

		if(PlayerIsOnThisBlock == true && WorthPointsIfOn == true && ScoreWorth == 1){
			_gameCon.PlayersCurrentTilePoints = 1;
		}
		if(PlayerIsOnThisBlock == true && WorthPointsIfOn == true && ScoreWorth == 2){
			_gameCon.PlayersCurrentTilePoints = 2;
		}
		if(PlayerIsOnThisBlock == true && WorthPointsIfOn == true && ScoreWorth == 3){
			_gameCon.PlayersCurrentTilePoints = 3;
		}
		if(PlayerIsOnThisBlock == true && WorthPointsIfOn == false){
			_gameCon.PlayersCurrentTilePoints = 0;
		}
	}
}
