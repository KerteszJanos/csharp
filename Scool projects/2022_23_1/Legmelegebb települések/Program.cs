using System;

namespace progalap_komplexBead
{
    class Program
    {
        const int MAX = 1000;
        static void ellenorzottbeolvas(ref int[,] telepulesXnap, out int tsz, out int nsz)
        {
            nsz = 0;
            bool jo;
            do
            {
                Console.Error.Write("n, m : (\"n m\") = ");
                string[] tnh = Console.ReadLine().Split();
                if (tnh.Length == 2)
                {
                    jo = int.TryParse(tnh[0], out tsz) && tsz >= 0 && tsz <= 1000 &&
                         int.TryParse(tnh[1], out nsz) && nsz >= 0 && nsz <= 1000;
                    if (!jo)
                    {
                        Console.Error.WriteLine("Hibás adat! Add meg újra!");
                    }
                }
                else
                {
                    tsz = 0;
                    Console.Error.WriteLine("Hibás adat! Add meg újra!");
                    jo = false;
                }
            } while (!jo);


            int szam;
            for (int i = 0; i < tsz; i++)
            {
                bool jo2 = true;
                Console.Error.Write(i + 1 + ". sor=  ");
                string[] sor = Console.ReadLine().Split(" ");
                if (sor.Length == nsz)
                {
                    int e = 0;
                    while (e < nsz && jo2)
                    {
                        jo2 = int.TryParse(sor[e], out szam) && szam >= -50 && szam <= 50;
                        e++;
                    }
                }
                else
                {
                    jo2 = false;
                }
                if (!jo2)
                {
                    Console.Error.WriteLine("Hibás adat! Add meg újra.");
                }
                while (!jo2)
                {
                    jo2 = true;
                    Console.Error.Write(i + 1 + ". sor=  ");
                    sor = Console.ReadLine().Split(" ");
                    if (sor.Length == nsz)
                    {
                        int e = 0;
                        while (e < nsz && jo2)
                        {
                            jo2 = int.TryParse(sor[e], out szam) && szam >= -50 && szam <= 50;
                            e++;
                        }
                    }
                    else
                    {
                        jo2 = false;
                    }
                    if (!jo2)
                    {
                        Console.Error.WriteLine("Hibás adat! Add meg újra.");
                    }
                }
                //ertekadas
                for (int k = 0; k < nsz; k++)
                {
                    telepulesXnap[i, k] = Int32.Parse(sor[k]);
                }
            }
        }
        static void beolvas(ref int[,] telepulesXnap, out int tsz, out int nsz)
        {
            string[] tnh = Console.ReadLine().Split();
            tsz = Int32.Parse(tnh[0]);
            nsz = Int32.Parse(tnh[1]);

            for (int a = 0; a < tsz; a++)
            {
                string[] oszlop = Console.ReadLine().Split(" ");
                for (int k = 0; k < nsz; k++)
                {
                    telepulesXnap[a, k] = Int32.Parse(oszlop[k]);
                }
            }
        }
        static int mxmax(int[,] telepulesXnap, int tsz, int nsz)
        {
            int max = telepulesXnap[0, 0];
            for (int i = 0; i < tsz; i++)
            {
                for (int j = 0; j < nsz; j++)
                {
                    if (max < telepulesXnap[i, j])
                    {
                        max = telepulesXnap[i, j];
                    }
                }
            }
            return max;
        } //maxmax2 az egyben
        static bool vane(int[,] telepulesXnap, int i, int nsz, int max)
        {
            bool vane = false;
            int j = 0;
            while (!vane && j < nsz)
            {
                if (max == telepulesXnap[i, j])
                {
                    vane = true;
                }
                j++;
            }

            return vane;
        }

        static void Main(string[] args)
        {
            int tsz;
            int nsz;
            int[,] telepulesXnap = new int[MAX, MAX];

            //ellenorzottbeolvas(ref telepulesXnap, out tsz, out nsz);
            beolvas(ref telepulesXnap, out tsz, out nsz);

            int db = -1;
            int[] y = new int[MAX];

            int max = mxmax(telepulesXnap, tsz, nsz);
            for (int i = 0; i < tsz; i++)
            {
                if (vane(telepulesXnap, i, nsz, max))
                {
                    db++;
                    y[db] = i;
                }
            }
            Console.Write(db + 1 + " ");
            for (int i = 0; i < db + 1; i++)
            {
                Console.Write(y[i] + 1 + " ");
            }
        }
    }
}
