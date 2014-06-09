using UnityEngine;
using System.Collections.Generic;
using Tactics.InputController;

namespace Tactics
{
    public class GM : MonoBehaviour
    {
        public Atributtes[] initialAttributes;

        IInputController inputController;

        static GM master;

        public static IInputController input { get { return master.inputController; } }
		public static Character CurrentPlayer;

		Character[] characters;
		int currentPlayerIndex = 0;


        public void StartGMBatlle()
        {
			Debug.Log ("stargmbattlebegin");
			master = this;
            inputController = new BattleController();
            inputController.ResetInput();
			Debug.Log ("stargmbattleend");
        }

        public void StartGMMapEdition()
        {
			master = this;
            inputController = new MapEditController();
            inputController.ResetInput();
            inputController.waitForInput(InputSelectionType.All, InputSelectionType.Single, 0, null, null);
        }

        /// <summary>
        /// TODO: delete this in final version
        /// </summary>
        public void sampletest1()
        {
			Debug.Log ("stbegin");
            GameObject go =  Instantiate (Resources.Load("CharTest"), new Vector3(50,0,30), transform.rotation) as GameObject;
			go.SendMessage ("setCharacter", newCharacter());
			Character character = go.GetComponent<CharacterBehaviour> ().details;
			character.baseSpeed = 6;
			character.actions.Add (new NormalMovement ());
			characters = new Character[1];
			characters[0] = character;
			Beginturn ();
			Debug.Log ("stend");
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

		public static void Beginturn()
		{
			master.inputController.ResetInput();
			CurrentPlayer = master.characters [master.currentPlayerIndex];
			BattleUIBehaviour.actions = CurrentPlayer.actions.ToArray();
		}
        
		public static void Endturn()
		{
			BattleUIBehaviour.actions = null;
			master.currentPlayerIndex ++;
			if (master.currentPlayerIndex >= master.characters.Length) 
			{
				master.currentPlayerIndex = 0;
			}

		}

    }
}