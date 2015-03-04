using UnityEngine;
using System.Collections;

public class TileClickHandler : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TileMap map;

	void OnMouseUp() {
		Debug.Log("click");

		map.MoveSelectedUnitTo(tileX, tileY);
	}
}
