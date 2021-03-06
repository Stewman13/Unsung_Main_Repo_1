﻿using UnityEngine;
using System.Collections;

public class Heros_Player_Detection : MonoBehaviour {

	public GameObject Hero;
	public GameObject PlayerCharacter;

	//public LineRenderer LineCastLeft;
	//public LineRenderer LineCastRight;
	//public LineRenderer LineCastForward;
	//public LineRenderer LineCastBack;

	public int SneakSpotDistance;
	public int RunSpotDistance;
	public int WalkSpotDistance;
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
	private Player_Controller _PlayerControler;

	// Use this for initialization
	void Start () {
		AlertPlaying = false;
		_gameCon = GameObject.Find("Main Camera").GetComponent<Game_Controler>();
		_PlayerControler = PlayerCharacter.GetComponent<Player_Controller>();
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

			if(Physics.Raycast(TileCheckForward, out HitForward, SneakSpotDistance)){ 
				if(HitForward.collider.tag == "Player"){
					print("DETECTED!");
				}
			}

			if(Physics.Raycast(TileCheckLeft, out HitLeft, SneakSpotDistance)){ 
				if(HitLeft.collider.tag == "Player"){
					print("DETECTED!");
				}
			}

			if(Physics.Raycast(TileCheckRight, out HitRight, SneakSpotDistance)){ 
				if(HitRight.collider.tag == "Player"){
					print("DETECTED!");
				}
			}

			if(Physics.Raycast(TileCheckBack, out HitBack, SneakSpotDistance)){ 
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
			
			if(Physics.Raycast(TileCheckForward, out HitForward, WalkSpotDistance) && AlertPlaying == false){ 
				if(HitForward.collider.tag == "Player"){
					print("DETECTED! P_Walk D_Front");
					StartCoroutine(PlayerDetected());
				}
			}
			
			if(Physics.Raycast(TileCheckLeft, out HitLeft, WalkSpotDistance) && AlertPlaying == false){ 
				if(HitLeft.collider.tag == "Player"){
					print("DETECTED! P_Walk D_Left");
					StartCoroutine(PlayerDetected());
				}
			}
			
			if(Physics.Raycast(TileCheckRight, out HitRight, WalkSpotDistance) && AlertPlaying == false){ 
				if(HitRight.collider.tag == "Player"){
					print("DETECTED! P_Walk D_Right");
					StartCoroutine(PlayerDetected());
				}
			}
			
			if(Physics.Raycast(TileCheckBack, out HitBack, WalkSpotDistance) && AlertPlaying == false){ 
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
			
			if(Physics.Raycast(TileCheckForward, out HitForward, RunSpotDistance) && AlertPlaying == false){ 
				if(HitForward.collider.tag == "Player"){
					print("DETECTED! P_Run D_Forward");
					StartCoroutine(PlayerDetected());
				}
			}
			
			if(Physics.Raycast(TileCheckLeft, out HitLeft, RunSpotDistance) && AlertPlaying == false){ 
				if(HitLeft.collider.tag == "Player"){
					print("DETECTED! P_Run D_Left");
					StartCoroutine(PlayerDetected());
				}
			}
			
			if(Physics.Raycast(TileCheckRight, out HitRight, RunSpotDistance) && AlertPlaying == false){ 
				if(HitRight.collider.tag == "Player"){
					print("DETECTED! P_Run D_Right");
					StartCoroutine(PlayerDetected());
				}
			}
			
			if(Physics.Raycast(TileCheckBack, out HitBack, RunSpotDistance) && AlertPlaying == false){ 
				if(HitBack.collider.tag == "Player"){
					print("DETECTED! P_Run D_Back");
					StartCoroutine(PlayerDetected());
				}
			}
		}
	}

	public IEnumerator PlayerDetected(){
		AlertPlaying = true;
		//send SpottedByEnemyFirst = true;
		_PlayerControler.GetComponent<Player_Controller>().SpottedByEnemyFirst = true;
		float WaitForNotification = 2.0f;
		AudioSource.PlayClipAtPoint(AlertSound,Camera.main.transform.position, 0.3f);
		Instantiate (AlertIcon, gameObject.transform.position, AlertIcon.transform.rotation);
		//print ("detected audio alert");
		yield return new WaitForSeconds(WaitForNotification);
		Application.LoadLevel ("Defeat");	
		AlertPlaying = false; 
	}

}
