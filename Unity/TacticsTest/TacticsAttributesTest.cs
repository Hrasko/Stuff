using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TacticsTest
{
    [TestClass]
    public class TacticsAttributesTest
    {
        [TestMethod]
        public void XP0Value10()
        {
            AtributtesTactics ta = new AtributtesTactics();
            ta.id = 0;
            ta.initValue = 10;
            CharacterTactics tc = new CharacterTactics();
            tc.attributesXP = new float[1];
            tc.attributesXP[0] = 0;

            float value = ta.Value(tc);

            Assert.AreEqual(value, 10);
        }

        [TestMethod]
        public void XP1Value11()
        {
            AtributtesTactics ta = new AtributtesTactics();
            ta.id = 0;
            ta.initValue = 10;
            CharacterTactics tc = new CharacterTactics();
            tc.attributesXP = new float[1];
            tc.attributesXP[0] = 1;

            float value = ta.Value(tc);

            Assert.AreEqual(value, 11);
        }

        [TestMethod]
        public void XP2and5Value11()
        {
            AtributtesTactics ta = new AtributtesTactics();
            ta.id = 0;
            ta.initValue = 10;
            CharacterTactics tc = new CharacterTactics();
            tc.attributesXP = new float[1];
            tc.attributesXP[0] = 2.5f;

            float value = ta.Value(tc);

            Assert.AreEqual(value, 11);
        }

        [TestMethod]
        public void XP3and5Value12()
        {
            AtributtesTactics ta = new AtributtesTactics();
            ta.id = 0;
            ta.initValue = 10;
            CharacterTactics tc = new CharacterTactics();
            tc.attributesXP = new float[1];
            tc.attributesXP[0] = 3.5f;

            float value = ta.Value(tc);

            Assert.AreEqual(value, 12);
        }

    }
}
