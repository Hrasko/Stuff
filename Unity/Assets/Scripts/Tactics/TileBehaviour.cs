using UnityEngine;
using System.Collections;
using Tactics;

public class TileBehaviour:MonoBehaviour {
	Tile tile;

	public Tile Tile
	{
		get { return tile;}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (tile.statusFlag [Tile.SELECTED]) {
				renderer.material.color = Color.red;
		} else if (tile.statusFlag [Tile.SELECTABLE]) {
				renderer.material.color = Color.yellow;
		} else if (tile.statusFlag [Tile.INAREA]) {
				renderer.material.color = Color.blue;
		} else {
				renderer.material.color = Color.white;
		}
}

	
	void OnMouseOver()
	{
		InputController.OnMouseOver(tile);
	}

	void OnMouseDown()
	{
		InputController.OnMouseSelect(tile);
	}

	public void setTile(int index)
	{
		tile = new Tile (index);
	}

}
