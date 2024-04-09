using System;
using System.Collections.Generic;

namespace progalap2_bead
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] sor = Console.ReadLine().Split(" ");
            int days = Int32.Parse(sor[0]);
            int money = Int32.Parse(sor[1]);
            int runningmoney;

            sor = Console.ReadLine().Split(" ");
            int max = -1;
            int szamlalo;
            for (int i = 0; i < sor.Length; i++)
            {
                szamlalo = 0;
                runningmoney = money;
                while (i + szamlalo < sor.Length && runningmoney >= Int32.Parse(sor[i + szamlalo]))
                {
                    runningmoney -= Int32.Parse(sor[i + szamlalo]);
                    szamlalo++;
                }
                if (max<szamlalo)
                {
                    max = szamlalo;
                }
            }
            Console.WriteLine(max);
        }
    }
}
