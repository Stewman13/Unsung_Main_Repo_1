using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

	public GameObject Player;
	public GameObject TileUnderPlayer;
	public GameObject Controller;
	private Game_Controler _gameCon;
	private FloorTile_Controler _tileCon;
	
//	public GameObject TileForward;
//	public GameObject TileBack;
//	public GameObject TileLeft;
//	public GameObject TileRight;

	private bool SelfPlayerTurnCheck;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		whereAmI();

	}

	void whereAmI(){
		RaycastHit HitDown;
		int layerMask = 1 << 8;
		
		Ray TileCheckDown = new Ray(transform.position, Vector3.down);
		Debug.DrawRay(transform.position, Vector3.down * 1.0f);
		
		if(Physics.Raycast(TileCheckDown, out HitDown, 1.0f,layerMask)){ 
			
			TileUnderPlayer = HitDown.transform.gameObject;

			
			if(TileUnderPlayer.GetComponent<FloorTile_Controler>().PlayerIsOnThisBlock == true && Controller.GetComponent<Game_Controler>().isAiTurn == false){
				if(TileUnderPlayer.GetComponent<FloorTile_Controler>().PlayerInteractive == 0){
					Controller.GetComponent<Game_Controler>().P_InteractPickup = false;
					Controller.GetComponent<Game_Controler>().P_InteractCamera = false;
					Controller.GetComponent<Game_Controler>().P_InteractLaser = false;
					Controller.GetComponent<Game_Controler>().P_InteractLight = false;
					Controller.GetComponent<Game_Controler>().DiceIcon = false;
					Debug.Log ("Normal Tile");
				}

				if(TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractCamera == 1){
					Controller.GetComponent<Game_Controler>().DiceIcon = true;
					Controller.GetComponent<Game_Controler>().P_InteractCamera = true;
					Debug.Log ("Player Can Interact: Camera");
				}

				if(TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractLaser == 1){
					Controller.GetComponent<Game_Controler>().DiceIcon = true;
					Controller.GetComponent<Game_Controler>().P_InteractLaser = true;
					Debug.Log ("Player Can Interact: Laser");
				}

				if(TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractLight == 1){
					Controller.GetComponent<Game_Controler>().DiceIcon = true;
					Controller.GetComponent<Game_Controler>().P_InteractLight = true;
					Debug.Log ("Player Can Interact: Light");
				}

				if(TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractPickup == 1){
					Controller.GetComponent<Game_Controler>().DiceIcon = true;
					Controller.GetComponent<Game_Controler>().P_InteractPickup = true;
					Debug.Log ("Player Can Interact: Pickup Item");
				}
			
				//Debug.Log("Checking end of TileUnderPlayer");
				}
			}
		}
}

