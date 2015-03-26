using UnityEngine;
using System.Collections;

public class EnemyPathing : MonoBehaviour {

	public GameObject AI;//Hero
	public GameObject TileUnderAI;
	public GameObject Controller;
	private Game_Controler _gameCon;
	private FloorTile_Controler _tileCon;

	public GameObject TileForward;
	public GameObject TileBack;
	public GameObject TileLeft;
	public GameObject TileRight;
	public bool aiTurn;
	public int AIPathChannel = 1;
	public int MoveDir;
	public int ChanceToMove;


	public int AIsMoves = 1;

	//Stuff For Lerping
	private float timeStartedLerping;
	private bool isLerping;
	private float journeyLength;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float timeTakenDuringLerp;

	// Use this for initialization
	void Start () {
		_gameCon = GameObject.Find("Main Camera").GetComponent<Game_Controler>();
		timeTakenDuringLerp = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		whereAmI();
		isItMyTurn();
		MoveDir = Random.Range(1,5);
		ChanceToMove = Random.Range(1,3);
	}

	//Tells the AI what block they are on, where they can move to, and where they are moving.
	void whereAmI(){
		RaycastHit HitDown;
		int layerMask = 1 << 8;
		
		Ray TileCheckDown = new Ray(transform.position, Vector3.down);
		Debug.DrawRay(transform.position, Vector3.down * 1.0f);
		
		if(Physics.Raycast(TileCheckDown, out HitDown, 1.0f,layerMask)){ 
			
			TileUnderAI = HitDown.transform.gameObject;
			TileForward = TileUnderAI.GetComponent<FloorTile_Controler>().TileForward;
			TileBack = TileUnderAI.GetComponent<FloorTile_Controler>().TileBack;
			TileLeft = TileUnderAI.GetComponent<FloorTile_Controler>().TileLeft;
			TileRight = TileUnderAI.GetComponent<FloorTile_Controler>().TileRight;
			
			if(TileUnderAI.GetComponent<FloorTile_Controler>().AiIsOnThisBlock == true && aiTurn == true && AIsMoves >= 1 && isLerping == false){
		
				//TileUnderAI.GetComponent<FloorTile_Controler>().AIPathChannel = 0;
				ChanceToMove = Random.Range(1,3);
				if(TileForward){
					if(TileForward.GetComponent<FloorTile_Controler>().AIPathChannel == AIPathChannel && 
					   TileForward.GetComponent<FloorTile_Controler>().tag == "Available" && MoveDir == 1 /*&& ChanceToMove == 1*/){
						StartLerpingForward();
					}
				}
				if(TileBack){
					if(TileBack.GetComponent<FloorTile_Controler>().AIPathChannel == AIPathChannel && 
					   TileBack.GetComponent<FloorTile_Controler>().tag == "Available" && MoveDir == 2 /*&& ChanceToMove == 1*/){
						StartLerpingBack();
					}
				}
				if(TileLeft){
					if(TileLeft.GetComponent<FloorTile_Controler>().AIPathChannel == AIPathChannel && 
					   TileLeft.GetComponent<FloorTile_Controler>().tag == "Available" && MoveDir == 3 /*&& ChanceToMove == 1*/){
						StartLerpingLeft();
					}
				}
				if(TileRight){
					if(TileRight.GetComponent<FloorTile_Controler>().AIPathChannel == AIPathChannel && 
					   TileRight.GetComponent<FloorTile_Controler>().tag == "Available" && MoveDir == 4 /*&& ChanceToMove == 1*/){
						StartLerpingRight();
					}
				}
			//	if(ChanceToMove == 2){
			//		AIsMoves = 0;
			//	}
			}
		}
	}

	//This is Setup information needed before movement can be done in the fixed update.
	void StartLerpingForward(){
		journeyLength = Vector3.Distance(AI.transform.position, TileForward.GetComponent<FloorTile_Controler>().Node.transform.position);
		
		isLerping = true;
		timeStartedLerping = Time.time;
		
		startPosition = AI.transform.position;
		endPosition = TileForward.GetComponent<FloorTile_Controler>().Node.transform.position;
	}
	void StartLerpingBack(){
		journeyLength = Vector3.Distance(AI.transform.position, TileBack.GetComponent<FloorTile_Controler>().Node.transform.position);
		
		isLerping = true;
		timeStartedLerping = Time.time;
		
		startPosition = AI.transform.position;
		endPosition = TileBack.GetComponent<FloorTile_Controler>().Node.transform.position;
	}
	void StartLerpingLeft(){
		journeyLength = Vector3.Distance(AI.transform.position, TileLeft.GetComponent<FloorTile_Controler>().Node.transform.position);
		
		isLerping = true;
		timeStartedLerping = Time.time;
		
		startPosition = AI.transform.position;
		endPosition = TileLeft.GetComponent<FloorTile_Controler>().Node.transform.position;
	}
	void StartLerpingRight(){
		journeyLength = Vector3.Distance(AI.transform.position, TileRight.GetComponent<FloorTile_Controler>().Node.transform.position);
		
		isLerping = true;
		timeStartedLerping = Time.time;
		
		startPosition = AI.transform.position;
		endPosition = TileRight.GetComponent<FloorTile_Controler>().Node.transform.position;
	}

	//This is where movement is done.
	void FixedUpdate()
	{
		if(isLerping)
		{
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;
			
			AI.transform.position = Vector3.Lerp (startPosition, endPosition, percentageComplete);
			
			if(percentageComplete >= 1.0f)
			{
				AIsMoves -= 1;
				print("AIMoves = " + AIsMoves);
				isLerping = false;
			}
		}
	}

	//tells us that it's the AI's turn
	void isItMyTurn(){
		if(aiTurn == false && _gameCon.HighAlert == false){
			AIsMoves = 1;
			print ("A.I Is Patrolling Normally");
		}
		else if(aiTurn == false && _gameCon.HighAlert == true){
			AIsMoves = 2;
			print ("A.I. Is on High alert");
		}
		aiTurn = Controller.GetComponent<Game_Controler>().isAiTurn;
		if(AIsMoves <= 0){
			AIsMoves = 0;
		}
	}
}
