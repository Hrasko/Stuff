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
                if (tile.row > 0)
                {
                    Tile.get(tile.row - 1, tile.column).invertWallStatus(Tile.SOUTHWALL);
                }
            }
			else if (Input.GetKeyDown(KeyCode.S))
			{
				tile.invertWallStatus(Tile.SOUTHWALL);
				if (tile.row < Tile.mapSize)
				{
					Tile.get(tile.row + 1, tile.column).invertWallStatus(Tile.NORTHWALL);
				}
			}
			else if (Input.GetKeyDown(KeyCode.D))
			{
				tile.invertWallStatus(Tile.EASTWALL);
				if (tile.column < Tile.mapSize-1)
				{
					Tile.get(tile.row, tile.column + 1).invertWallStatus(Tile.WESTWALL);
				}
			}
			else if (Input.GetKeyDown(KeyCode.A))
			{
				tile.invertWallStatus(Tile.WESTWALL);
				if (tile.column > 0)
				{
					Tile.get(tile.row, tile.column - 1).invertWallStatus(Tile.EASTWALL);
				}
			}
        }
    }
}
