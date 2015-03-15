using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

	public GameObject Hero;
	public GameObject TileUnderHero;
	public GameObject Controller;
	private Game_Controler _gameCon;
	private FloorTile_Controler _tileCon;

	public GameObject TileForward;
	public GameObject TileBack;
	public GameObject TileLeft;
	public GameObject TileRight;
	public bool herosTurn;

	public int herosMoves = 2;

	//Stuff For Lerping
	private float timeStartedLerping;
	private bool isLerping;
	private float journeyLength;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float timeTakenDuringLerp;


	// Use this for initialization
	void Start () {
		timeTakenDuringLerp = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		whereAmI();
		isItMyTurn();
	}

	void whereAmI(){
		RaycastHit HitDown;
		int layerMask = 1 << 8;

		Ray TileCheckDown = new Ray(transform.position, Vector3.down);
		Debug.DrawRay(transform.position, Vector3.down * 1.0f);

		if(Physics.Raycast(TileCheckDown, out HitDown, 1.0f,layerMask)){ 

			TileUnderHero = HitDown.transform.gameObject;
			TileForward = TileUnderHero.GetComponent<FloorTile_Controler>().TileForward;
			TileBack = TileUnderHero.GetComponent<FloorTile_Controler>().TileBack;
			TileLeft = TileUnderHero.GetComponent<FloorTile_Controler>().TileLeft;
			TileRight = TileUnderHero.GetComponent<FloorTile_Controler>().TileRight;
	
			if(TileUnderHero.GetComponent<FloorTile_Controler>().HeroIsOnThisBlock == true && herosTurn == true && herosMoves >= 1 && isLerping == false){

				TileUnderHero.GetComponent<FloorTile_Controler>().HerosPath = 0;
				if(TileForward){
					if(TileForward.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileForward.GetComponent<FloorTile_Controler>().tag == "Available"){
						StartLerpingForward();
					}
					//this part tells the hero, if theres something in your way, end turn. Aka: Wait for smoke to clear.
					if(TileForward.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileForward.GetComponent<FloorTile_Controler>().tag == "UnAvailable"){
						herosMoves -= 1;
					}
				}
				if(TileBack){
					if(TileBack.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileBack.GetComponent<FloorTile_Controler>().tag == "Available" ){
						StartLerpingBack();
					}
					if(TileBack.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileBack.GetComponent<FloorTile_Controler>().tag == "UnAvailable"){
						herosMoves -= 1;
					}
				}
				if(TileLeft){
					if(TileLeft.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileLeft.GetComponent<FloorTile_Controler>().tag == "Available"){
						StartLerpingLeft();
					}
					if(TileLeft.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileLeft.GetComponent<FloorTile_Controler>().tag == "UnAvailable"){
						herosMoves -= 1;
					}
				}
				if(TileRight){
					if(TileRight.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileRight.GetComponent<FloorTile_Controler>().tag == "Available"){
						StartLerpingRight();
					}
					if(TileRight.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileRight.GetComponent<FloorTile_Controler>().tag == "UnAvailable"){
						herosMoves -= 1;
					}
				}
			}
		}
	}

	void StartLerpingForward(){
		journeyLength = Vector3.Distance(Hero.transform.position, TileForward.GetComponent<FloorTile_Controler>().Node.transform.position);
		
		isLerping = true;
		timeStartedLerping = Time.time;
		
		startPosition = Hero.transform.position;
		endPosition = TileForward.GetComponent<FloorTile_Controler>().Node.transform.position;
	}
	void StartLerpingBack(){
		journeyLength = Vector3.Distance(Hero.transform.position, TileBack.GetComponent<FloorTile_Controler>().Node.transform.position);
		
		isLerping = true;
		timeStartedLerping = Time.time;
		
		startPosition = Hero.transform.position;
		endPosition = TileBack.GetComponent<FloorTile_Controler>().Node.transform.position;
	}
	void StartLerpingLeft(){
		journeyLength = Vector3.Distance(Hero.transform.position, TileLeft.GetComponent<FloorTile_Controler>().Node.transform.position);
		
		isLerping = true;
		timeStartedLerping = Time.time;
		
		startPosition = Hero.transform.position;
		endPosition = TileLeft.GetComponent<FloorTile_Controler>().Node.transform.position;
	}
	void StartLerpingRight(){
		journeyLength = Vector3.Distance(Hero.transform.position, TileRight.GetComponent<FloorTile_Controler>().Node.transform.position);
		
		isLerping = true;
		timeStartedLerping = Time.time;
		
		startPosition = Hero.transform.position;
		endPosition = TileRight.GetComponent<FloorTile_Controler>().Node.transform.position;
	}

	void FixedUpdate()
	{
		if(isLerping)
		{
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;
			
			Hero.transform.position = Vector3.Lerp (startPosition, endPosition, percentageComplete);

			if(percentageComplete >= 1.0f)
			{
				herosMoves -= 1;
				isLerping = false;
				print("Hero Lerp Complete");
			}
		}
	}

	//tells us that it's the heros turn
	void isItMyTurn(){
		herosTurn = Controller.GetComponent<Game_Controler>().isAiTurn;
		if(herosTurn == true){
			Controller.GetComponent<Game_Controler>().DiceIcon = true;
			print ("HerosTurn " + "Moves remaining " + herosMoves);
		}
			if(herosMoves <= 0 && herosTurn == true){
				herosMoves = 0;
				Controller.GetComponent<Game_Controler>().DiceIcon = false;
				Controller.SendMessage ("ActivatePlayerTurn");
				print ("Hero: Ended turn successfully");
			}
		}
	}

