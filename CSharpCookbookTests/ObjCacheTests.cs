using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpCookbook.Tests
{
    public class SomeComplexObj
    {
        public SomeComplexObj() {  }

        private int _idcode = -1;

        public int IdCode
        {
            get { return _idcode; }
            set { _idcode = value; }
        }
    }

    [TestClass()]
    public class ObjCacheTests
    {
        static ObjCache<string, SomeComplexObj> oc = new ObjCache<string, SomeComplexObj>();
        [TestMethod()]
        public void ObjCacheTest()
        {
            oc["ID1"] = new SomeComplexObj();
            oc["ID2"] = new SomeComplexObj();
            oc["ID3"] = new SomeComplexObj();
            oc["ID4"] = new SomeComplexObj();
            oc["ID5"] = new SomeComplexObj();

            Console.WriteLine("---> Add 5 weak references");
            Console.WriteLine($"OC.TotalCacheSlots = {oc.TotalCacheSlots()}");
            Console.WriteLine($"OC.AliveObjsInCache = {oc.AliveObjsInCache()}");

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("---> Collect all weak references");
            Console.WriteLine($"OC.TotalCacheSlots = {oc.TotalCacheSlots()}");
            Console.WriteLine($"OC.AliveObjsInCache = {oc.AliveObjsInCache()}");

            oc["ID1"] = new SomeComplexObj();
            oc["ID2"] = new SomeComplexObj();
            oc["ID3"] = new SomeComplexObj();
            oc["ID4"] = new SomeComplexObj();
            oc["ID5"] = new SomeComplexObj();

            Console.WriteLine("---> Add 5 weak references");
            Console.WriteLine($"OC.TotalCacheSlots = {oc.TotalCacheSlots()}");
            Console.WriteLine($"OC.AliveObjsInCache = {oc.AliveObjsInCache()}");

            CreateObjLongMethod();
            Create135();
            CollectAll();
        }

        private void CreateObjLongMethod()
        {
            Console.WriteLine("----> Obtain ID1");
            string id1 = "ID1";
            if (oc.IsObjectAlive(ref id1))
            {
                var temp = oc["ID1"];
                temp.IdCode = 100;
                Console.WriteLine($"temp.IdCode = {temp.IdCode}");
            }
            else
            {
                Console.WriteLine("Object ID1 does not exist...Creating new ID1...");
                oc["ID1"] = new SomeComplexObj();
                var temp = oc["ID1"];
                temp.IdCode = 101;
                Console.WriteLine($"temp.IdCode = {temp.IdCode}");
            }
        }

        private void Create135()
        {
            Console.WriteLine("---> Obtain ID1, ID3, ID5");
            var sco1 = oc["ID1"];
            var sco3 = oc["ID3"];
            var sco5 = oc["ID5"];
            sco1.IdCode = 1000;
            sco3.IdCode = 3000;
            sco5.IdCode = 5000;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("---->Collect all weak references");
            Console.WriteLine($"OC.TotalCacheSlots = {oc.TotalCacheSlots()}");
            Console.WriteLine($"OC.AliveObjsInCache = {oc.AliveObjsInCache()}");

            Console.WriteLine($"SCO1.IDCode = {sco1.IdCode}");
            Console.WriteLine($"SCO3.IDCode = {sco3.IdCode}");
            Console.WriteLine($"SCO5.IDCode = {sco5.IdCode}");

            string id2 = "ID2";
            Console.WriteLine($"---->Get ID2, which has been collected. ID2 Exist=={oc.IsObjectAlive(ref id2)}");
            var sco2 = oc["ID2"];
            Console.WriteLine($"ID2 has now been re-created. ID2 Exist == {oc.IsObjectAlive(ref id2)}");
            Console.WriteLine($"OC.AliveObjsInCache = {oc.AliveObjsInCache()}");
            sco2.IdCode = 2000;
            Console.WriteLine($"Sco2.IDCode = {sco2.IdCode}");

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("--->Collect all weak references");
            Console.WriteLine($"OC.TotalCacheSlots = {oc.TotalCacheSlots()}");
            Console.WriteLine($"OC.AliveObjsInCache = {oc.AliveObjsInCache()}");
        }

        private void CollectAll()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine("--->Collect all weak references");
            Console.WriteLine($"OC.TotalCacheSlots = {oc.TotalCacheSlots()}");
            Console.WriteLine($"OC.AliveObjsInCache = {oc.AliveObjsInCache()}");

        }
    }
}