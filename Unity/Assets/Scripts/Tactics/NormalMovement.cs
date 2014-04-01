namespace Tactics
{
    public class NormalMovement : ActionTemplate
    {
        public NormalMovement(): base()
        {
            type = ActionType.movement;
            onStart = InputSelectionType.Area;
            onMouseOver = InputSelectionType.Single;
        }

        protected override void act(Tile[] selection)
        {
            throw new System.NotImplementedException();
        }
    }
}