using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace KerteszJanos_OEP_NagyBead
{
    public class Megbizas
    {
        public int fuvTav; //km
        public int fuvSuly; //kg
        public int fuvDij;
        public int indulas; //ora
        public int szallitasiIdo; //ora
        public int erkezesiIdo; //ora

        public Sofor sofor; //nev
        public Kamion kamion; //rendszam

        public Megbizas(int fuvTav, int fuvSuly, int fuvDij, int indulas, int szallitasiIdo, Sofor sofor, Kamion kamion)
        {
            this.fuvTav = fuvTav;
            this.fuvSuly = fuvSuly;
            this.fuvDij = fuvDij;
            this.indulas = indulas;
            this.szallitasiIdo = szallitasiIdo;
            this.erkezesiIdo = -1;
            this.sofor = sofor;
            this.kamion = kamion;
        }

        public double nyereseg()
        {
            int ber = 0;
            if (this.kamion.IsNyerges())
            {
                if (this.sofor.isKezdo())
                {
                    ber = this.fuvTav * 25;
                }
                else if (this.sofor.isGyakorlott())
                {
                    ber = this.fuvTav * 35;
                }
                else
                {
                    ber = this.fuvTav * 40;
                }
            }
            else //fulkes
            {
                if (this.sofor.isKezdo())
                {
                    ber = this.fuvTav * 20;
                }
                else if (this.sofor.isGyakorlott())
                {
                    ber = this.fuvTav * 30;
                }
                else
                {
                    ber = this.fuvTav * 40;
                }
            }
            return (this.fuvDij) - (this.kamion.fogyasztas / 100 * this.fuvTav) - ber;
        }
    }
}
