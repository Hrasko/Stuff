using UnityEngine;
using System.Collections;
using Tactics;

public class MapBehaviour : MonoBehaviour {
	public int size;
	public GameObject tilePrefab;
	public int layerTile = 1 << 8;
    int totalNodes;

	// Use this for initialization
	void Awake () {
        totalNodes = size * size;
	}

    public void StartBatlle()
    {
        LoadMap();
        createMap(createBattleTile);
    }

    public void StartCleanMapEdition()
    {
        startCleanMap();
    }

    private Tile createBattleTile(int index)
    {
        return Tile.map[index];
    }

    private Tile createCleanTile(int index)
    {
        Tile t = new Tile();
        t.setIndex(index);
        return t;
    }

    private int[][] createCleanGraph()
    {
        int[][] graph  = new int[totalNodes][];
        // Start graph
        for (int i = 0; i < totalNodes; i++)
        {
            graph[i] = new int[totalNodes];
            for (int k = 0; k < totalNodes; k++)
            {
                graph[i][k] = Tile.WALLGRAPHCOST;
            }
        }
        return graph;
    }

    private void startCleanMap()
    {
        Tile.graph = createCleanGraph();
        createMap(createCleanTile);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject go = GameObject.Find("Tile" + i + "," + j);
                Tile.map[i * size + j] = go.GetComponent<TileBehaviour>().Tile;
            }
        }        
    }

	private void createMap(TileFactory tileFactory)
	{
		Tile.map = new Tile[totalNodes];
        Tile.mapSize = size;
		float tileSize = tilePrefab.transform.localScale.x;
		Vector3 posicao = new Vector3 (transform.position.x, transform.position.y, transform.position.z);               

		for (int i = 0; i < size; i++) {
			posicao.x = tileSize*i;


			for (int j = 0; j < size; j++) {
				posicao.z = tileSize*j;

				int index = i*size+j;
				

				GameObject go = Instantiate(tilePrefab,posicao,Quaternion.identity) as GameObject;
                go.SendMessage("setTile", tileFactory(index));
				go.name = "Tile"+i+","+j;				
			}
		}
	}

    public void LoadMap()
    {
        Tile.graph = Util.Serializer.LoadXMLString("graph",typeof(int[][])) as int[][];
        Debug.Log("loaded graph");
        Tile.map = Util.Serializer.LoadXMLString("tiles", typeof(Tile[])) as Tile[];
        Debug.Log("loaded tiles");
    }

    public void SaveMap()
    {
        Util.Serializer.SaveXMLString("graph", Tile.graph, typeof(int[][]));
        Debug.Log("saved graph");
        Util.Serializer.SaveXMLString("tiles", Tile.map, typeof(Tile[]));
        Debug.Log("saved tiles");
    }

	public string lastTile = "";
	void Update()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast(ray,out hit,layerTile)){
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

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "save"))
        {
            SaveMap();
        }
    }

}
