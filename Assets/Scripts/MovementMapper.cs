using UnityEngine;
using System.Collections;

public class MovementMapper : MonoBehaviour {

	
	public float SneakSpotDistance;
	public float RunSpotDistance;
	public float WalkSpotDistance;
	
	public RaycastHit HitBack;
	public RaycastHit HitForward;
	public RaycastHit HitLeft;
	public RaycastHit HitRight;
	
	public Vector3 Left;
	public Vector3 Right;
	public Vector3 Forward;
	public Vector3 Back;
	private Color newColour;

	private Game_Controler _gameCon;
	private FloorTile_Controler _tileCon;
	
	// Use this for initialization
	void Start () {
		_gameCon = GameObject.Find("Main Camera").GetComponent<Game_Controler>();
		WalkSpotDistance = 1;
		RunSpotDistance = 1;
		SneakSpotDistance = 1;
	}
	
	// Update is called once per frame
	void Update () {
		ThreatMap();
		MovementDistance ();
	}

	void MovementDistance (){
		//Movement for sneak
		if(_gameCon.AP >= 3){
			SneakSpotDistance = 1;
		}
		if(_gameCon.AP < _gameCon.SneakMovementCost){
			SneakSpotDistance = 1.0f;
		}
		//movemnet for Run
		if(_gameCon.AP >= _gameCon.RunMovementCost){
			RunSpotDistance = _gameCon.AP;
		}
		if(_gameCon.AP < _gameCon.RunMovementCost){
			RunSpotDistance = 1.0f;
		}
		//movemnet for Walk
		if(_gameCon.AP > _gameCon.WalkMovementCost){
			WalkSpotDistance = 2.0f;
		}
		if(_gameCon.AP <= _gameCon.WalkMovementCost){
			WalkSpotDistance = 1.0f;
		}
	}
	
	void ThreatMap(){
		
		RaycastHit[] hitsForward;
		RaycastHit[] hitsBack;
		RaycastHit[] hitsLeft;
		RaycastHit[] hitsRight;

		
		if(_gameCon.playerSneaking == true){
			
			Debug.DrawRay(transform.position, Vector3.left * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * SneakSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * SneakSpotDistance);
			
			hitsForward = Physics.RaycastAll(transform.position, Vector3.forward, SneakSpotDistance);
			hitsBack = Physics.RaycastAll(transform.position, Vector3.back, SneakSpotDistance);
			hitsLeft = Physics.RaycastAll(transform.position, Vector3.left, SneakSpotDistance);
			hitsRight = Physics.RaycastAll(transform.position, Vector3.right, SneakSpotDistance);
			
			int a = 0;
			int b = 0;
			int c = 0;
			int d = 0;
			while (a < hitsForward.Length) {
				RaycastHit hitForward = hitsForward[a];
				Renderer rendForward = hitForward.transform.GetComponent<Renderer>();
				if(rendForward && hitForward.collider.tag == "Available" && _gameCon.AP <= 0 && _gameCon.PlayerInteractive == 0){
					newColour.a = 0.0f;
					rendForward.material.color = newColour;
					rendForward.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendForward && (hitForward.collider.tag == "UnAvailable" || hitForward.collider.tag == "Wall")) {
					break;
				}
				if (rendForward && hitForward.collider.tag == "Available" && _gameCon.AP >= _gameCon.SneakMovementCost) {
					hitForward.collider.SendMessage ("MovementMapped");
				}
				a++;
			}
			while (b < hitsBack.Length) {
				RaycastHit hitBack = hitsBack[b];
				Renderer rendBack = hitBack.transform.GetComponent<Renderer>();
				if(rendBack && hitBack.collider.tag == "Available" && _gameCon.AP <= 0 && _gameCon.PlayerInteractive == 0){
					newColour.a = 0.0f;
					rendBack.material.color = newColour;
					rendBack.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendBack && (hitBack.collider.tag == "UnAvailable" || hitBack.collider.tag == "Wall")) {
					break;
				}
				if (rendBack && hitBack.collider.tag == "Available" && _gameCon.AP >= _gameCon.SneakMovementCost) {
					hitBack.collider.SendMessage ("MovementMapped");
				}
				b++;
			}
			while (c < hitsLeft.Length) {
				RaycastHit hitLeft = hitsLeft[c];
				Renderer rendLeft = hitLeft.transform.GetComponent<Renderer>();
				if(rendLeft && hitLeft.collider.tag == "Available" && _gameCon.AP <= 0 && _gameCon.PlayerInteractive == 0){
					newColour.a = 0.0f;
					rendLeft.material.color = newColour;
					rendLeft.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendLeft && (hitLeft.collider.tag == "UnAvailable" || hitLeft.collider.tag == "Wall")) {
					break;
				}
				if (rendLeft && hitLeft.collider.tag == "Available" && _gameCon.AP >= _gameCon.SneakMovementCost) {
					hitLeft.collider.SendMessage ("MovementMapped");
				}
				c++;
			}
			while (d < hitsRight.Length) {
				RaycastHit hitRight = hitsRight[d];
				Renderer rendRight = hitRight.transform.GetComponent<Renderer>();
				if(rendRight && hitRight.collider.tag == "Available" && _gameCon.AP <= 0 && _gameCon.PlayerInteractive == 0){
					newColour.a = 0.0f;
					rendRight.material.color = newColour;
					rendRight.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendRight && (hitRight.collider.tag == "UnAvailable" || hitRight.collider.tag == "Wall")) {
					break;
				}
				if (rendRight && hitRight.collider.tag == "Available" && _gameCon.AP >= _gameCon.SneakMovementCost) {
					hitRight.collider.SendMessage ("MovementMapped");
				}
				d++;
			}
		}
		
		if(_gameCon.playerWalking == true){
			
			Debug.DrawRay(transform.position, Vector3.left * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * WalkSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * WalkSpotDistance);
			
			hitsForward = Physics.RaycastAll(transform.position, Vector3.forward, WalkSpotDistance);
			hitsBack = Physics.RaycastAll(transform.position, Vector3.back, WalkSpotDistance);
			hitsLeft = Physics.RaycastAll(transform.position, Vector3.left, WalkSpotDistance);
			hitsRight = Physics.RaycastAll(transform.position, Vector3.right, WalkSpotDistance);
			
			int a = 0;
			int b = 0;
			int c = 0;
			int d = 0;
			while (a < hitsForward.Length) {
				RaycastHit hitForward = hitsForward[a];
				Renderer rendForward = hitForward.transform.GetComponent<Renderer>();
				if(rendForward && hitForward.collider.tag == "Available" && _gameCon.AP <= 0){
					newColour.a = 0.0f;
					rendForward.material.color = newColour;
					rendForward.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendForward && (hitForward.collider.tag == "UnAvailable" || hitForward.collider.tag == "Wall")) {
					break;
				}
				if (rendForward && hitForward.collider.tag == "Available" && _gameCon.AP >= _gameCon.WalkMovementCost) {
					hitForward.collider.SendMessage ("MovementMapped");
				}
				a++;
			}
			while (b < hitsBack.Length) {
				RaycastHit hitBack = hitsBack[b];
				Renderer rendBack = hitBack.transform.GetComponent<Renderer>();
				if(rendBack && hitBack.collider.tag == "Available" && _gameCon.AP <= 0){
					newColour.a = 0.0f;
					rendBack.material.color = newColour;
					rendBack.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendBack && (hitBack.collider.tag == "UnAvailable" || hitBack.collider.tag == "Wall")) {
					break;
				}
				if (rendBack && hitBack.collider.tag == "Available") {
					hitBack.collider.SendMessage ("MovementMapped");
				}
				b++;
			}
			while (c < hitsLeft.Length) {
				RaycastHit hitLeft = hitsLeft[c];
				Renderer rendLeft = hitLeft.transform.GetComponent<Renderer>();
				if(rendLeft && hitLeft.collider.tag == "Available" && _gameCon.AP <= 0){
					newColour.a = 0.0f;
					rendLeft.material.color = newColour;
					rendLeft.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendLeft && (hitLeft.collider.tag == "UnAvailable" || hitLeft.collider.tag == "Wall")) {
					break;
				}
				if (rendLeft && hitLeft.collider.tag == "Available") {
					hitLeft.collider.SendMessage ("MovementMapped");
				}
				c++;
			}
			while (d < hitsRight.Length) {
				RaycastHit hitRight = hitsRight[d];
				Renderer rendRight = hitRight.transform.GetComponent<Renderer>();
				if(rendRight && hitRight.collider.tag == "Available" && _gameCon.AP <= 0 && _gameCon.PlayerInteractive == 0){
					newColour.a = 0.0f;
					rendRight.material.color = newColour;
					rendRight.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendRight && (hitRight.collider.tag == "UnAvailable" || hitRight.collider.tag == "Wall")) {
					break;
				}
				if (rendRight && hitRight.collider.tag == "Available") {
					hitRight.collider.SendMessage ("MovementMapped");
				}
				d++;
			}
		}
		
		if(_gameCon.playerRunning == true){
			
			Debug.DrawRay(transform.position, Vector3.left * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.right * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.forward * RunSpotDistance);
			Debug.DrawRay(transform.position, Vector3.back * RunSpotDistance);
			
			
			hitsForward = Physics.RaycastAll(transform.position, Vector3.forward, RunSpotDistance);
			hitsBack = Physics.RaycastAll(transform.position, Vector3.back, RunSpotDistance);
			hitsLeft = Physics.RaycastAll(transform.position, Vector3.left, RunSpotDistance);
			hitsRight = Physics.RaycastAll(transform.position, Vector3.right, RunSpotDistance);
			
			int a = 0;
			int b = 0;
			int c = 0;
			int d = 0;
			while (a < hitsForward.Length) {
				RaycastHit hitForward = hitsForward[a];
				Renderer rendForward = hitForward.transform.GetComponent<Renderer>();
				if(rendForward && hitForward.collider.tag == "Available" && _gameCon.AP <= 0 && _gameCon.PlayerInteractive == 0){
					newColour.a = 0.0f;
					rendForward.material.color = newColour;
					rendForward.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendForward && (hitForward.collider.tag == "UnAvailable" || hitForward.collider.tag == "Wall")) {
					break;
				}
				if (rendForward && hitForward.collider.tag == "Available" && _gameCon.AP >= _gameCon.RunMovementCost) {
					hitForward.collider.SendMessage ("MovementMapped");
				}
				a++;
			}
			while (b < hitsBack.Length) {
				RaycastHit hitBack = hitsBack[b];
				Renderer rendBack = hitBack.transform.GetComponent<Renderer>();
				if(rendBack && hitBack.collider.tag == "Available" && _gameCon.AP <= 0 && _gameCon.PlayerInteractive == 0){
					newColour.a = 0.0f;
					rendBack.material.color = newColour;
					rendBack.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendBack && (hitBack.collider.tag == "UnAvailable" || hitBack.collider.tag == "Wall")) {
					break;
				}
				if (rendBack && hitBack.collider.tag == "Available" && _gameCon.AP >= _gameCon.RunMovementCost) {
					hitBack.collider.SendMessage ("MovementMapped");
				}
				b++;
			}
			while (c < hitsLeft.Length) {
				RaycastHit hitLeft = hitsLeft[c];
				Renderer rendLeft = hitLeft.transform.GetComponent<Renderer>();
				if(rendLeft && hitLeft.collider.tag == "Available" && _gameCon.AP <= 0 && _gameCon.PlayerInteractive == 0){
					newColour.a = 0.0f;
					rendLeft.material.color = newColour;
					rendLeft.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendLeft && (hitLeft.collider.tag == "UnAvailable" || hitLeft.collider.tag == "Wall")) {
					break;
				}
				if (rendLeft && hitLeft.collider.tag == "Available" && _gameCon.AP >= _gameCon.RunMovementCost) {
					hitLeft.collider.SendMessage ("MovementMapped");
				}
				c++;
			}
			while (d < hitsRight.Length) {
				RaycastHit hitRight = hitsRight[d];
				Renderer rendRight = hitRight.transform.GetComponent<Renderer>();
				if(rendRight && hitRight.collider.tag == "Available" && _gameCon.AP <= 0 && _gameCon.PlayerInteractive == 0){
					newColour.a = 0.0f;
					rendRight.material.color = newColour;
					rendRight.material.shader = Shader.Find("Transparent/Diffuse");
				}
				if (rendRight && (hitRight.collider.tag == "UnAvailable" || hitRight.collider.tag == "Wall")) {
					break;
				}
				if (rendRight && hitRight.collider.tag == "Available" && _gameCon.AP >= _gameCon.RunMovementCost) {
					hitRight.collider.SendMessage ("MovementMapped");
				}
				d++;
			}
		}
	}
}