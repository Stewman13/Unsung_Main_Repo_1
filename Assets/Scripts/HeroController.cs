using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

	public GameObject TileUnderHero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		whereAmI();
	}

	void whereAmI(){
		RaycastHit HitDown;

		Ray TileCheckDown = new Ray(transform.position, Vector3.down);
		Debug.DrawRay(transform.position, Vector3.up * 1.0f);

		if(Physics.Raycast(TileCheckDown, out HitDown, 1.0f)){ 
			TileUnderHero = HitDown.transform.gameObject;
			if(TileUnderHero.GetComponent<FloorTile_Controler>().HeroIsOnThisBlock == true){
				//Do Something... can't think straight
			}
		}
	}
}
