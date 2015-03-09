using UnityEngine;
using System.Collections;


//I added a comment
public class FloorTile_Controler : MonoBehaviour {
	
	public float TileGapDistance = 1.0f;
	public GameObject Player;
	public GameObject Node;
	public GameObject Floor;
	public GameObject ThisTile;
	public GameObject Controller;

	//Movement Avalability list
	public bool ForwardAvailable = false;
	public bool BackAvailable = false;
	public bool LeftAvailable = false;
	public bool RightAvailable = false;

	public bool PlayerIsOnThisBlock = false;
	public bool HeroIsOnThisBlock = false;
	public bool AiIsOnThisBlock = false;
	public bool BoxIsOnThisBlock = false;

	public bool NextToPlayersTile = false;
	private int ap;

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
		NextToPlayersTile = false;
	}

	// Run every update
	void Update () {
		TileDetector();
		AvailabilityChecker();
		MouseUp();
	}
	//this makes sure all tiles are detected at all times.

	//START. Raycasts, to detect tiles in all directions
	//This allows us to check all tile variables using tags! 
	void TileDetector(){
		RaycastHit HitUp;
		RaycastHit HitBack;
		RaycastHit HitForward;
		RaycastHit HitLeft;
		RaycastHit HitRight;
			
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
				if(PlayerIsOnThisBlock == true){
					HitForward.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		if(Physics.Raycast(TileCheckLeft, out HitLeft, TileGapDistance)){ 
			if(HitLeft.collider.tag == "Available"){
				LeftAvailable = true;
				if(PlayerIsOnThisBlock == true){
					HitLeft.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		if(Physics.Raycast(TileCheckRight, out HitRight, TileGapDistance)){ 
			if(HitRight.collider.tag == "Available"){
				RightAvailable = true;
				if(PlayerIsOnThisBlock == true){
					HitRight.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		//Checks if UnAvailable
		if(Physics.Raycast(TileCheckBack, out HitBack, TileGapDistance)){ 
			if(HitBack.collider.tag == "UnAvailable"){
				BackAvailable = false;
				if(PlayerIsOnThisBlock == true){
					HitBack.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		if(Physics.Raycast(TileCheckForward, out HitForward, TileGapDistance)){ 
			if(HitForward.collider.tag == "UnAvailable"){
				ForwardAvailable = false;
				if(PlayerIsOnThisBlock == true){
					HitForward.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		if(Physics.Raycast(TileCheckLeft, out HitLeft, TileGapDistance)){ 
			if(HitLeft.collider.tag == "UnAvailable"){
				LeftAvailable = false;
				if(PlayerIsOnThisBlock == true){
					HitLeft.collider.SendMessage ("NextToPlayer");
				}
			}
		}
		if(Physics.Raycast(TileCheckRight, out HitRight, TileGapDistance)){ 
			if(HitRight.collider.tag == "UnAvailable"){
				RightAvailable = false;
				if(PlayerIsOnThisBlock == true){
					HitRight.collider.SendMessage ("NextToPlayer");
				}
			}
		}


		//Checks for any form of player or Ai.  
		//*IMPORTANT* it is possible to have the player RayDown, then send Messagse.
		//Keep this in mind for future reference!! could be usefull.
		if(Physics.Raycast(TileCheckUp, out HitUp, TileGapDistance)){ 
			if(HitUp.collider.tag == "Player"){
				PlayerIsOnThisBlock = true;
				gameObject.tag = "UnAvailable";
				NextToPlayersTile = false;
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
		}
	}
	//END of RayCasting


	//checks the tag of this tile, then sets colour accordingly
	void AvailabilityChecker(){
		if(gameObject.tag == "UnAvailable"){
			gameObject.renderer.material.color = Color.grey;
		}
		if(gameObject.tag == "Available"){
			gameObject.renderer.material.color = Color.white;
		}
	}

	//Uses Raycast collison with layer 8 (Tiles) then moves player to clicked tile.
	//Note, this also only allows movement to an adjacent tile.
	void MouseUp(){
		ap = Controller.GetComponent<Game_Controler>().AP;
		RaycastHit HitMe;
		int layerMask = 1 << 8;

		if (Input.GetButtonDown("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out HitMe,Mathf.Infinity,layerMask)){
				if(HitMe.collider.gameObject == ThisTile && NextToPlayersTile == true && ap >= 1){
					Player.transform.position = Node.transform.position;
					Controller.SendMessage ("MovePlayer");
					StartCoroutine(WaitAndGo(0.1F));
				}
			}
		}
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
}
