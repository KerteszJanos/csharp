using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerteszJanos_OEP_NagyBead
{
    public abstract class State
    {
        public virtual bool IsMegbizason() { return false; }
        public virtual bool IsUton() { return false; }
        public virtual bool IsTelephelyen() { return false; }
    }

    public class Megbizason : State
    {
        private static Megbizason instance;
        private Megbizason() { } //kell a private ctor ha nincs adattag a szülőben??
        public static Megbizason Instance()
        {
            if (instance == null)
            {
                instance = new Megbizason();
            }
            return instance;
        }
        public override bool IsMegbizason() { return true; }
    }
    public class Uton : State //singleton
    {
        private static Uton instance;
        private Uton() { } //kell a private ctor ha nincs adattag a szülőben??
        public static Uton Instance()
        {
            if (instance == null)
            {
                instance = new Uton();
            }
            return instance;
        }
        public override bool IsUton() { return true; }
    }
    public class Telephelyen : State //singleton
    {
        private static Telephelyen instance;
        private Telephelyen() { } //kell a private ctor ha nincs adattag a szülőben??
        public static Telephelyen Instance()
        {
            if (instance == null)
            {
                instance = new Telephelyen();
            }
            return instance;
        }
        public override bool IsTelephelyen() { return true; }
    }
}
