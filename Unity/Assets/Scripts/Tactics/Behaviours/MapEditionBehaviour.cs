using UnityEngine;
using System.Collections;
using Tactics;

public class MapEditionBehaviour : MonoBehaviour {


	public MapBehaviour mapBehaviour;
	void OnGUI()
	{
		mapBehaviour.mapName = GUI.TextField(new Rect(10, 50, 200, 30), mapBehaviour.mapName, 25);
		if (GUI.Button(new Rect(10, 10, 100, 30), "load"))
		{
            Tile.all.Clear();
			mapBehaviour.LoadMap();
			GameObject[] allTiles = GameObject.FindGameObjectsWithTag("tile");
			for (int i = 0; i < allTiles.Length-1; i++)
			{
				Debug.Log(i);
				allTiles[i].SendMessage("setTile", Tile.all[i]);
				allTiles[i].SendMessage("UpdateWallStatus");
			}
		}
		if (GUI.Button(new Rect(110, 10, 100, 30), "save"))
		{
			mapBehaviour.SaveMap();
		}
	}
}
