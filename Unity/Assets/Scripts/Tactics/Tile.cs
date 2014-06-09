using System.Collections.Generic;
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

        public static List<Tile> all = new List<Tile>();

        public static int mapSize;
        public static int totalNodes;
        public static int layerTile = 1 << 8;
        public static float tileSize;
        /// <summary>
        /// NORTHWALL, SOUTHWALL, WESTWALL, EASTWALL
        /// </summary>
        public bool[] walls;

        public int moveCost = 0;
        public int height = 0;

        public int edgeNorth = -1, edgeSouth = -1, edgeWest = -1, edgeEast = -1;

        /// <summary>
        /// Selectable,Selected,InArea
        /// </summary>
        public bool[] _statusFlag = new bool[3];
        public int _row;
        public int _column;
		public int _index;

        public int row { get { return this._row; } }
        public int column { get { return this._column; } }

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
			Tile.all.Add (this);
			for (int i = 0; i < _statusFlag.Length; i++) {
				_statusFlag [i] = false;
			}
			walls = new bool[4];
			for (int i = 0; i < 4; i++) {
				walls [i] = false;
			}

			this._index = index;
            
            this._row = index / mapSize;
            this._column = index % mapSize;
            
            if (this._column > 0)
            {
				this.edgeWest = _row*mapSize + _column - 1;
            }
            if (this._row < mapSize-1)
            {
				this.edgeSouth = (_row + 1)*mapSize + _column;
            }
            if (this._column < mapSize - 1)
            {
				this.edgeEast = _row * mapSize + _column + 1;
            }
            if (this._row > 0)
            {
				this.edgeNorth = (_row - 1)*mapSize + _column;
            }
            
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
					if (i == k){
						graph[i][k] = 0;
					}else{
                    	graph[i][k] = Tile.WALLGRAPHCOST;
					}
                }
            }
            return graph;
        }

        public static int graphCost(int from, int to)
        {
			if (Tile.all[from].edgeNorth >=0 && Tile.all[Tile.all[from].edgeNorth] == Tile.all[from])
            {
                return 1;
            }
            else
				if (Tile.all[from].edgeEast >=0 && Tile.all[Tile.all[from].edgeEast] == Tile.all[from])
                {
                    return 1;
                }
                else
				if (Tile.all[from].edgeSouth >=0 && Tile.all[Tile.all[from].edgeSouth] == Tile.all[from])
                    {
                        return 1;
                    }
                    else
				if (Tile.all[from].edgeWest >=0 && Tile.all[Tile.all[from].edgeWest] == Tile.all[from])
                        {
                            return 1;
                        }
            return WALLGRAPHCOST;
        }

        /*
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
            UpdateGraphCosts(_row, Tile.mapSize-1, NORTHWALL, _index, _row + 1, _column);
            UpdateGraphCosts(-(_row), 0, SOUTHWALL, _index, _row - 1,_column);
            UpdateGraphCosts(_column, Tile.mapSize-1, EASTWALL, _index, _row, _column + 1);
            UpdateGraphCosts(-(_column), 0, WESTWALL, _index, _row, _column - 1);

        }

        private void UpdateGraphCosts(int rc, int limit, int constWall, int thisNode, int otherX, int otherY)
		{
			int value = 1;
            
			if (rc < limit)
			{
				int otherNode = Tile.get(otherX,otherY)._index;
				if (walls [constWall]) {
					value = WALLGRAPHCOST;
				}
				Tile.graph[thisNode][otherNode] = value;
				Tile.graph[otherNode][thisNode] = value;
			}
		}
         * */

        public void invertWallStatus(int constIndex)
        {
            walls[constIndex] = !walls[constIndex];

            if (wallStatusEditionChanged != null)
            {
                wallStatusEditionChanged();
            }
        }

        public static Tile get(int index_)
        {
            return all[index_];
        }

        public static Tile getNorth(int index_)
        {
			if (all [index_].edgeNorth >= 0) {
								return Tile.all [all [index_].edgeNorth];
						}
			return null;
        }

        public static Tile getEast(int index_)
        {
			if (all [index_].edgeEast >= 0) {
				return Tile.all [all [index_].edgeEast];
			}
			return null;
        }

        public static Tile getSouth(int index_)
        {
			if (all [index_].edgeSouth >= 0) {
				return Tile.all [all [index_].edgeSouth];
			}
			return null;
        }

        public static Tile getWest(int index_)
        {
			if (all [index_].edgeWest >= 0) {
				return Tile.all [all [index_].edgeWest];
			}
			return null;
        }

        public static void changeStatus(int previousFlag, int newFlag)
        {
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i]._statusFlag[previousFlag])
                {
                    all[i]._statusFlag[previousFlag] = false;
                    all[i]._statusFlag[newFlag] = true;
                    if (all[i].tileStatusChanged != null)
                    {
                        all[i].tileStatusChanged();
                    }
                }
            }
        }

        public static System.Collections.Generic.List<Tile> getSelectedTiles()
        {
            System.Collections.Generic.List<Tile> list = new System.Collections.Generic.List<Tile>();
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i]._statusFlag[SELECTED])
                {
                    list.Add(all[i]);
                }
            }
            return list;
        }

        public static void ResetMapStatus()
        {
            for (int i = 0; i < all.Count; i++)
            {
                for (int j = 0; j < all[i]._statusFlag.Length; j++)
                {
                    all[i]._statusFlag[j] = false;
                    if (all[i].tileStatusChanged != null)
                    {
                        all[i].tileStatusChanged();
                    }
                }
            }
        }

        public static void ResetEspecificMapStatus(int index)
        {
            for (int i = 0; i < all.Count; i++)
            {
                all[i]._statusFlag[index] = false;
                if (all[i].tileStatusChanged != null)
                {
                    all[i].tileStatusChanged();
                }
            }
        }
    }
}
