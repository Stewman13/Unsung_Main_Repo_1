using UnityEngine;
using System.Collections;

public class FloorTile_Controler : MonoBehaviour {

	//public FloorTile_Controler forward, back, left, right;
	public float TileGapDistance = 1.0f;

	public bool ForwardAvailable = false;
	public bool BackAvailable = false;
	public bool LeftAvailable = false;
	public bool RightAvailable = false;


	void Start(){
		//set bools to false, so there's no bugs
		ForwardAvailable = false;
		BackAvailable = false;
		LeftAvailable = false;
		RightAvailable = false;
	}

	// Run every update
	void Update () {
		TileDetector();
	}

	//Raycasts, to detect tiles in all directions
	//This allows us to check all tile variables using tags! 
	void TileDetector(){
		RaycastHit HitBack;
		RaycastHit HitForward;
		RaycastHit HitLeft;
		RaycastHit HitRight;
			
			
		Ray TileCheckForward = new Ray(transform.position, Vector3.forward);
		Ray TileCheckBack = new Ray(transform.position, Vector3.back);
		Ray TileCheckLeft = new Ray(transform.position, Vector3.left);
		Ray TileCheckRight = new Ray(transform.position, Vector3.right);
			
		Debug.DrawRay(transform.position, Vector3.left * TileGapDistance);
		Debug.DrawRay(transform.position, Vector3.right * TileGapDistance);
		Debug.DrawRay(transform.position, Vector3.forward * TileGapDistance);
		Debug.DrawRay(transform.position, Vector3.back * TileGapDistance);

		//Checks if Available
		if(Physics.Raycast(TileCheckBack, out HitBack, TileGapDistance)){ 
			if(HitBack.collider.tag == "Available"){
				BackAvailable = true;
			}
		}
		if(Physics.Raycast(TileCheckForward, out HitForward, TileGapDistance)){ 
			if(HitForward.collider.tag == "Available"){
				ForwardAvailable = true;
			}
		}
		if(Physics.Raycast(TileCheckLeft, out HitLeft, TileGapDistance)){ 
			if(HitLeft.collider.tag == "Available"){
				LeftAvailable = true;
			}
		}
		if(Physics.Raycast(TileCheckRight, out HitRight, TileGapDistance)){ 
			if(HitRight.collider.tag == "Available"){
				RightAvailable = true;
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
	}
}
