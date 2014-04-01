using UnityEngine;
using System.Collections;

namespace Tactics
{
    [System.Serializable]
    public class Atributtes : System.Object
    {
        public int id;
        public string name;
        public int initValue;
        public int maxValue;

        public float Value(Character character)
        {
            float xp = character.attributesXP[id];
            int value = initValue;
            while (xp > 0)
            {
                int diff = value + 1 - initValue;
                //int xpDiff = (int)(xp - diff);
                if (xp - diff >= 0)
                {
                    xp -= diff;
                    value++;
                }
                else
                {
                    xp = 0;
                }
            }
            return value;
        }

    }
}