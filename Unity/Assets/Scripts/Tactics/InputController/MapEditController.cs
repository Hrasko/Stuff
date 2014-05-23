using UnityEngine;

namespace Tactics.InputController
{
    public class MapEditController : BattleController
    {
        public override InputSelection switchSelection(InputSelectionType type)
        {
			switch (type) 
			{
			case InputSelectionType.EditionStart: return (SelectAll);
			default: return (MapEdition);
			}
            
        }

		public void SelectAll(Tile tile, int tileStatusIndex)
		{
			for (int i = 0; i < Tile.map.Length; i++) {
				Tile.map[i].statusFlag[Tile.INAREA] = true;
			}
		}

        public void MapEdition(Tile tile, int tileStatusIndex)
        {
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
				if (tile.column < Tile.mapSize)
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
