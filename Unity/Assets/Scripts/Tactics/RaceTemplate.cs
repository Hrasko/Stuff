using UnityEngine;
using System.Collections;

namespace Tactics
{
    [System.Serializable]
    public class RaceTemplate : System.Object
    {
        public int id;
        public string name;
        public int minHeight;
        public int maxHeight;
        public int minWeight;
        public int maxWeight;
        public int[] bonusAttributesId;
        public Size size;
        public int baseSpeed;
        public Vision vision;

        //TODO: Colocar poderes

    }
}