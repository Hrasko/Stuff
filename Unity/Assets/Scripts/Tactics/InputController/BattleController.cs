using System.Collections.Generic;
using UnityEngine;

namespace Tactics.InputController
{
    public class BattleController : IInputController
    {
        public event InputSelection onStartInput;
        public event InputSelection onMouseOver;
		//public static event InputSelection onSelect;
        static InputCallback callback;
        public int range;

        public override void waitForInput(InputSelectionType onStart_, InputSelectionType onMouseOver_, int range_, InputCallback callback_, Tile startLocation)
        {
            onStartInput = switchSelection(onStart_);
            onMouseOver = switchSelection(onMouseOver_);
            range = range_;
            callback = callback_;
            OnInputStart(startLocation);
        }

        public override void ResetInput()
        {
            onStartInput = DoNothing;
            onMouseOver = DoNothing;
            range = 1;
            callback = null;
        }

        public override InputSelection switchSelection(InputSelectionType type)
        {
            switch (type)
            {
                case InputSelectionType.Area: return AreaSelection;
                case InputSelectionType.Single: return SingleTileSelection;
                default: return DoNothing;
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
                        Tile.get(i, j).statusFlag[tileStatusIndex] = true;
                    }
                }
            }
        }

        public void SingleTileSelection(Tile center, int tileStatusIndex)
        {
            Tile.ResetEspecificMapStatus(tileStatusIndex);
            center.statusFlag[tileStatusIndex] = true;
        }

        public void DoNothing(Tile center, int tileStatusIndex)
        {

        }

        // Events
        public override void OnInputStart(Tile startLocation)
        {
            onStartInput(startLocation, Tile.INAREA);
        }

        public override void OnMouseOver(Tile tile)
        {
            if (tile.statusFlag[Tile.INAREA])
            {
                onMouseOver(tile, Tile.SELECTABLE);
            }
            else
            {
                Tile.ResetEspecificMapStatus(Tile.SELECTABLE);
            }
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