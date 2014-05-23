using UnityEngine;
using System.Collections;
using Tactics;

/// <summary>
/// Class used to organize the statup
/// </summary>
public class StartBehaviour : MonoBehaviour {

    public string startGM;
    public string startMap;
	// Use this for initialization
	void Start () {
        gameObject.SendMessage(startGM);
        gameObject.SendMessage(startMap);
	}
}
