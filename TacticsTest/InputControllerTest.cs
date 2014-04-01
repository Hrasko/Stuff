using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tactics;

namespace TacticsTest
{
    [TestClass]
    public class InputControllerTest
    {
        [TestMethod]
        public void InAreaSelectorTest()
        {
            Tile.map = new Tile[25];
            Tile.mapSize = 5;
            for (int i = 0; i < Tile.map.Length; i++)
            {
                Tile.map[i] = new Tile(i);
            }
            NormalMovement action = new NormalMovement();
            action.activate(Tile.get(3,3));

            Assert.IsFalse(Tile.get(0, 0).statusFlag[Tile.INAREA]);
        }
    }
}
