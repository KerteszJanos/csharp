using System.Runtime.ConstrainedExecution;

namespace KerteszJanos_OEP_NagyBead
{
    [TestClass]
    public class objektum_kapcsolatok
    {
        [TestMethod]
        public void Ceg_Telephely()
        {
            Ceg ceg = new Ceg("Jani kft");
            Telephely telephely = new Telephely("telephely");

            ceg.addTelephely(telephely);

            Assert.AreEqual(telephely, ceg.telephelyek[0]);
            Assert.AreEqual(ceg,telephely.ceg);
        }
        [TestMethod]
        public void Ceg_Sofor()
        {
            Ceg ceg = new Ceg("Jani kft");
            Kezdo sofor_1 = new Kezdo("Sanyi");
            Gyakorlott sofor_2 = new Gyakorlott("Pisti");
            Torzstag sofor_3 = new Torzstag("Bence");

            ceg.soforok.Add(sofor_1);
            ceg.soforok.Add(sofor_2);
            ceg.soforok.Add(sofor_3);

            Assert.AreEqual(3,ceg.soforok.Count);
            Assert.AreEqual("Sanyi", ceg.soforok[0].nev);
            Assert.AreEqual(true, ceg.soforok[1].isGyakorlott());
        }
        [TestMethod]
        public void Telephely_Kamion()
        {
            Telephely telephely = new Telephely("telephely");
            Nyerges kamion_1 = new Nyerges("ABC-123", 100, 50);
            Fulkes kamion_2 = new Fulkes("CBA-321", 70, 40);
            
            telephely.addKamion(kamion_1);
            telephely.addKamion(kamion_2);

            Assert.AreEqual(2,telephely.kamionok.Count);
            Assert.AreEqual(telephely, kamion_1.telephely);

            telephely.removeKamion(kamion_1);

            Assert.AreEqual(1,telephely.kamionok.Count);
            Assert.AreEqual(null,kamion_1.telephely);
        }
        [TestMethod]
        public void Kamion_Megbizas__AND__Kamion_Telephely_Ceg()
        {
            Ceg ceg = new Ceg("Jani kft");
            Telephely telephely = new Telephely("telephely");
            Nyerges kamion = new Nyerges("ABC-123", 100, 50);
            Kezdo sofor = new Kezdo("Sanyi");
            Megbizas megbizas = new Megbizas(0, 0, 0, 0, 0, sofor, kamion);

            ceg.addTelephely(telephely);
            ceg.soforok.Add(sofor);
            telephely.addKamion(kamion);

            kamion.MegbizastKap(megbizas);
            //|
            Assert.AreEqual(true,kamion.state.IsMegbizason());
            Assert.AreEqual(megbizas,kamion.megbizas);

            kamion.MegbizasTeljesitve(12,telephely);
            //|
            Assert.AreEqual(true,kamion.state.IsTelephelyen());
            Assert.AreEqual(null,kamion.megbizas);

            //Mivel a MegbizasTeljesitve metodus automatikusan hivja a megbizasKesz()-t:
            Assert.AreEqual(megbizas, ceg.megbizasok[0]);
            Assert.AreEqual(12, ceg.megbizasok[0].erkezesiIdo);
        }
    }
    [TestClass]
    public class feladatok
    {
        [TestMethod]
        public void minKamion_Test() 
        {
            //1
            Ceg ceg = new Ceg("Jani kft");
            Telephely telephely_1 = new Telephely("telephely_1");
            Nyerges kamion_1 = new Nyerges("ABC-123", 100, 50);
            Nyerges kamion_2 = new Nyerges("ABC-123", 100, 50);
            telephely_1.addKamion(kamion_1);
            telephely_1.addKamion(kamion_2);
            ceg.addTelephely(telephely_1);
            
            Assert.AreEqual(telephely_1,ceg.minKamion());

            //tobb
            Telephely telephely_2 = new Telephely("telephely_2");
            Telephely telephely_3 = new Telephely("telephely_3");
            Nyerges kamion_3 = new Nyerges("ABC-123", 100, 50);
            Fulkes kamion_4 = new Fulkes("ABC-123", 100, 50);
            Fulkes kamion_5 = new Fulkes("ABC-123", 100, 50);
            Fulkes kamion_6 = new Fulkes("ABC-123", 100, 50);
            telephely_2.addKamion(kamion_3);
            telephely_2.addKamion(kamion_4);
            telephely_2.addKamion(kamion_5);
            telephely_3.addKamion(kamion_6);
            ceg.addTelephely(telephely_2);
            ceg.addTelephely(telephely_3);
            
            Assert.AreEqual(telephely_3, ceg.minKamion());
        }
        [TestMethod]
        public void maxTeher_Test()
        {
            //0
            Telephely telephely = new Telephely("telephely");
            Assert.AreEqual(0,telephely.MaxTeher());

            //tobb
            Nyerges kamion_1 = new Nyerges("ABC-123", 100, 50);
            Nyerges kamion_2 = new Nyerges("ABC-123", 100, 50);
            Nyerges kamion_3 = new Nyerges("ABC-123", 100, 50);
            telephely.addKamion(kamion_1);
            telephely.addKamion(kamion_2);
            telephely.addKamion(kamion_3);

            Assert.AreEqual(300,telephely.MaxTeher());

        }
        [TestMethod]
        public void voltTulterhelt_Test()
        {
            //0
            Ceg ceg = new Ceg("Jani kft");

            Assert.AreEqual(false,ceg.VoltTulterhelt());

            //tobb
            Nyerges kamion_1 = new Nyerges("ABC-123",100,50);
            Nyerges kamion_2 = new Nyerges("ABC-123",100,50);
            Fulkes kamion_3 = new Fulkes("ABC-123",100,50);
            Fulkes kamion_4 = new Fulkes("ABC-123",100,50);
            Torzstag sofor_1 = new Torzstag("AlphaMale");
            Megbizas megbizas_1 = new Megbizas(0,0,0,0,0,sofor_1,kamion_1);
            Megbizas megbizas_2 = new Megbizas(0,0,0,0,0,sofor_1,kamion_2);
            Megbizas megbizas_3 = new Megbizas(0,101,0,0,0,sofor_1,kamion_3); //<- tulterhelt
            Megbizas megbizas_4 = new Megbizas(0,0,0,0,0,sofor_1,kamion_4);
            ceg.megbizasok.Add(megbizas_1);
            ceg.megbizasok.Add(megbizas_2);
            ceg.megbizasok.Add(megbizas_3);
            ceg.megbizasok.Add(megbizas_4); //(nem szimulalom le az egesz fuvart hogy addolhassam, hell no)

            Assert.AreEqual(true,ceg.VoltTulterhelt());
        }
        [TestMethod]
        public void nyereseg_Test()
        {
            //0
            Ceg ceg = new Ceg("Jani kft");

            Assert.AreEqual(0, ceg.Nyereseg());

            //tobb
            Nyerges kamion_1 = new Nyerges("ABC-123", 100, 50);
            Fulkes kamion_2 = new Fulkes("ABC-123", 100, 50);
            Kezdo sofor_1 = new Kezdo("traPista");
            Gyakorlott sofor_2 = new Gyakorlott("ZsigaBiga");
            Torzstag sofor_3 = new Torzstag("AlphaMale");
            Megbizas megbizas_1 = new Megbizas(50, 0, 2000, 0, 0, sofor_1, kamion_1);
            Megbizas megbizas_2 = new Megbizas(50, 0, 2000, 0, 0, sofor_2, kamion_1);
            Megbizas megbizas_3 = new Megbizas(50, 0, 2000, 0, 0, sofor_3, kamion_1);
            Megbizas megbizas_4 = new Megbizas(50, 0, 2000, 0, 0, sofor_1, kamion_2);
            Megbizas megbizas_5 = new Megbizas(50, 0, 2000, 0, 0, sofor_2, kamion_2);
            Megbizas megbizas_6 = new Megbizas(50, 0, 2000, 0, 0, sofor_3, kamion_2);

            ceg.megbizasok.Add(megbizas_1);
            Assert.AreEqual(725,ceg.Nyereseg());

            ceg.megbizasok.Remove(megbizas_1);
            ceg.megbizasok.Add(megbizas_2);
            Assert.AreEqual(225, ceg.Nyereseg());

            ceg.megbizasok.Remove(megbizas_2);
            ceg.megbizasok.Add(megbizas_3);
            Assert.AreEqual(-25, ceg.Nyereseg()); //veszteseges

            ceg.megbizasok.Remove(megbizas_3);
            ceg.megbizasok.Add(megbizas_4);
            Assert.AreEqual(975, ceg.Nyereseg());

            ceg.megbizasok.Remove(megbizas_4);
            ceg.megbizasok.Add(megbizas_5);
            Assert.AreEqual(475, ceg.Nyereseg());

            ceg.megbizasok.Remove(megbizas_5);
            ceg.megbizasok.Add(megbizas_6);
            Assert.AreEqual(-25, ceg.Nyereseg());

            ceg.megbizasok.Add(megbizas_1);
            ceg.megbizasok.Add(megbizas_2);
            ceg.megbizasok.Add(megbizas_3);
            ceg.megbizasok.Add(megbizas_4);
            ceg.megbizasok.Add(megbizas_5);
            Assert.AreEqual(2350, ceg.Nyereseg());
        }
    }
}