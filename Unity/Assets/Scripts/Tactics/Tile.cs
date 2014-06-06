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
        public static int totalNodes;
        /// <summary>
        /// NORTHWALL, SOUTHWALL, WESTWALL, EASTWALL
        /// </summary>
        public bool[] walls;

        public int moveCost = 0;
        public int height = 0;
        
        /// <summary>
        /// Selectable,Selected,InArea
        /// </summary>
        public bool[] _statusFlag = new bool[3];
        
        public int _row;
        public int _column;
		public int _index;

        public int row { get { return this._row; } }
        public int column { get { return this._column; } }

        public Tile()
        {
            for (int i = 0; i < _statusFlag.Length; i++)
            {
                _statusFlag[i] = false;
            }
            walls = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                walls[i] = false;
            }
        }

		public void setFlag(int flag, bool value)
		{
			_statusFlag [flag] = value;
			if (tileStatusChanged != null)
			{
				tileStatusChanged();
			}
		}

		public bool status(int flag)
		{
			return _statusFlag [flag];
		}

        public void setIndex(int index)
        {
			this._index = index;
            
            this._row = index / mapSize;
            this._column = index % mapSize;
        }

        public static int[][] CreateCleanGraph()
        {
            int[][] graph = new int[totalNodes][];
            // Start graph
            for (int i = 0; i < totalNodes; i++)
            {
                graph[i] = new int[totalNodes];
                for (int k = 0; k < totalNodes; k++)
                {
                    graph[i][k] = Tile.WALLGRAPHCOST;
                }
            }
            return graph;
        }

        public static void UpdateWholeGraph()
        {
            graph = CreateCleanGraph();
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] != null)
                {
                    map[i].UpdateGraphCosts();
                }
            }
        }

        private void UpdateGraphCosts()
        {
            UpdateGraphCosts(_row, Tile.mapSize, NORTHWALL, _index, _row + 1);
            UpdateGraphCosts(-(_row), 0, SOUTHWALL, _index, _row - 1);
            UpdateGraphCosts(_column, Tile.mapSize, EASTWALL, _index, _column + 1);
            UpdateGraphCosts(-(_column), 0, WESTWALL, _index, _column - 1);
        }

        private void UpdateGraphCosts(int rc, int limit, int constWall, int thisNode, int otherNode)
		{
			int value = 1;
            
			if (rc < limit)
			{
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
                if (map[i]._statusFlag[previousFlag])
                {
                    map[i]._statusFlag[previousFlag] = false;
                    map[i]._statusFlag[newFlag] = true;
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
                if (map[i]._statusFlag[SELECTED])
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
                for (int j = 0; j < map[i]._statusFlag.Length; j++)
                {
                    map[i]._statusFlag[j] = false;
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
                map[i]._statusFlag[index] = false;
                if (map[i].tileStatusChanged != null)
                {
                    map[i].tileStatusChanged();
                }
            }
        }
    }
}
