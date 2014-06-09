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
			for (int i = 0; i < Tile.all.Count; i++) {
				Tile.all[i].setFlag(tileStatusIndex,true);
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
            Vector3 centerPosition = new Vector3(
                center._row * Tile.tileSize,
                center._column * Tile.tileSize,
                0);

            Collider[] tilesInArea = Physics.OverlapSphere(centerPosition, range * Tile.tileSize, Tile.layerTile);
            for (int i = 0; i < tilesInArea.Length; i++)
            {
                Tile tileInArea = tilesInArea[i].gameObject.GetComponent<TileBehaviour>()._tile;
                listIndexes.Add(tileInArea._index);
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
			// Initiate the cost and before vectors
			dijkstraBefore = new int[totalDijkstraNodes];
			for (int i = 0; i < totalDijkstraNodes; i++) {
				costs[i] = Tile.graphCost(dijkstraIndexes[thisNodeIndex],dijkstraIndexes[i]);
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
                        int cost = Tile.graphCost(thisNode, thatNode);
						if (state[i] == 0 && cost < Tile.WALLGRAPHCOST)
						{
							state[i] = 1;
                            if (cost + costs[dijkstraBefore[i]] <= costs[i])
							{
								dijkstraBefore[i] = thisNodeIndex;
                                costs[i] = cost + costs[dijkstraBefore[i]];
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
				Stack<int> pilha = GetDijstraPath(Tile.all[dijkstraIndexes[i]]);
				int currentCost = 0;
				while (pilha.Count > 1 && currentCost <= range){
					int currentIndex = pilha.Pop();
					Tile.all[currentIndex].setFlag(tileStatusIndex,true);
					//currentCost += Tile.graph[dijkstraIndexes[currentIndex]][dijkstraIndexes[dijkstraBefore[currentIndex]]];
				}
			}
		}

		private Stack<int> GetDijstraPath(Tile target)
		{
			int begin = GM.CurrentPlayer.mapLocation._index;
			int current = target._index;
			int tries = 0;
			float currentCost = 0;
			Stack<int> myPath = new Stack<int> ();
			while (current != begin && tries < Tile.totalNodes && currentCost <= range) 
			{
				tries++;

				for (int i = 0; i < dijkstraIndexes.Length; i++) {
					if (dijkstraIndexes[i] == current)
					{
						current = dijkstraIndexes[dijkstraBefore[i]];
						currentCost += Tile.graphCost(dijkstraIndexes[i],dijkstraIndexes[dijkstraBefore[i]]);
						break;
					}
				}
				if (currentCost <= range){
					myPath.Push(current);
				}
			}
			myPath.Push(begin);
			return myPath;
		}

		public void DijstraPath(Tile center, int tileStatusIndex)
		{
			Tile.ResetEspecificMapStatus(tileStatusIndex);
			Stack<int> pilha = GetDijstraPath(center);
			while (pilha.Count > 0) {
								Tile.all[pilha.Pop()].setFlag (tileStatusIndex, true);
						}
		}

        public void AreaSelection(Tile center, int tileStatusIndex)
        {
            Tile.ResetEspecificMapStatus(tileStatusIndex);
            Vector3 centerPosition = new Vector3(
                center._row * Tile.tileSize,
                center._column * Tile.tileSize,
                0);

            Collider[] tilesInArea = Physics.OverlapSphere(centerPosition, range * Tile.tileSize, Tile.layerTile);
            for (int i = 0; i < tilesInArea.Length; i++)
            {
                Tile tileInArea = tilesInArea[i].gameObject.GetComponent<TileBehaviour>()._tile;
                tileInArea.setFlag(tileStatusIndex, true);
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