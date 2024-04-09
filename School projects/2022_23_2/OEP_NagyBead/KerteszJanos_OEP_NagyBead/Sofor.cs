using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerteszJanos_OEP_NagyBead
{
    public abstract class Sofor
    {
        public readonly string nev;

        public Sofor(string nev)
        {
            this.nev = nev;
        }

        public virtual bool isKezdo()
        {
            return false;
        }
        public virtual bool isGyakorlott()
        {
            return false;
        }
        public virtual bool isTorzstag()
        {
            return false;
        }
    }

    public class Kezdo : Sofor
    {
        public Kezdo(string nev) :base(nev) { }
        public override bool isKezdo()
        { 
            return true; 
        }
    }
    public class Gyakorlott : Sofor
    {
        public Gyakorlott(string nev) : base(nev) { }
        public override bool isGyakorlott()
        {
            return true;
        }
    }
    public class Torzstag : Sofor
    {
        public Torzstag(string nev) : base(nev) { }
        public override bool isTorzstag()
        {
            return true;
        }
    }
}
