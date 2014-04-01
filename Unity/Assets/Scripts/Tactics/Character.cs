using UnityEngine;
using System.Collections;

namespace Tactics
{
    [System.Serializable]
    public class Character : System.Object
    {

        public float[] attributesXP;
        public RaceTemplate father, mother;
        public int Height;
        public int Weight;
        public int baseSpeed;
        public Tile mapLocation;
    }
}