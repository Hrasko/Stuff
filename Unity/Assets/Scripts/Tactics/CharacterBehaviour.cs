using UnityEngine;
using System.Collections;
using Tactics;

public class CharacterBehaviour : MonoBehaviour {
	public Character details;

	// Use this for initialization
	void Start () {
		updateMapPosition ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)) {
			details.actions[0].activate(details);
		}
	}

	public void updateMapPosition()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position + Vector3.up, Vector3.up * (-1000), out hit)) {
			details.mapLocation = hit.collider.GetComponent<TileBehaviour>().Tile;
			Debug.Log("acertando " + hit.collider.name);
		}
	}

	public void setCharacter(Character newC)
	{
		details = newC;
	}


}
