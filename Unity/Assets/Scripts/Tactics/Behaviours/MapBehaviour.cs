using UnityEngine;
using System.Collections;
using Tactics;
using System.Collections.Generic;

public class MapBehaviour : MonoBehaviour {
	public int size;
    public int floors = 1;
	public GameObject tilePrefab;
	public string mapName = "nomemapa";
	public string lastTile = "";

	// Use this for initialization
	void Awake () {
        Tile.totalNodes = size * size * floors;
	}

    public void StartBatlle()
    {
		Debug.Log ("starbattlebegin");
        LoadMap();
        createMap(createBattleTile);
		Debug.Log ("starbattleend");
    }

    public void StartCleanMapEdition()
    {
        startCleanMap();
    }

    private Tile createBattleTile(int index)
    {
        return Tile.all[index];
    }

    private Tile createCleanTile(int index)
    {
        Tile t = new Tile();
        t.setIndex(index);
        return t;
    }

    private int[][] createCleanGraph()
    {
        return Tile.CreateCleanGraph();
    }

    private void startCleanMap()
    {
        createMap(createCleanTile);     
    }

	private void createMap(TileFactory tileFactory)
	{
		
		Debug.Log ("1");
        Tile.mapSize = size;
		Tile.tileSize = tilePrefab.transform.localScale.x;
		Vector3 posicao = new Vector3 (transform.position.x, transform.position.y, transform.position.z);               

		for (int i = 0; i < size; i++) {
            posicao.x = Tile.tileSize * i;

			for (int j = 0; j < size; j++) {
                posicao.z = Tile.tileSize * j;

				int index = i*size+j;

				GameObject go = Instantiate(tilePrefab,posicao,Quaternion.identity) as GameObject;
                go.SendMessage("setTile", tileFactory(index));
				go.name = "Tile"+i+","+j;
				go.SendMessage("UpdateWallStatus");
			}
		}
		
		Debug.Log ("1");
	}

    public void LoadMap()
    {
        Tile.all = Util.Serializer.LoadXMLString("tiles" + mapName, typeof(List<Tile>)) as List<Tile>;
        Debug.Log("loaded tiles");        
    }

    public void SaveMap()
    {
        for (int i = 0; i < Tile.all.Count; i++)
        {
            if (Tile.all[i].walls[Tile.NORTHWALL])
            {
                Tile.all[i].edgeNorth = -1;
            }
            if (Tile.all[i].walls[Tile.EASTWALL])
            {
                Tile.all[i].edgeEast = -1;
            }
            if (Tile.all[i].walls[Tile.SOUTHWALL])
            {
                Tile.all[i].edgeSouth = -1;
            }
            if (Tile.all[i].walls[Tile.WESTWALL])
            {
                Tile.all[i].edgeWest = -1;
            }
        }

        Util.Serializer.SaveXMLString("tiles" + mapName, Tile.all, typeof(List<Tile>));
        Debug.Log("saved tiles");
    }

	void Update()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Tile.layerTile))
        {
			if (lastTile != hit.collider.name){
				hit.collider.SendMessage("enterMouseOver");
				lastTile = hit.collider.name;
			}else{
				hit.collider.SendMessage("stayMouseOver");
			}
			if (Input.GetMouseButtonDown (0)) {
				hit.collider.SendMessage("mouseClick");
			}
		}
	}

}
