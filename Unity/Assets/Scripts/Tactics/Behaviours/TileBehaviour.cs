using UnityEngine;
using System.Collections.Generic;
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
		if (_tile.status(Tile.SELECTED)) {
				renderer.material.color = Color.red;
		} else if (_tile.status(Tile.SELECTABLE)) {
				renderer.material.color = Color.yellow;
		} else if (_tile.status(Tile.INAREA)) {
				renderer.material.color = Color.blue;
		} else {
				renderer.material.color = Color.white;
		}

        
    }

    void UpdateWallStatus()
    {
		Walls[NWALL].renderer.enabled = _tile.walls[Tile.NORTHWALL];
		Walls[EWALL].renderer.enabled = _tile.walls[Tile.EASTWALL];
		Walls[SWALL].renderer.enabled = _tile.walls[Tile.SOUTHWALL];
		Walls[WWALL].renderer.enabled = _tile.walls[Tile.WESTWALL];

		Walls[NWWALL].renderer.enabled = _tile.walls[Tile.NORTHWALL] || _tile.walls[Tile.WESTWALL];
		Walls[SWWALL].renderer.enabled = _tile.walls[Tile.SOUTHWALL] || _tile.walls[Tile.WESTWALL];
		Walls[SEWALL].renderer.enabled = _tile.walls[Tile.SOUTHWALL] || _tile.walls[Tile.EASTWALL];
		Walls[NEWALL].renderer.enabled = _tile.walls[Tile.NORTHWALL] || _tile.walls[Tile.EASTWALL];
    }

	
	void enterMouseOver()
	{
		GM.input.MouseOverEnter(_tile);
	}

	void stayMouseOver()
	{
		GM.input.MouseOverStay(_tile);
	}

	void mouseClick()
	{
		if (_tile.status (Tile.SELECTED)) {
			GM.input.OnMouseConfirm (_tile);
		}else if (_tile.status (Tile.SELECTABLE)) {
			GM.input.OnMouseSelect (_tile);
		}        
	}

    public void setTile(Tile newTile)
    {
        _tile = newTile;
        _tile.tileStatusChanged += UpdateTileGraphics;
        _tile.wallStatusEditionChanged += UpdateWallStatus;
    }

}
