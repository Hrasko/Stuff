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

		void Awake()
		{
			InputController.ResetInput();
		}

        void Start()
        {
            master = this;			
        }

        /// <summary>
        /// TODO: delete this in final version
        /// </summary>
        public void sampletest1()
        {
            GameObject go =  Instantiate (Resources.Load("CharTest"), transform.position, transform.rotation) as GameObject;
			go.SendMessage ("setCharacter", newCharacter());
			Character character = go.GetComponent<CharacterBehaviour> ().details;
			character.baseSpeed = 6;
			character.actions.Add (new NormalMovement ());
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