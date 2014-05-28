using UnityEngine;

namespace Tactics
{
    [System.Serializable]
    public class Tile : System.Object
    {
        public event voidDelegate tileStatusChanged;
        public event voidDelegate wallStatusEditionChanged;

        public const int INAREA = 0;
        public const int SELECTED = 1;
        public const int SELECTABLE = 2;

        public const int NORTHWALL = 0;
        public const int SOUTHWALL = 1;
        public const int WESTWALL = 2;
        public const int EASTWALL = 3;

		public const int WALLGRAPHCOST = 10000;

        public static Tile[] map;
        public static int[][] graph;
        public static int mapSize;

        /// <summary>
        /// NORTHWALL, SOUTHWALL, WESTWALL, EASTWALL
        /// </summary>
        public bool[] walls;

        public int moveCost = 0;
        public int height = 0;
        
        /// <summary>
        /// Selectable,Selected,InArea
        /// </summary>
        bool[] statusFlag = new bool[3];

        int _row;
        int _column;
		int _index;

        public int row { get { return this._row; } }
        public int column { get { return this._column; } }

        public Tile()
        {
            for (int i = 0; i < statusFlag.Length; i++)
            {
                statusFlag[i] = false;
            }
            walls = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                walls[i] = false;
            }
        }

		public void setFlag(int flag, bool value)
		{
			statusFlag [flag] = value;
			if (tileStatusChanged != null)
			{
				tileStatusChanged();
			}
		}

		public bool status(int flag)
		{
			return statusFlag [flag];
		}

        public void setIndex(int index)
        {
			this._index = index;
            
            this._row = index / mapSize;
            this._column = index % mapSize;

			UpdateGraph (_row, Tile.mapSize, NORTHWALL, index, _row + 1);
			UpdateGraph (-(_row), 0, SOUTHWALL, index, _row - 1);
			UpdateGraph (_column, Tile.mapSize, EASTWALL, index, _column + 1);
			UpdateGraph (-(_column), 0, WESTWALL, index, _column - 1);

        }

		private void UpdateGraph(int rc,int limit, int constWall, int thisNode, int otherNode)
		{
			int value = 1;
            
			if (rc < limit)
			{
                Debug.Log(constWall);
				if (walls [constWall]) {
					value = WALLGRAPHCOST;
				}
				Tile.graph[thisNode][otherNode] = value;
				Tile.graph[otherNode][thisNode] = value;
				
			}
		}

        public void invertWallStatus(int constIndex)
        {
            walls[constIndex] = !walls[constIndex];

			UpdateGraph (_row, Tile.mapSize, NORTHWALL, _index, _row + 1);
			UpdateGraph (-(_row), 0, SOUTHWALL, _index, _row - 1);
			UpdateGraph (_column, Tile.mapSize, EASTWALL, _index, _column + 1);
			UpdateGraph (-(_column), 0, WESTWALL, _index, _column - 1);

            if (wallStatusEditionChanged != null)
            {
                
                wallStatusEditionChanged();
            }
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
                    if (map[i].tileStatusChanged != null)
                    {
                        map[i].tileStatusChanged();
                    }
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
                    if (map[i].tileStatusChanged != null)
                    {
                        map[i].tileStatusChanged();
                    }
                }
            }
        }

        public static void ResetEspecificMapStatus(int index)
        {
            for (int i = 0; i < map.Length; i++)
            {
                map[i].statusFlag[index] = false;
                if (map[i].tileStatusChanged != null)
                {
                    map[i].tileStatusChanged();
                }
            }
        }
    }
}
