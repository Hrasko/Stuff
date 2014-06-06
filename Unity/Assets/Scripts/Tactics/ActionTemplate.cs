namespace Tactics
{
    [System.Serializable]
    public abstract class ActionTemplate : System.Object
    {
        protected ActionType type;
        protected InputSelectionType onStart;
        protected InputSelectionType onMouseOver;

        public void activate(Character actor)
        {
            GM.input.waitForInput(onStart, onMouseOver, getRange(actor), act, actor.mapLocation);
        }

        protected abstract void act(Tile[] selection);
        protected abstract int getRange(Character actor);
		public abstract string name();
    }
}
