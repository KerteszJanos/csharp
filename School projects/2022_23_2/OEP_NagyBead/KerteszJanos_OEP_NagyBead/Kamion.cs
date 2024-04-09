using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerteszJanos_OEP_NagyBead
{
    public abstract class Kamion
    {
        public readonly string rendszam;
        public readonly double terhelhetoseg; //kg
        public readonly double fogyasztas; //100km-en forintban
        public readonly int tengelyszam; //Nyergesnel 3, Fulkesnel 2

        public Telephely telephely;
        public State state { get; private set; } //Megbizason, Uton, Telephelyen
        public Megbizas megbizas { get; private set; }

        protected Kamion(string rendszam, double terhelhetoseg, double fogyasztas, int tengelyszam)
        {
            this.rendszam = rendszam;
            this.terhelhetoseg = terhelhetoseg;
            this.fogyasztas = fogyasztas;
            this.tengelyszam = tengelyszam;
        }
        public virtual bool IsNyerges()
        {
            return false;
        }
        public virtual bool IsFulkes()
        {
            return false;
        }
        public void changeState(State state, Telephely t)
        {
            if (state.IsMegbizason() || state.IsUton())
            {
                this.telephely.removeKamion(this);
                //this.telephely = null; <-- ez a removeKamion()-ban tortenik
            }
            else //IsTelephelyen
            {
                this.telephely = t;
                telephely.addKamion(this);
            }
            this.state = state;
        }
        public void MegbizastKap(Megbizas m)
        {
            if (this.megbizas == null)
            {
                this.megbizas = m;
                changeState(Megbizason.Instance(), null);
            }
            else
            {
                throw new Exception("Ez a kamion éppen megbízást teljesít");
            }
        }
        public void MegbizasTeljesitve(int erkezes, Telephely t) //mikkra szallitotta ki, es melyik telephelyre ert vissza
        {
            this.megbizas.erkezesiIdo = erkezes;
            changeState(Telephelyen.Instance(), t);
            telephely.megbizasKesz(megbizas);
            this.megbizas = null;
        }
    }

    public class Nyerges : Kamion
    {
        public Nyerges(string rendszam, double terhelhetoseg, double fogyasztas) : base(rendszam, terhelhetoseg, fogyasztas, 3) { }
        public override bool IsNyerges()
        {
            return true;
        }
    }
    public class Fulkes : Kamion
    {
        public Fulkes(string rendszam, double terhelhetoseg, double fogyasztas) : base(rendszam, terhelhetoseg, fogyasztas, 2) { }
        public override bool IsFulkes()
        {
            return true;
        }
    }
}
