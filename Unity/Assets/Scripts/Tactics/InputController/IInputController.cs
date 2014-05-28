using System;

namespace Tactics.InputController
{
    public abstract class IInputController
    {
        public abstract  void waitForInput(InputSelectionType onStart_, InputSelectionType onMouseOver_, int range_, InputCallback callback_, Tile startLocation);

        public abstract void ResetInput();

        public abstract InputSelection switchSelection(InputSelectionType type);

        public abstract void OnInputStart(Tile startLocation);

		public abstract void MouseOverEnter(Tile tile);

		public abstract void MouseOverStay(Tile tile);

        public abstract void OnMouseSelect(Tile tile);

        public abstract void OnMouseConfirm(Tile tile);
    }
}
