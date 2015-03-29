using UnityEngine;
using System.Collections;

public class AI_Player_Detection : MonoBehaviour {

	//public LineRenderer LineCastLeft;
	//public LineRenderer LineCastRight;
	//public LineRenderer LineCastForward;
	//public LineRenderer LineCastBack;
	
	public int SneakSpotDistance;
	public int SneakSpotDistanceTwo;
	public int RunSpotDistance;
	public int RunSpotDistanceTwo;
	public int WalkSpotDistance;
	public int WalkSpotDistanceTwo;
	public AudioClip AlertSound;
	public GameObject AlertIcon;
	private bool AlertPlaying = false;
	
	public RaycastHit HitBack;
	public RaycastHit HitForward;
	public RaycastHit HitLeft;
	public RaycastHit HitRight;
	
	public Vector3 Left;
	public Vector3 Right;
	public Vector3 Forward;
	public Vector3 Back;
	
	private Game_Controler _gameCon;
	private FloorTile_Controler _tileCon;
	
	// Use this for initialization
	void Start () {
		AlertPlaying = false;
		_gameCon = GameObject.Find("Main Camera").GetComponent<Game_Controler>();
		SneakSpotDistanceTwo = SneakSpotDistance;
		RunSpotDistanceTwo = RunSpotDistance;
		WalkSpotDistanceTwo = WalkSpotDistance;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerDetector();
	}
	
	void PlayerDetector(){

		int layerMask = 1 << 9;

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
		
		//LINE CASTS ARE SUPER BROKEN. MIGHT JUST REMOVE THEM!
		
		//This Can be rewritten And Should be! A test is at hand. 
		//ReWrite this so it's shorter. I have mine saved as a Notepad++
		if(_gameCon.playerSneaking == true){
			
			Debug.DrawRay(transform.position, Vector3.left * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * SneakSpotDistance);
			//	LineCastLeft.SetPosition(1, Vector3.left * SneakSpotDistance);
			//	LineCastRight.SetPosition(1, Vector3.right * SneakSpotDistance);
			//	LineCastBack.SetPosition(1, Vector3.back * SneakSpotDistance);
			//	LineCastForward.SetPosition(1, Vector3.forward * SneakSpotDistance);
			
			while(Physics.Raycast(TileCheckForward, out HitForward, SneakSpotDistance,layerMask)){ 
				if(HitForward.collider.tag == "Box" || HitForward.collider.tag == "Wall"){
					break;
				}
				if(HitForward.collider.tag == "Player"){
					print("DETECTED!");
				}
			}
			
			while(Physics.Raycast(TileCheckLeft, out HitLeft, SneakSpotDistance,layerMask)){ 
				if(HitLeft.collider.tag == "Box" || HitLeft.collider.tag == "Wall"){
					break;
				}
				if(HitLeft.collider.tag == "Player"){
					print("DETECTED!");
				}
			}
			
			while(Physics.Raycast(TileCheckRight, out HitRight, SneakSpotDistance,layerMask)){ 
				if(HitRight.collider.tag == "Box" || HitRight.collider.tag == "Wall"){
					break;
				}
				if(HitRight.collider.tag == "Player"){
					print("DETECTED!");
				}
			}
			
			while(Physics.Raycast(TileCheckBack, out HitBack, SneakSpotDistance,layerMask)){ 
				if(HitBack.collider.tag == "Box" || HitBack.collider.tag == "Wall"){
					break;
				}
				if(HitBack.collider.tag == "Player"){
					print("DETECTED!");
				}
			}
		}
		
		if(_gameCon.playerWalking == true){
			
			Debug.DrawRay(transform.position, Vector3.left * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * WalkSpotDistance);
			//	LineCastLeft.SetPosition(1, Vector3.left * WalkSpotDistance);
			//	LineCastRight.SetPosition(1, Vector3.right * WalkSpotDistance);
			//	LineCastBack.SetPosition(1, Vector3.back * WalkSpotDistance);
			//	LineCastForward.SetPosition(1, Vector3.forward * WalkSpotDistance);
			
			while(Physics.Raycast(TileCheckForward, out HitForward, WalkSpotDistance,layerMask) && AlertPlaying == false){ 
				if(HitForward.collider.tag == "Box" || HitForward.collider.tag == "Wall"){
					break;
				}
				if(HitForward.collider.tag == "Player"){
					print("DETECTED! P_Walk D_Front");
					StartCoroutine(PlayerDetected());
				}
			}
			
			while(Physics.Raycast(TileCheckLeft, out HitLeft, WalkSpotDistance,layerMask) && AlertPlaying == false){ 
				if(HitLeft.collider.tag == "Box" || HitLeft.collider.tag == "Wall"){
					break;
				}
				if(HitLeft.collider.tag == "Player"){
					print("DETECTED! P_Walk D_Left");
					StartCoroutine(PlayerDetected());
				}
			}
			
			while(Physics.Raycast(TileCheckRight, out HitRight, WalkSpotDistance,layerMask) && AlertPlaying == false){ 
				if(HitRight.collider.tag == "Box" || HitRight.collider.tag == "Wall"){
					break;
				}
				if(HitRight.collider.tag == "Player"){
					print("DETECTED! P_Walk D_Right");
					StartCoroutine(PlayerDetected());
				}
			}
			
			while(Physics.Raycast(TileCheckBack, out HitBack, WalkSpotDistance,layerMask) && AlertPlaying == false){ 
				if(HitBack.collider.tag == "Box" || HitBack.collider.tag == "Wall"){
					break;
				}
				if(HitBack.collider.tag == "Player"){
					print("DETECTED! P_Walk D_Back");
					StartCoroutine(PlayerDetected());
				}
			}
		}
		
		if(_gameCon.playerRunning == true){
			
			Debug.DrawRay(transform.position, Vector3.left * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * RunSpotDistance);
			//	LineCastLeft.SetPosition(1, Vector3.left * RunSpotDistance);
			//	LineCastRight.SetPosition(1, Vector3.right * RunSpotDistance);
			//	LineCastBack.SetPosition(1, Vector3.back * RunSpotDistance);
			//	LineCastForward.SetPosition(1, Vector3.forward * RunSpotDistance);
			
			while(Physics.Raycast(TileCheckForward, out HitForward, RunSpotDistance,layerMask) && AlertPlaying == false){ 
				if(HitForward.collider.tag == "Box" || HitForward.collider.tag == "Wall"){
					break;
				}
				if(HitForward.collider.tag == "Player"){
					print("DETECTED! P_Run D_Forward");
					StartCoroutine(PlayerDetected());
				}
			}
			
			while(Physics.Raycast(TileCheckLeft, out HitLeft, RunSpotDistance,layerMask) && AlertPlaying == false){ 
				if(HitLeft.collider.tag == "Box" || HitLeft.collider.tag == "Wall"){
					break;
				}
				if(HitLeft.collider.tag == "Player"){
					print("DETECTED! P_Run D_Left");
					StartCoroutine(PlayerDetected());
				}
			}
			
			while(Physics.Raycast(TileCheckRight, out HitRight, RunSpotDistance,layerMask) && AlertPlaying == false){ 
				if(HitRight.collider.tag == "Box" || HitRight.collider.tag == "Wall"){
					break;
				}
				if(HitRight.collider.tag == "Player"){
					print("DETECTED! P_Run D_Right");
					StartCoroutine(PlayerDetected());
				}
			}
			
			while(Physics.Raycast(TileCheckBack, out HitBack, RunSpotDistance,layerMask) && AlertPlaying == false){ 
				if(HitBack.collider.tag == "Box" || HitBack.collider.tag == "Wall"){
					break;
				}
				if(HitBack.collider.tag == "Player"){
					print("DETECTED! P_Run D_Back");
					StartCoroutine(PlayerDetected());
				}
			}
		}
	}
	
	public IEnumerator PlayerDetected(){
		AlertPlaying = true;
		float WaitForNotification = 2.0f;
		AudioSource.PlayClipAtPoint(AlertSound,Camera.main.transform.position, 0.3f);
		Instantiate (AlertIcon, gameObject.transform.position, AlertIcon.transform.rotation);
		//print ("detected audio alert");
		yield return new WaitForSeconds(WaitForNotification);
		Application.LoadLevel ("Defeat");	
		AlertPlaying = false; 
	}
	void Alert(){
		SneakSpotDistance = SneakSpotDistanceTwo * 2;
		RunSpotDistance = RunSpotDistanceTwo * 2;
		WalkSpotDistance = WalkSpotDistanceTwo * 2;
	}
	void UnAlert(){
		SneakSpotDistance = SneakSpotDistanceTwo;
		RunSpotDistance = RunSpotDistanceTwo;
		WalkSpotDistance = WalkSpotDistanceTwo;
	}
}
