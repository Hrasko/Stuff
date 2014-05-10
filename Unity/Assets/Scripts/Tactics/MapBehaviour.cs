using UnityEngine;
using System.Collections;
using Tactics;

public class MapBehaviour : MonoBehaviour {
	public int size;
	public GameObject tilePrefab;
    

	// Use this for initialization
	void Start () {
		createCleanMap ();
	}

	private void createCleanMap()
	{
		Tile.map = new Tile[size*size];
        Tile.graph = new int[size][];
		Tile.mapSize = size;
		float tileSize = tilePrefab.transform.localScale.x;
		Vector3 posicao = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		for (int i = 0; i < size; i++) {
			posicao.x = tileSize*i;
            Tile.graph[i] = new int[size];
			for (int j = 0; j < size; j++) {
				posicao.z = tileSize*j;
				GameObject go = Instantiate(tilePrefab,posicao,Quaternion.identity) as GameObject;
				go.SendMessage("setTile",i*size+j);
				go.name = "Tile"+i+","+j;
				Tile.map[i*size+j] = go.GetComponent<TileBehaviour>().Tile;
                Tile.graph[i][j] = 0;
			}
		}
	}

	private void populateMap()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
