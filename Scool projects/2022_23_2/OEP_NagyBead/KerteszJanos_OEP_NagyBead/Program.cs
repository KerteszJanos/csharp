namespace KerteszJanos_OEP_NagyBead
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sw = new StreamReader("input.txt");
            string line;
            while ((line = sw.ReadLine()) != null)
            {
                string[] adatok = line.Split(';');
                switch (adatok[0])
                {
                    case "C":
                        Ceg ceg = new Ceg(adatok[1]);
                        break;
                    case "S":
                        if (adatok[1] == "Kezdo")
                        {
                            Kezdo kezdo = new Kezdo(adatok[2]);
                        }
                        else if (adatok[1] == "Gyakorlott")
                        {
                            Gyakorlott gyakorlott = new Gyakorlott(adatok[2]);
                        }
                        else
                        {
                            Torzstag torzstag = new Torzstag(adatok[2]);
                        }
                        break;
                    case "T":
                        Telephely telephely = new Telephely(adatok[1]);
                        break;
                    case "K":
                        if (adatok[1] == "Fulkes")
                        {
                            Fulkes fulkes = new Fulkes(adatok[2],int.Parse(adatok[3]), int.Parse(adatok[4]));
                        }
                        else
                        {
                            Nyerges nyerges = new Nyerges(adatok[2], int.Parse(adatok[3]), int.Parse(adatok[4]));
                        }
                        break;
                    default: break;
                }
            }
            sw.Close();
        }
    }
}