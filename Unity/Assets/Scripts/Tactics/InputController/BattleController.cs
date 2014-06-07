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
			Tile.ResetMapStatus ();
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
				case InputSelectionType.DijkstraPreparation: return DijstraPreparation;
				case InputSelectionType.DijkstraPath: return DijstraPath;
                default: return DoNothing;
            }
        }

		public void SelectAll(Tile tile, int tileStatusIndex)
		{
			for (int i = 0; i < Tile.map.Length; i++) {
				Tile.map[i].setFlag(tileStatusIndex,true);
			}
		}

#region Selection Area
		public int[] dijkstraIndexes;
		public int[] dijkstraBefore;

		public void DijstraPreparation(Tile center, int tileStatusIndex)
		{
			Tile.ResetEspecificMapStatus(tileStatusIndex);
			// we have to take only the subgraph 
			int minX = center.row - range + 1;
			int maxX = center.row + range - 1;
			if (minX < 0)
			{
				minX = 0;
			}
			if (maxX >= Tile.mapSize)
			{
				maxX = Tile.mapSize - 1;
			}
			
			int minY = center.column - range + 1;
			int maxY = center.column + range - 1;

			if (minY < 0)
			{
				minY = 0;
			}
			if (maxY >= Tile.mapSize)
			{
				maxY = Tile.mapSize - 1;
			}

			List<int> listIndexes = new List<int> ();

			for (int i = minX; i <= maxX; i++)
			{
				int distX = System.Math.Abs(center.row - i);
				for (int j = minY; j <= maxY; j++)
				{
					int distY = System.Math.Abs(center.column - j);
					if (distX + distY < range)
					{
						Debug.Log(listIndexes.Count + ":" + i + ":" + j + ":"+Tile.get(i, j)._index);
						listIndexes.Add(Tile.get(i, j)._index);
					}
				}
			}
			dijkstraIndexes = listIndexes.ToArray ();
			int totalDijkstraNodes = dijkstraIndexes.Length;
			// Here begins Dijkstra
			int thisNodeIndex = -1;

			int[] dijkstraDistance = new int[totalDijkstraNodes];
			int[] costs = new int[totalDijkstraNodes];
			int[] state = new int[totalDijkstraNodes]; // 0 not visited, 1 open, 2 visited

			for (int i = 0; i < totalDijkstraNodes; i++) {
				if (dijkstraIndexes[i] == center._index)
				{
					state[i] = 2;
					thisNodeIndex = i;
					costs[i] = 0;
					dijkstraDistance[i] = 0;
				}
				else
				{
					state[i] = 0;

					dijkstraDistance[i] = 1;
				}
			}
			// Initiate the before vector
			dijkstraBefore = new int[totalDijkstraNodes];
			for (int i = 0; i < totalDijkstraNodes; i++) {
				dijkstraBefore[i] = thisNodeIndex;
			}

			//main loop
			for (int k = 0; k < totalDijkstraNodes; k++)
			{
				state[thisNodeIndex] = 2;
				for (int i = 0; i < totalDijkstraNodes; i++) {
					if (state[i] < 2) // not visited
					{
						int thisNode = dijkstraIndexes[thisNodeIndex];
						int thatNode = dijkstraIndexes[i];
						if (state[i] == 0 && Tile.graph[thisNode][thatNode] < Tile.WALLGRAPHCOST)
						{
							state[i] = 1;
							if (Tile.graph[thisNode][thatNode] + costs[dijkstraBefore[i]] <= costs[i])
							{
								dijkstraBefore[i] = thisNodeIndex;
								costs[i] = Tile.graph[thisNode][thatNode] + costs[dijkstraBefore[i]];
							}
						}
					}
				}

				// Do we have to continue?
				int min = Tile.WALLGRAPHCOST;
				int minIndex = -1;
				for (int i = 0; i < totalDijkstraNodes; i++) {
					if (state[i] < 2 && costs[i] <= min)
					{
						minIndex = i;
						min = costs[i];
						thisNodeIndex = minIndex;
					}
				}
			}

			for (int i = 0; i < totalDijkstraNodes; i++) {
				Tile.map[dijkstraIndexes[i]].setFlag(tileStatusIndex,true);
			}
		}

		public void DijstraPath(Tile center, int tileStatusIndex)
		{
			Tile.ResetEspecificMapStatus(tileStatusIndex);
			int target = GM.CurrentPlayer.mapLocation._index;
			int current = center._index;
			int tries = 0;
			while (current != target && tries < range) 
			{
				tries++;
				Tile.map[current].setFlag(tileStatusIndex,true);
				for (int i = 0; i < dijkstraIndexes.Length; i++) {
					if (dijkstraIndexes[i] == current)
					{
						current = dijkstraIndexes[dijkstraBefore[i]];
						break;
					}
				}
			}
		}

        public void AreaSelection(Tile center, int tileStatusIndex)
        {
            Tile.ResetEspecificMapStatus(tileStatusIndex);
            int minX = center.row - range + 1;
            int maxX = center.row + range - 1;
            if (minX < 0)
            {
                minX = 0;
            }
            if (maxX >= Tile.mapSize)
            {
                maxX = Tile.mapSize-1;
            }

            int minY = center.column - range + 1;
            int maxY = center.column + range - 1;
            if (minY < 0)
            {
                minY = 0;
            }
            if (maxY >= Tile.mapSize)
            {
                maxY = Tile.mapSize-1;
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
#endregion
#region Events
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
#endregion

    }
}