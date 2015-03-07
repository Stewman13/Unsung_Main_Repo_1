using UnityEngine;
using System.Collections;

public class FloorTile_Controler : MonoBehaviour {
	
	public float TileGapDistance = 1.0f;
	public GameObject Player;
	public GameObject Node;

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
	}

	//Raycasts, to detect tiles in all directions
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
				//it tells the adjacent block the the player is next to it.
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
			}
		}
		if(Physics.Raycast(TileCheckForward, out HitForward, TileGapDistance)){ 
			if(HitForward.collider.tag == "UnAvailable"){
				ForwardAvailable = false;
			}
		}
		if(Physics.Raycast(TileCheckLeft, out HitLeft, TileGapDistance)){ 
			if(HitLeft.collider.tag == "UnAvailable"){
				LeftAvailable = false;
			}
		}
		if(Physics.Raycast(TileCheckRight, out HitRight, TileGapDistance)){ 
			if(HitRight.collider.tag == "UnAvailable"){
				RightAvailable = false;
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
		else{
			PlayerIsOnThisBlock = false;
			HeroIsOnThisBlock = false;
			AiIsOnThisBlock = false;
			BoxIsOnThisBlock = false;
			gameObject.tag = "Available";
		}
	}

	void AvailabilityChecker(){
		if(gameObject.tag == "UnAvailable"){
			gameObject.renderer.material.color = Color.grey;
		}
		if(gameObject.tag == "Available"){
			gameObject.renderer.material.color = Color.white;
		}
	}

	
	//When Clicked, do something
	void OnMouseUp() {
		Debug.Log("Drag ended!");
		Player.transform.position = Node.transform.position;
	}

	void NextToPlayer(){
		NextToPlayersTile = true;
	}
}
