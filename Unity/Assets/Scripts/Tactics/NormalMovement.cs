namespace Tactics
{
    public class NormalMovement : ActionTemplate
    {
        public NormalMovement(): base()
        {
            type = ActionType.movement;
            onStart = InputSelectionType.DijkstraPreparation;
            onMouseOver = InputSelectionType.DijkstraPath;
        }

        protected override void act(Tile[] selection)
        {
            throw new System.NotImplementedException();
        }

        protected override int getRange(Character actor)
        {
            return actor.Speed;
        }

		public override string name ()
		{
			return "Movement";
		}
    }
}