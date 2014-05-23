using UnityEngine;
using System.Collections;
using Tactics;

public class TileBehaviour:MonoBehaviour {
	
    public const int NWALL = 0;
    public const int EWALL = 1;
    public const int SWALL = 2;
    public const int WWALL = 3;
    public const int NEWALL = 4;
    public const int SEWALL = 5;
    public const int SWWALL = 6;
    public const int NWWALL = 7;
    
    public Tile _tile;    
    public GameObject[] Walls;

	public Tile Tile
	{
		get { return _tile;}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void UpdateTileGraphics () {
		if (_tile.statusFlag [Tile.SELECTED]) {
				renderer.material.color = Color.red;
		} else if (_tile.statusFlag [Tile.SELECTABLE]) {
				renderer.material.color = Color.yellow;
		} else if (_tile.statusFlag [Tile.INAREA]) {
				renderer.material.color = Color.blue;
		} else {
				renderer.material.color = Color.white;
		}

        
    }

    void UpdateWallStatus()
    {
        if (_tile.walls[Tile.NORTHWALL])
        {
            Walls[NWALL].renderer.enabled = true;
            Walls[NWWALL].renderer.enabled = true;
            Walls[NEWALL].renderer.enabled = true;
        }
        if (_tile.walls[Tile.SOUTHWALL])
        {
            Walls[SWALL].renderer.enabled = true;
            Walls[SWWALL].renderer.enabled = true;
            Walls[SEWALL].renderer.enabled = true;
        }
        if (_tile.walls[Tile.EASTWALL])
        {
            Walls[NEWALL].renderer.enabled = true;
            Walls[EWALL].renderer.enabled = true;
            Walls[SEWALL].renderer.enabled = true;
        }
        if (_tile.walls[Tile.WESTWALL])
        {
            Walls[NWWALL].renderer.enabled = true;
            Walls[WWALL].renderer.enabled = true;
            Walls[SWWALL].renderer.enabled = true;
        }
    }

	
	void OnMouseOver()
	{
		Debug.Log (name);
		GM.input.OnMouseOver(_tile);
	}

	void OnMouseDown()
	{
        GM.input.OnMouseSelect(_tile);
	}

    public void setTile(Tile newTile)
    {
        _tile = newTile;
        _tile.tileStatusChanged += UpdateTileGraphics;
        _tile.wallStatusEditionChanged += UpdateWallStatus;
    }

}
