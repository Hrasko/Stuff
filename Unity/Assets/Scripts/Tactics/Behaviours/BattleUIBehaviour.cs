using UnityEngine;
using System.Collections;
using Tactics;
public class BattleUIBehaviour : MonoBehaviour {

	public static ActionTemplate[] actions;

	void Awake()
	{
		actions = new ActionTemplate[0];
	}

	public void OnGUI()
	{
		for (int i = 0; i < actions.Length; i++) 
		{
			if (GUI.Button(
				new Rect(10,10+20*i,100,30),
				actions[i].name()))
			{
				actions[i].activate(GM.CurrentPlayer);
			}
		}
	}

}
