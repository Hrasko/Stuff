namespace Tactics
{
    [System.Serializable]
    public class Tile : System.Object
    {
        public const int INAREA = 0;
        public const int SELECTED = 1;
        public const int SELECTABLE = 2;

        public static Tile[] map;
        public static int[][] graph;
        public static int mapSize;

        public int moveCost = 0;
        public int height = 0;
        
        /// <summary>
        /// Selectable,Selected,InArea
        /// </summary>
        public bool[] statusFlag = new bool[3];

        int _x;
        int _y;

        public int x { get { return this._x; } }
        public int y { get { return this._y; } }

        public Tile(int index)
        {
            for (int i = 0; i < statusFlag.Length; i++)
            {
                statusFlag[i] = false;
            }
            this._x = index / mapSize;
            this._y = index % mapSize;
        }

        public static Tile get(int row, int column)
        {
            return map[row * mapSize + column];
        }

        public static void changeStatus(int previousFlag, int newFlag)
        {
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i].statusFlag[previousFlag])
                {
                    map[i].statusFlag[previousFlag] = false;
                    map[i].statusFlag[newFlag] = true;
                }
            }
        }

        public static System.Collections.Generic.List<Tile> getSelectedTiles()
        {
            System.Collections.Generic.List<Tile> list = new System.Collections.Generic.List<Tile>();
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i].statusFlag[SELECTED])
                {
                    list.Add(map[i]);
                }
            }
            return list;
        }

        public static void ResetMapStatus()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].statusFlag.Length; j++)
                {
                    map[i].statusFlag[j] = false;
                }
            }
        }

        public static void ResetEspecificMapStatus(int index)
        {
            for (int i = 0; i < map.Length; i++)
            {
                map[i].statusFlag[index] = false;                
            }
        }
    }
}
