using UnityEngine;
using System.Collections.Generic;

namespace Tactics
{
    [System.Serializable]
    public class Character : System.Object
    {
        public const string SPEEDMOD = "speed";

        public float[] attributesXP;
        public RaceTemplate father, mother;
        public int Height;
        public int Weight;
        public int baseSpeed;
        public Tile mapLocation;
        Dictionary<string, List<Modifier>> modifiers = new Dictionary<string, List<Modifier>>();

		public List<ActionTemplate> actions = new List<ActionTemplate>();

        public int Speed
        {
            get
            {
                int total = baseSpeed;
                List<Modifier> lista = modifiers[SPEEDMOD];
                for (int i = 0; i < lista.Count; i++)
                {
                    total += lista[i].Value;                    
                }
                return total;
            }
        }

        public Character()
        {
            modifiers.Add(SPEEDMOD, new List<Modifier>());
        }

    }
}