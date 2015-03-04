using UnityEngine;
using System.Collections.Generic;

public class TileMap : MonoBehaviour {

	public GameObject selectedUnit;

	public TileType[] tileTypes;

	int[,] tiles;
	Node[,] graph;

	public int mapSizeX = 10;
	public int mapSizeY = 10;

	void Start(){
		//** This is all for Temporary Map Generation, and will be replaced later! **
		GenerateMapData();
		GeneratePathFindingGraph();
		GenerateMapVisual();
	}

	void GenerateMapData(){

		//Allocates mape tiles **
		tiles = new int[mapSizeX,mapSizeY];

		int x,y;

		//Initialize the map tiles to be ground
		for(x=0; x < mapSizeX; x++) {
			for(y=0; y < mapSizeX; y++) {
				tiles[x,y] = 0;
			}
		}

		//Make a big swamp area
		for(x=3; x <= 5; x++){
			for(y=0; y < 4; y++){
				tiles[x,y] =1;
			}
		}

		//Generating a U-shaped wall for testing. **
		
		tiles[4,4] = 2;
		tiles[5,4] = 2;
		tiles[6,4] = 2;
		tiles[7,4] = 2;
		tiles[8,4] = 2;
		
		tiles[4,5] = 2;
		tiles[4,6] = 2;
		tiles[8,5] = 2;
		tiles[8,6] = 2;
		//Now the map data is given, The visuals need to be spawned as prefabs **
	}

	class Node{
		public List<Node> neighbours;

		public Node(){
			neighbours = new List<Node>();
		}
	}

	void GeneratePathFindingGraph(){
		graph = new Node[mapSizeX,mapSizeY];

		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {

				//There is a four way connected map
				//This also works 6, and 8 way tiles

			//	if(x > 0)
			//		graph[x,y].neighbours.Add(graph[x-1,y]);
			//	if(x < mapSizeX-1)
			//		graph[x,y].neighbours.Add(graph[x+1,y]);
			//	if(y > 0)
			//		graph[x,y].neighbours.Add(graph[x,y-1]);
			//	if(y < mapSizeY-1)
			//		graph[x,y].neighbours.Add(graph[x,y+1]);
			}
		}
	}

	// Turning Data into Visuals **
	void GenerateMapVisual(){
		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {
				TileType tt = tileTypes[ tiles[x,y] ];
				GameObject go = (GameObject)Instantiate( tt.tileVisualPrefab, new Vector3(x,y, 0), Quaternion.identity);
		
				TileClickHandler ct = go.GetComponent<TileClickHandler>();
				ct.tileX = x;
				ct.tileY = y;
				ct.map = this;
			}
		}
	}
	//tells wolrd, tiles location.
	public Vector3 TileCoordToWorldCoord(int x, int y){
		return new Vector3(x,y,0);
	}
	//moves unit to tiles location.
	public void MoveSelectedUnitTo(int x, int y){
		selectedUnit.GetComponent<Unit>().tileX = x;
		selectedUnit.GetComponent<Unit>().tileY = y;
		selectedUnit.transform.position = TileCoordToWorldCoord(x,y);

	
	
	
	}
}
