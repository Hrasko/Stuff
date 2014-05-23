using UnityEngine;
using System.Collections;
using Tactics.InputController;

namespace Tactics
{
    public class GM : MonoBehaviour
    {
        public Atributtes[] initialAttributes;

        IInputController inputController;

        static GM master;

        public static IInputController input { get { return master.inputController; } }


        void Start()
        {            
            master = this;			
        }

        public void StartBatlle()
        {
            inputController = new BattleController();
            inputController.ResetInput();
        }

        public void StartMapEdition()
        {
            inputController = new MapEditController();
            inputController.ResetInput();
            inputController.waitForInput(InputSelectionType.EditionStart, InputSelectionType.None, 0, null, null);
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