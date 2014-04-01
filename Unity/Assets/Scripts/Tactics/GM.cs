using UnityEngine;
using System.Collections;

namespace Tactics
{
    public delegate void EventHandler();

    public class GM : MonoBehaviour
    {
        public static event EventHandler _show;

        public Atributtes[] initialAttributes;
        
        static GM master;

        void Start()
        {
            master = this;
        }

        public static void action()
        {

        }

        public static Character newCharacter()
        {
            Character newC = new Character();
            newC.attributesXP = new float[master.initialAttributes.Length];

            for (int i = 0; i < master.initialAttributes.Length; i++)
            {
                newC.attributesXP[i] = 0;
            }

            return newC;
        }

    }
}