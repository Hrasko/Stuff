using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace TacticsTest
{
    [TestClass]
    public class Sandbox
    {
        
        [Flags]
        public enum myflags
        {
            a = 1,
            b = 2,
            c = 4,
            d = 8
        }

        [TestMethod]
        public void TestMethod1()
        {
            string textoCompleto = "À exceção do Jornal da Record, todos os principais telejornais das maiores emissoras estão perdendo audiência de modo irreversível; números do Ibope mostram que tombo maior é no Jornal Nacional, ancorado por William Bonner; perda na Grande São Paulo foi de 9% na pontuação de janeiro a abril; audiência nacional, que era de quase 50 pontos nos anos 1990, está agora em apenas 29 pontos de média; na baixa mais dramática";
            string[] splitted = textoCompleto.Split(' ');
            System.Text.StringBuilder sb = new System.Text.StringBuilder(splitted[0]);
            int maxLetras = 18;
            int letrasSobrando = maxLetras - splitted[0].Length;
            for (int i = 1; i < splitted.Length; i++)
            {
                sb.Append(" ");
                letrasSobrando--;
                if (splitted[i].Length > letrasSobrando)
                {
                   sb.Append("/n");
                   letrasSobrando = 18;
                }
                letrasSobrando -= splitted[i].Length;
                sb.Append(splitted[i]);
            }
            Debug.Write(sb.ToString());
        }
    }
}
