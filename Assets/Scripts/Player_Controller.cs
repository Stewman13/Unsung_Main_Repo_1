using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

	public GameObject Player;
	public GameObject PlayerAlertIcon;
	public GameObject TileUnderPlayer;
	public GameObject Controller;
	private Game_Controler _gameCon;
	private FloorTile_Controler _tileCon;

	public int TurnsInLight = 0;

	public AudioClip AlertSound;
	public GameObject AlertIcon;
	private bool AlertPlaying = false;
	
//	public GameObject TileForward;
//	public GameObject TileBack;
//	public GameObject TileLeft;
//	public GameObject TileRight;

	private bool SelfPlayerTurnCheck;
	private bool PlayerOutOfCameraCheck = false;
	public bool SpottedByEnemyFirst = false;

	// Use this for initialization
	void Start () {
		PlayerOutOfCameraCheck = false;
		SpottedByEnemyFirst = false;
		StartCoroutine (PlayerLocationAlert()); //will show the player where the character is located at start of each scene
	}
	
	// Update is called once per frame
	void Update () {
		whereAmI();

	}

	//player character is outside camera, begin game over.
	void OnBecameInvisible(){
		PlayerOutOfCameraCheck = false;
		if (!SpottedByEnemyFirst && gameObject.activeSelf== true){
			Controller.GetComponent<Game_Controler>().isPlayerOutOfCamera = true;
			print ("Player should be dead");
		}

	}

	void whereAmI(){
		RaycastHit HitDown;
		int layerMask = 1 << 8;
		
		Ray TileCheckDown = new Ray(transform.position, Vector3.down);
		Debug.DrawRay(transform.position, Vector3.down * 1.0f);
		
		if(Physics.Raycast(TileCheckDown, out HitDown, 1.0f,layerMask)){ 
			
			TileUnderPlayer = HitDown.transform.gameObject;

			if(PlayerOutOfCameraCheck == false){
				if(TileUnderPlayer.GetComponent<FloorTile_Controler>().PlayerIsOnThisBlock == true && Controller.GetComponent<Game_Controler>().isAiTurn == false){
					if(TileUnderPlayer.GetComponent<FloorTile_Controler>().PlayerInteractive == 0){
						Controller.GetComponent<Game_Controler>().P_InteractPickup = false;
						Controller.GetComponent<Game_Controler>().P_InteractCamera = false;
						Controller.GetComponent<Game_Controler>().P_InteractLaser = false;
						Controller.GetComponent<Game_Controler>().P_InteractLight = false;
						Controller.GetComponent<Game_Controler>().DiceIcon = false;
						//Debug.Log ("Normal Tile");
					}

					if(TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractCamera == 1){
						Controller.GetComponent<Game_Controler>().DiceIcon = true;
						Controller.GetComponent<Game_Controler>().P_InteractCamera = true;

						Controller.GetComponent<Game_Controler>().P_InteractPickup = false;
						Controller.GetComponent<Game_Controler>().P_InteractLaser = false;
						Controller.GetComponent<Game_Controler>().P_InteractLight = false;
						Debug.Log ("Player Can Interact: Camera");
					}

					if(TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractLaser == 1){
						Controller.GetComponent<Game_Controler>().DiceIcon = true;
						Controller.GetComponent<Game_Controler>().P_InteractLaser = true;

						Controller.GetComponent<Game_Controler>().P_InteractPickup = false;
						Controller.GetComponent<Game_Controler>().P_InteractCamera = false;
						Controller.GetComponent<Game_Controler>().P_InteractLight = false;
						Debug.Log ("Player Can Interact: Laser");
					}

					if(TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractLight == 1){
						Controller.GetComponent<Game_Controler>().DiceIcon = true;
						Controller.GetComponent<Game_Controler>().P_InteractLight = true;

						Controller.GetComponent<Game_Controler>().P_InteractPickup = false;
						Controller.GetComponent<Game_Controler>().P_InteractCamera = false;
						Controller.GetComponent<Game_Controler>().P_InteractLaser = false;
						Debug.Log ("Player Can Interact: Light");
					}

					if(TileUnderPlayer.GetComponent<FloorTile_Controler>().Tile_InteractPickup == 1){
						Controller.GetComponent<Game_Controler>().DiceIcon = true;
						Controller.GetComponent<Game_Controler>().P_InteractPickup = true;

						Controller.GetComponent<Game_Controler>().P_InteractCamera = false;
						Controller.GetComponent<Game_Controler>().P_InteractLaser = false;
						Controller.GetComponent<Game_Controler>().P_InteractLight = false;
						Debug.Log ("Player Can Interact: Pickup Item");
					}
				
					//Debug.Log("Checking end of TileUnderPlayer");
					}
				}
			}
		}

	IEnumerator PlayerLocationAlert(){
		float delayTime = 2.0f;
		yield return new WaitForSeconds (delayTime);
		Instantiate (PlayerAlertIcon, gameObject.transform.position, PlayerAlertIcon.transform.rotation);		
	}

	void inLight(){
		//THIS NEEDS TO HAVE SOMETHING DONE TO IT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	}

	void Alert(){
		StartCoroutine(PlayerDetected());
	}

	public IEnumerator PlayerDetected(){
		if (AlertPlaying == false) {
			AlertPlaying = true;
			SpottedByEnemyFirst = true;
			float WaitForNotification = 2.0f;
			AudioSource.PlayClipAtPoint (AlertSound, Camera.main.transform.position, 0.3f);
			Instantiate (AlertIcon, gameObject.transform.position, AlertIcon.transform.rotation);
			yield return new WaitForSeconds (WaitForNotification);
			Application.LoadLevel ("Defeat");	
		}
	}
}

