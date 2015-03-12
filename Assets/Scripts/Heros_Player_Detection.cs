using UnityEngine;
using System.Collections;

public class Heros_Player_Detection : MonoBehaviour {

	public int SneakSpotDistance;
	public int RunSpotDistance;
	public int WalkSpotDistance;

	public RaycastHit HitBack;
	public RaycastHit HitForward;
	public RaycastHit HitLeft;
	public RaycastHit HitRight;

	private Game_Controler _gameCon;
	private FloorTile_Controler _tileCon;

	// Use this for initialization
	void Start () {
		_gameCon = GameObject.Find("Main Camera").GetComponent<Game_Controler>();
	}
	
	// Update is called once per frame
	void Update () {
		PlayerDetector();
	}

	void PlayerDetector(){

		Ray TileCheckForward = new Ray(transform.position, Vector3.forward);
		Ray TileCheckBack = new Ray(transform.position, Vector3.back);
		Ray TileCheckLeft = new Ray(transform.position, Vector3.left);
		Ray TileCheckRight = new Ray(transform.position, Vector3.right);

		//Ray TileCheckForwardWalk = new Ray(transform.position, Vector3.forward);
		//Ray TileCheckBackWalk = new Ray(transform.position, Vector3.back);
		//Ray TileCheckLeftWalk = new Ray(transform.position, Vector3.left);
		//Ray TileCheckRightWalk = new Ray(transform.position, Vector3.right);

		//Ray TileCheckForwardRun = new Ray(transform.position, Vector3.forward);
		//Ray TileCheckBackRun = new Ray(transform.position, Vector3.back);
		//Ray TileCheckLeftRun = new Ray(transform.position, Vector3.left);
		//Ray TileCheckRightRun = new Ray(transform.position, Vector3.right);

		if(_gameCon.playerSneaking == true){

			Debug.DrawRay(transform.position, Vector3.left * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * SneakSpotDistance);


			if(Physics.Raycast(TileCheckForward, out HitForward, SneakSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}

			if(Physics.Raycast(TileCheckLeft, out HitLeft, SneakSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}

			if(Physics.Raycast(TileCheckRight, out HitRight, SneakSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}

			if(Physics.Raycast(TileCheckBack, out HitBack, SneakSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}
		}

		if(_gameCon.playerWalking == true){
			
			Debug.DrawRay(transform.position, Vector3.left * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * WalkSpotDistance);
			
			
			if(Physics.Raycast(TileCheckForward, out HitForward, WalkSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}
			
			if(Physics.Raycast(TileCheckLeft, out HitLeft, WalkSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}
			
			if(Physics.Raycast(TileCheckRight, out HitRight, WalkSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}
			
			if(Physics.Raycast(TileCheckBack, out HitBack, WalkSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}
		}

		if(_gameCon.playerRunning == true){
			
			Debug.DrawRay(transform.position, Vector3.left * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * RunSpotDistance);
			
			
			if(Physics.Raycast(TileCheckForward, out HitForward, RunSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}
			
			if(Physics.Raycast(TileCheckLeft, out HitLeft, RunSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}
			
			if(Physics.Raycast(TileCheckRight, out HitRight, RunSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}
			
			if(Physics.Raycast(TileCheckBack, out HitBack, RunSpotDistance)){ 
				if(HitBack.collider.tag == "Player"){
					//DETECTED!
				}
			}
		}
	}
}
