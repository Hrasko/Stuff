using UnityEngine;

namespace Tactics.InputController
{
    public class MapEditController : BattleController
    {
		public override void MouseOverStay (Tile tile)
		{
			if (tile.status(Tile.SELECTABLE))
            if (Input.GetKeyDown(KeyCode.Plus))
            {
                tile.height += 1;
            }
            else if (Input.GetKeyDown(KeyCode.Minus))
            {
                tile.height += 1;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                tile.invertWallStatus(Tile.NORTHWALL);
                Tile edge = Tile.getNorth(tile._index);
                if (edge != null)
                {
                    edge.invertWallStatus(Tile.SOUTHWALL);
                }
            }
			else if (Input.GetKeyDown(KeyCode.S))
			{
				tile.invertWallStatus(Tile.SOUTHWALL);
                Tile edge = Tile.getSouth(tile._index);
                if (edge != null)
                {
                    edge.invertWallStatus(Tile.NORTHWALL);
                }
			}
			else if (Input.GetKeyDown(KeyCode.D))
			{
				tile.invertWallStatus(Tile.EASTWALL);
                Tile edge = Tile.getEast(tile._index);
                if (edge != null)
                {
                    edge.invertWallStatus(Tile.WESTWALL);
                }
			}
			else if (Input.GetKeyDown(KeyCode.A))
			{
				tile.invertWallStatus(Tile.WESTWALL);
                Tile edge = Tile.getWest(tile._index);
                if (edge != null)
                {
                    edge.invertWallStatus(Tile.EASTWALL);
                }
			}
        }
    }
}
