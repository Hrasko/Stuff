using UnityEngine;
using System.Collections;

[System.Serializable]
public class RaceTemplateTactics : System.Object
{
    public int id;
    public string name;
    public int minHeight;
    public int maxHeight;
    public int minWeight;
    public int maxWeight;
    public int[] bonusAttributesId;
    public SizeTactics size;
    public int baseSpeed;
    public VisionTactics vision;

    //TODO: Colocar poderes

}
