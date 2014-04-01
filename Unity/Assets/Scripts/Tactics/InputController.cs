using System.Collections.Generic;

namespace Tactics
{
    [System.Serializable]
    public class InputController : System.Object
    {
        public static event InputSelection onStartInput;
        public static event InputSelection onMouseOver;
        static InputCallback callback;
        public static int range;

        public static void waitForInput(InputSelectionType onStart_, InputSelectionType onMouseOver_, int range_, InputCallback callback_, Tile startLocation)
        {
            onStartInput = switchSelection(onStart_);
            onMouseOver = switchSelection(onMouseOver_);
            range = range_;
            callback = callback_;
            OnInputStart(startLocation);
        }

        public static void ResetInput()
        {
            onStartInput = DoNothing;
            onMouseOver = DoNothing;
            range = 1;
            callback = null;
        }

        public static InputSelection switchSelection(InputSelectionType type)
        {
            switch (type)
            {
                case InputSelectionType.Area: return AreaSelection;
                case InputSelectionType.Single: return SingleTileSelection;
                default: return DoNothing;
            }
        }

        public static void AreaSelection(Tile center, int tileStatusIndex)
        {
            Tile.ResetEspecificMapStatus(tileStatusIndex);
            int minX = center.x - range + 1;
            int maxX = center.x + range - 1;
            if (minX < 0)
            {
                minX = 0;
            }
            if (maxX > Tile.mapSize)
            {
                maxX = Tile.mapSize;
            }

            int minY = center.y - range + 1;
            int maxY = center.y + range - 1;
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
                int distX = System.Math.Abs(center.x - i);
                for (int j = minY; j <= maxY; j++)
                {
                    int distY = System.Math.Abs(center.y - j);
                    if (distX + distY < range)
                    {
                        Tile.get(i, j).statusFlag[tileStatusIndex] = true;
                    }
                }
            }
        }

        public static void SingleTileSelection(Tile center, int tileStatusIndex)
        {
            Tile.ResetEspecificMapStatus(tileStatusIndex);
            center.statusFlag[tileStatusIndex] = true;
        }

        public static void DoNothing(Tile center, int tileStatusIndex)
        {

        }

        public static void OnInputStart(Tile startLocation)
        {
            onStartInput(startLocation, Tile.INAREA);
        }

        public static void OnMouseOver(Tile tile)
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

        public static void OnMouseSelect(Tile tile)
        {
            Tile.changeStatus(Tile.SELECTABLE, Tile.SELECTED);
        }

        public static void OnMouseConfirm(Tile tile)
        {
            List<Tile> list = Tile.getSelectedTiles();
            Tile.ResetMapStatus();
            callback(list.ToArray());
            ResetInput();
        }

    }
}