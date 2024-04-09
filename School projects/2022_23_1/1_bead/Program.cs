using System;

namespace progalap_1_bead
{
    class Program
    {
        static void Main(string[] args)
        {
            int s = 0; ;

            int mennyiseg = int.Parse(Console.ReadLine());
            int[] vilagcsucsok = new int[mennyiseg];
            for (int i = 0; i < mennyiseg; i++)
            {
                string[] sor = Console.ReadLine().Split();
                vilagcsucsok[i] = int.Parse(sor[1]);
            }

            int legutolso = vilagcsucsok[0];
            for (int i = 1; i < vilagcsucsok.Length; i++)
            {
                if (legutolso == vilagcsucsok[i])
                {
                    s++;
                }
                legutolso = vilagcsucsok[i];
            }
            Console.WriteLine(s);
        }
    }
}
