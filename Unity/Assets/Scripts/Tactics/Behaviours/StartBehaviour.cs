using UnityEngine;
using System.Collections;
using Tactics;

/// <summary>
/// Class used to organize the statup
/// </summary>
public class StartBehaviour : MonoBehaviour {

	public string[] startFunctions;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < startFunctions.Length; i++) {
			gameObject.SendMessage(startFunctions[i]);	
		}
	}
}
