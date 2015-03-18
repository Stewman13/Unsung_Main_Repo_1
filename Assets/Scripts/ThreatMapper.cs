using UnityEngine;
using System.Collections;

public class ThreatMapper : MonoBehaviour {

	
	public int SneakSpotDistance;
	public int RunSpotDistance;
	public int WalkSpotDistance;

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
		_gameCon = GameObject.Find("Main Camera").GetComponent<Game_Controler>();
	}
	
	// Update is called once per frame
	void Update () {
		ThreatMap();
	}
	
	void ThreatMap(){

		RaycastHit[] hitsForward;
		RaycastHit[] hitsBack;
		RaycastHit[] hitsLeft;
		RaycastHit[] hitsRight;

		int layerMask = 1 << 8;

		if(_gameCon.playerSneaking == true){
			
			Debug.DrawRay(transform.position, Vector3.left * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * SneakSpotDistance);

			hitsForward = Physics.RaycastAll(transform.position, Vector3.forward, SneakSpotDistance, layerMask);
			hitsBack = Physics.RaycastAll(transform.position, Vector3.back, SneakSpotDistance, layerMask);
			hitsLeft = Physics.RaycastAll(transform.position, Vector3.left, SneakSpotDistance, layerMask);
			hitsRight = Physics.RaycastAll(transform.position, Vector3.right, SneakSpotDistance, layerMask);
		
			int a = 0;
			int b = 0;
			int c = 0;
			int d = 0;
			while (a < hitsForward.Length) {
				RaycastHit hitForward = hitsForward[a];
				Renderer rendForward = hitForward.transform.GetComponent<Renderer>();
				if (rendForward && (hitForward.collider.tag == "UnAvailable" || hitForward.collider.tag == "Wall")) {
					break;
				}
				if (rendForward && hitForward.collider.tag == "Available") {
					hitForward.collider.SendMessage ("ThreatMapped");
				}
				a++;
			}
			while (b < hitsBack.Length) {
				RaycastHit hitBack = hitsBack[b];
				Renderer rendBack = hitBack.transform.GetComponent<Renderer>();
				if (rendBack && (hitBack.collider.tag == "UnAvailable" || hitBack.collider.tag == "Wall")) {
					break;
				}
				if (rendBack && hitBack.collider.tag == "Available") {
					hitBack.collider.SendMessage ("ThreatMapped");
				}
				b++;
			}
			while (c < hitsLeft.Length) {
				RaycastHit hitLeft = hitsLeft[c];
				Renderer rendLeft = hitLeft.transform.GetComponent<Renderer>();
				if (rendLeft && (hitLeft.collider.tag == "UnAvailable" || hitLeft.collider.tag == "Wall")) {
					break;
				}
				if (rendLeft && hitLeft.collider.tag == "Available") {
					hitLeft.collider.SendMessage ("ThreatMapped");
				}
				c++;
			}
			while (d < hitsRight.Length) {
				RaycastHit hitRight = hitsRight[d];
				Renderer rendRight = hitRight.transform.GetComponent<Renderer>();
				if (rendRight && (hitRight.collider.tag == "UnAvailable" || hitRight.collider.tag == "Wall")) {
					break;
				}
				if (rendRight && hitRight.collider.tag == "Available") {
					hitRight.collider.SendMessage ("ThreatMapped");
				}
				d++;
			}
		}
		
		if(_gameCon.playerWalking == true){
			
			Debug.DrawRay(transform.position, Vector3.left * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * WalkSpotDistance);

			hitsForward = Physics.RaycastAll(transform.position, Vector3.forward, WalkSpotDistance, layerMask);
			hitsBack = Physics.RaycastAll(transform.position, Vector3.back, WalkSpotDistance, layerMask);
			hitsLeft = Physics.RaycastAll(transform.position, Vector3.left, WalkSpotDistance, layerMask);
			hitsRight = Physics.RaycastAll(transform.position, Vector3.right, WalkSpotDistance, layerMask);

			int a = 0;
			int b = 0;
			int c = 0;
			int d = 0;
			while (a < hitsForward.Length) {
				RaycastHit hitForward = hitsForward[a];
				Renderer rendForward = hitForward.transform.GetComponent<Renderer>();
				if (rendForward && (hitForward.collider.tag == "UnAvailable" || hitForward.collider.tag == "Wall")) {
					break;
				}
				if (rendForward && hitForward.collider.tag == "Available") {
					hitForward.collider.SendMessage ("ThreatMapped");
				}
				a++;
			}
			while (b < hitsBack.Length) {
				RaycastHit hitBack = hitsBack[b];
				Renderer rendBack = hitBack.transform.GetComponent<Renderer>();
				if (rendBack && (hitBack.collider.tag == "UnAvailable" || hitBack.collider.tag == "Wall")) {
					break;
				}
				if (rendBack && hitBack.collider.tag == "Available") {
					hitBack.collider.SendMessage ("ThreatMapped");
				}
				b++;
			}
			while (c < hitsLeft.Length) {
				RaycastHit hitLeft = hitsLeft[c];
				Renderer rendLeft = hitLeft.transform.GetComponent<Renderer>();
				if (rendLeft && (hitLeft.collider.tag == "UnAvailable" || hitLeft.collider.tag == "Wall")) {
					break;
				}
				if (rendLeft && hitLeft.collider.tag == "Available") {
					hitLeft.collider.SendMessage ("ThreatMapped");
				}
				c++;
			}
			while (d < hitsRight.Length) {
				RaycastHit hitRight = hitsRight[d];
				Renderer rendRight = hitRight.transform.GetComponent<Renderer>();
				if (rendRight && (hitRight.collider.tag == "UnAvailable" || hitRight.collider.tag == "Wall")) {
					break;
				}
				if (rendRight && hitRight.collider.tag == "Available") {
					hitRight.collider.SendMessage ("ThreatMapped");
				}
				d++;
			}
		}
		
		if(_gameCon.playerRunning == true){
			
			Debug.DrawRay(transform.position, Vector3.left * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * RunSpotDistance);


			hitsForward = Physics.RaycastAll(transform.position, Vector3.forward, RunSpotDistance, layerMask);
			hitsBack = Physics.RaycastAll(transform.position, Vector3.back, RunSpotDistance, layerMask);
			hitsLeft = Physics.RaycastAll(transform.position, Vector3.left, RunSpotDistance, layerMask);
			hitsRight = Physics.RaycastAll(transform.position, Vector3.right, RunSpotDistance, layerMask);
		
		    int a = 0;
		    int b = 0;
		    int c = 0;
            int d = 0;
			while (a < hitsForward.Length) {
				RaycastHit hitForward = hitsForward[a];
				Renderer rendForward = hitForward.transform.GetComponent<Renderer>();
				if (rendForward && (hitForward.collider.tag == "UnAvailable" || hitForward.collider.tag == "Wall")) {
					break;
				}
				if (rendForward && hitForward.collider.tag == "Available") {
					hitForward.collider.SendMessage ("ThreatMapped");
				}
				a++;
			}
			while (b < hitsBack.Length) {
				RaycastHit hitBack = hitsBack[b];
				Renderer rendBack = hitBack.transform.GetComponent<Renderer>();
				if (rendBack && (hitBack.collider.tag == "UnAvailable" || hitBack.collider.tag == "Wall")) {
					break;
				}
				if (rendBack && hitBack.collider.tag == "Available") {
					hitBack.collider.SendMessage ("ThreatMapped");
				}
				b++;
			}
			while (c < hitsLeft.Length) {
				RaycastHit hitLeft = hitsLeft[c];
				Renderer rendLeft = hitLeft.transform.GetComponent<Renderer>();
				if (rendLeft && (hitLeft.collider.tag == "UnAvailable" || hitLeft.collider.tag == "Wall")) {
					break;
				}
				if (rendLeft && hitLeft.collider.tag == "Available") {
					hitLeft.collider.SendMessage ("ThreatMapped");
				}
				c++;
			}
			while (d < hitsRight.Length) {
				RaycastHit hitRight = hitsRight[d];
				Renderer rendRight = hitRight.transform.GetComponent<Renderer>();
				if (rendRight && (hitRight.collider.tag == "UnAvailable" || hitRight.collider.tag == "Wall")) {
					break;
				}
				if (rendRight && hitRight.collider.tag == "Available") {
					hitRight.collider.SendMessage ("ThreatMapped");
				}
				d++;
			}
        }
	}
}