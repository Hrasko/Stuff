using UnityEngine;
using System.Collections;

public delegate void EventHandler();

public enum SizeTactics
{
    Small,
    Medium,
    Large
}

public enum VisionTactics
{
    Normal,
    LowLight,
    DarkVision
}

public class GMTactics : MonoBehaviour {

	public static event EventHandler _show;
    public AtributtesTactics[] initialAttributes;

    static GMTactics master;

	// Use this for initialization
	void Start () {
        master = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static CharacterTactics newCharacter()
	{
        CharacterTactics newC = new CharacterTactics();
        newC.attributesXP = new float[master.initialAttributes.Length];

        for (int i = 0; i < master.initialAttributes.Length; i++)
        {
            newC.attributesXP[i] = 0;
        }

		return newC;
	}

}
