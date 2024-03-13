using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerteszJanos_OEP_NagyBead
{
    public class Ceg
    {
        public string cegNev;
        public List<Telephely> telephelyek { get; private set; }
        public List<Sofor> soforok;
        public List<Megbizas> megbizasok { get; private set; }

        public Ceg(string cegNev)
        {
            this.cegNev = cegNev;
            this.telephelyek = new List<Telephely>();
            this.soforok = new List<Sofor>();
            this.megbizasok = new List<Megbizas>();
        }
        public void addTelephely(Telephely telephely)
        {
            telephely.ceg = this;
            telephelyek.Add(telephely);
        }
        public void megbizasKesz(Megbizas megbizas)
        {
            megbizasok.Add(megbizas);
        }

        //Feladat_1
        //megkeresi a legkevesebb kamionnal rendelkezo telephelyet, majd vissza adja azt (az egesz objektumot)
        public Telephely minKamion()
        {
            Telephely minT = telephelyek[0];
            int minKamion = telephelyek[0].kamionok.Count;
            for (int i = 0; i < telephelyek.Count; i++)
            {
                if (minKamion > telephelyek[i].kamionok.Count)
                {
                    minKamion = telephelyek[i].kamionok.Count;
                    minT = telephelyek[i];
                }
            }
            return minT;
        }

        //Feladat_3
        public bool VoltTulterhelt()
        {
            for (int i = 0; i < megbizasok.Count; i++)
            {
                if (megbizasok[i].kamion.terhelhetoseg < megbizasok[i].fuvSuly)
                {
                    return true;
                }
            }
            return false;
        }

        //Feladat_4
        public double Nyereseg()
        {
            double nyereseg = 0;
            for (int i = 0; i < megbizasok.Count; i++)
            {
                nyereseg += megbizasok[i].nyereseg();
            }
            return nyereseg;
        }
    }
}
