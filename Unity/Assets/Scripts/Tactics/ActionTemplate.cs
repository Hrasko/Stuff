namespace Tactics
{
    [System.Serializable]
    public abstract class ActionTemplate : System.Object
    {
        protected ActionType type;
        protected InputSelectionType onStart;
        protected InputSelectionType onMouseOver;
        protected int range = 1;

        public void activate(Tile startLocation)
        {
            InputController.waitForInput(onStart, onMouseOver, range, act, startLocation);
        }

        protected abstract void act(Tile[] selection);

    }
}
