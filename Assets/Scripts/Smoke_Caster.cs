using UnityEngine;
using System.Collections;

public class Smoke_Caster : MonoBehaviour {

	public float ThrowDistance;
	
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
		
		
		if(_gameCon.GrenadeCount > 0){
			
			Debug.DrawRay(transform.position, Vector3.left * ThrowDistance);
			Debug.DrawRay(transform.position, Vector3.right * ThrowDistance);
			Debug.DrawRay(transform.position, Vector3.forward * ThrowDistance);
			Debug.DrawRay(transform.position, Vector3.back * ThrowDistance);
			
			hitsForward = Physics.RaycastAll(transform.position, Vector3.forward, ThrowDistance);
			hitsBack = Physics.RaycastAll(transform.position, Vector3.back, ThrowDistance);
			hitsLeft = Physics.RaycastAll(transform.position, Vector3.left, ThrowDistance);
			hitsRight = Physics.RaycastAll(transform.position, Vector3.right, ThrowDistance);
			
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
				if (rendForward && hitForward.collider.tag == "Available" && _gameCon.AP >= _gameCon.SneakMovementCost) {
					hitForward.collider.SendMessage ("Smokable");
				}
				a++;
			}
			while (b < hitsBack.Length) {
				RaycastHit hitBack = hitsBack[b];
				Renderer rendBack = hitBack.transform.GetComponent<Renderer>();
				
				if (rendBack && (hitBack.collider.tag == "UnAvailable" || hitBack.collider.tag == "Wall")) {
					break;
				}
				if (rendBack && hitBack.collider.tag == "Available" && _gameCon.AP >= _gameCon.SneakMovementCost) {
					hitBack.collider.SendMessage ("Smokable");
				}
				b++;
			}
			while (c < hitsLeft.Length) {
				RaycastHit hitLeft = hitsLeft[c];
				Renderer rendLeft = hitLeft.transform.GetComponent<Renderer>();
				
				if (rendLeft && (hitLeft.collider.tag == "UnAvailable" || hitLeft.collider.tag == "Wall")) {
					break;
				}
				if (rendLeft && hitLeft.collider.tag == "Available" && _gameCon.AP >= _gameCon.SneakMovementCost) {
					hitLeft.collider.SendMessage ("Smokable");
				}
				c++;
			}
			while (d < hitsRight.Length) {
				RaycastHit hitRight = hitsRight[d];
				Renderer rendRight = hitRight.transform.GetComponent<Renderer>();
				
				if (rendRight && (hitRight.collider.tag == "UnAvailable" || hitRight.collider.tag == "Wall")) {
					break;
				}
				if (rendRight && hitRight.collider.tag == "Available" && _gameCon.AP >= _gameCon.SneakMovementCost) {
					hitRight.collider.SendMessage ("Smokable");
				}
				d++;
			}
		}
	}
}