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
			
			TileUnderAI = HitDown.transform.gameObject;
			TileForward = TileUnderAI.GetComponent<FloorTile_Controler>().TileForward;
			TileBack = TileUnderAI.GetComponent<FloorTile_Controler>().TileBack;
			TileLeft = TileUnderAI.GetComponent<FloorTile_Controler>().TileLeft;
			TileRight = TileUnderAI.GetComponent<FloorTile_Controler>().TileRight;
			
			if(TileUnderAI.GetComponent<FloorTile_Controler>().AiIsOnThisBlock == true && aiTurn == true && AIsMoves >= 1 && isLerping == false){
				
				TileUnderAI.GetComponent<FloorTile_Controler>().HerosPath = 0;
				if(TileForward){
					if(TileForward.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileForward.GetComponent<FloorTile_Controler>().tag == "Available"){
						StartLerpingForward();
					}
				}
				if(TileBack){
					if(TileBack.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileBack.GetComponent<FloorTile_Controler>().tag == "Available"){
						StartLerpingBack();
					}
				}
				if(TileLeft){
					if(TileLeft.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileLeft.GetComponent<FloorTile_Controler>().tag == "Available"){
						StartLerpingLeft();
					}
				}
				if(TileRight){
					if(TileRight.GetComponent<FloorTile_Controler>().HerosPath == 1 && TileRight.GetComponent<FloorTile_Controler>().tag == "Available"){
						StartLerpingRight();
					}
				}
			}
		}
	}
	
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
				isLerping = false;
			}
		}
	}
	
	//tells us that it's the heros turn
	void isItMyTurn(){
		
		aiTurn = Controller.GetComponent<Game_Controler>().isAiTurn;
		if(AIsMoves <= 0){
			AIsMoves = 0;
			Controller.SendMessage ("ActivatePlayerTurn");
		}
	}
}
