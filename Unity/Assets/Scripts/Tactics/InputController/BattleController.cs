using System.Collections.Generic;
using UnityEngine;

namespace Tactics.InputController
{
    public class BattleController : IInputController
    {
        public event InputSelection onStartInput;
        public event InputSelection onMouseOverEnter;
		//public static event InputSelection onSelect;
        static InputCallback callback;
        public int range;

		public override void waitForInput(InputSelectionType onStart_, InputSelectionType onMouseOverEnter_, int range_, InputCallback callback_, Tile startLocation)
        {
            onStartInput = switchSelection(onStart_);
			onMouseOverEnter = switchSelection(onMouseOverEnter_);
            range = range_;
            callback = callback_;
            OnInputStart(startLocation);
        }

        public override void ResetInput()
        {
            onStartInput = DoNothing;
            onMouseOverEnter = DoNothing;
            range = 1;
            callback = null;
        }

        public override InputSelection switchSelection(InputSelectionType type)
        {
            switch (type)
            {
				case InputSelectionType.All: return SelectAll;
                case InputSelectionType.Area: return AreaSelection;
                case InputSelectionType.Single: return SingleTileSelection;
                default: return DoNothing;
            }
        }

		public void SelectAll(Tile tile, int tileStatusIndex)
		{
			for (int i = 0; i < Tile.map.Length; i++) {
				Tile.map[i].setFlag(tileStatusIndex,true);
			}
		}

        // Selection Area
        public void AreaSelection(Tile center, int tileStatusIndex)
        {
            Tile.ResetEspecificMapStatus(tileStatusIndex);
            int minX = center.row - range + 1;
            int maxX = center.row + range - 1;
            if (minX < 0)
            {
                minX = 0;
            }
            if (maxX > Tile.mapSize)
            {
                maxX = Tile.mapSize;
            }

            int minY = center.column - range + 1;
            int maxY = center.column + range - 1;
            if (minY < 0)
            {
                minY = 0;
            }
            if (maxY > Tile.mapSize)
            {
                maxY = Tile.mapSize;
            }

            for (int i = minX; i <= maxX; i++)
            {
                int distX = System.Math.Abs(center.row - i);
                for (int j = minY; j <= maxY; j++)
                {
                    int distY = System.Math.Abs(center.column - j);
                    if (distX + distY < range)
                    {
                        Tile.get(i, j).setFlag(tileStatusIndex,true);
                    }
                }
            }
        }

        public void SingleTileSelection(Tile center, int tileStatusIndex)
        {
            Tile.ResetEspecificMapStatus(tileStatusIndex);
            center.setFlag(tileStatusIndex,true);
        }

        public void DoNothing(Tile center, int tileStatusIndex)
        {

        }

        // Events
        public override void OnInputStart(Tile startLocation)
        {
            onStartInput(startLocation, Tile.INAREA);
        }

        public override void MouseOverEnter(Tile tile)
        {
			Tile.ResetEspecificMapStatus(Tile.SELECTABLE);
            if (tile.status(Tile.INAREA))
            {
                onMouseOverEnter(tile, Tile.SELECTABLE);
            }
        }

		public override void MouseOverStay (Tile tile)
		{

		}

        public override void OnMouseSelect(Tile tile)
        {
			Tile.ResetEspecificMapStatus(Tile.SELECTED);
            Tile.changeStatus(Tile.SELECTABLE, Tile.SELECTED);
        }

        public override void OnMouseConfirm(Tile tile)
        {
            List<Tile> list = Tile.getSelectedTiles();
            Tile.ResetMapStatus();
            callback(list.ToArray());
            ResetInput();
        }

    }
}