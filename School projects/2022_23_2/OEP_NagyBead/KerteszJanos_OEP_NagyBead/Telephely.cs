using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerteszJanos_OEP_NagyBead
{
    public class Telephely
    {
        public string cim;
        public Ceg ceg;
        public List<Kamion> kamionok;

        public Telephely(string cim)
        {
            this.cim = cim;
            kamionok = new List<Kamion>();

        }
        //kiszamolja a telephelyen allomasozo kamionok egyuttes teherbirasat
        public void addKamion(Kamion kamion)
        {
            kamion.telephely = this;
            kamionok.Add(kamion);
        }
        public void removeKamion(Kamion kamion)
        {
            kamion.telephely = null;
            kamionok.Remove(kamion);
        }
        public void megbizasKesz(Megbizas megbizas)
        {
            ceg.megbizasKesz(megbizas);
        }

        //Feladat_2
        public double MaxTeher()
        {
            double teherSum = 0;
            for (int i = 0; i < kamionok.Count; i++)
            {
                teherSum += kamionok[i].terhelhetoseg;
            }
            return teherSum;
        }
    }
}
