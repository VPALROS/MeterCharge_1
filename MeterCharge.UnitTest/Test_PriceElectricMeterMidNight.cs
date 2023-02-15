using MeterCharge.BusinessLogic;
using MeterCharge.BussinesLogic;
using MeterCharge.BussinesLogic.Entities;
using MeterCharge.Entity;
using MeterCharge.Entity.Type;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeterCharge.UnitTest
{
    [TestClass]
    public class Test_PriceElectricMeterMidNight
    {
        private IKernel _kernel;
        private IPersistMeter _testService;
        [TestInitialize]
        public void MyTestInitialize()
        {
            _kernel = new StandardKernel(new DependencyInjectionModule());
            _testService = _kernel.Get<IPersistMeter>();
        }


        [TestMethod]
        public void GetPriceElectricMeterEmptyList_IsTrue()
        {
            var m1 = new Meter {  };
            var list1 = new List<Meter> { m1 };
            var listChargableAssert = new List<int> {};
            List<int> listChargable = new List<int>();

            MeterChargeSaver meterSaver = new MeterChargeSaver(_testService);
            var data = meterSaver.GetData(list1);
            IEnumerable<List<MeterDataPersist>> dataField = data.Select(x => x.DataFields);

            
            foreach (List<MeterDataPersist> list in dataField)
            {
                var electricityList = list.Where(x => x.MeterType == MeterTypeEnum.Electricity);
                foreach (MeterDataPersist item in electricityList)
                {                 
                    
                    listChargable.Add(item.chargable);
                }
            }
            Assert.IsTrue(listChargable.SequenceEqual(listChargableAssert));
        }


        [TestMethod]
        public void GetPriceElectricMeterNoElectricElementList_IsTrue()
        {
            var m1 = new Meter { Id = Guid.NewGuid().ToString(), MeterType = MeterTypeEnum.Water, Readings = new List<int> { 3, 9 } };
            var m2 = new Meter { Id = Guid.NewGuid().ToString(), MeterType = MeterTypeEnum.Heating, Readings = new List<int> { 1, 2 } };
            var list1 = new List<Meter> { m1, m2 };
            
            var listChargableAssert = new List<int> { };
            List<int> listChargable = new List<int>();

            MeterChargeSaver meterSaver = new MeterChargeSaver(_testService);
            var data = meterSaver.GetData(list1);
            IEnumerable<List<MeterDataPersist>> dataField = data.Select(x => x.DataFields);


            foreach (List<MeterDataPersist> list in dataField)
            {
                var electricityList = list.Where(x => x.MeterType == MeterTypeEnum.Electricity);
                foreach (MeterDataPersist item in electricityList)
                {

                    listChargable.Add(item.chargable);
                }
            }
            Assert.IsTrue(listChargable.SequenceEqual(listChargableAssert));
        }

        [TestMethod]
        public void GetPriceElectricMeterMidnight_IsTrue()
        {
            var m1 = new Meter { Id = Guid.NewGuid().ToString(), MeterType = MeterTypeEnum.Electricity, Readings = new List<int> { 97, 50 } };
            var m2 = new Meter { Id = Guid.NewGuid().ToString(), MeterType = MeterTypeEnum.Heating, Readings = new List<int> { 1, 2 } };
            var list1 = new List<Meter> { m1,m2};

            //Object to comparation            
            var listChargableAssert = new List<int> { 97 * 2, 50 * 2 };
            List<int> listChargable = new List<int>();

            //Date to force for Check this scenario :halfPrice
            DateTime date = new DateTime(2023, 02, 08, 7, 0, 0);

            //Get Datas
            MeterChargeSaver meterSaver = new MeterChargeSaver(_testService);            
            var data=meterSaver.GetData(list1);
            IEnumerable<List<MeterDataPersist>> dataField = data.Select(x => x.DataFields);

            //Get Chargable
            foreach (List<MeterDataPersist> list in dataField)
            {
                var electricityList = list.Where(x => x.MeterType == MeterTypeEnum.Electricity);
                foreach(MeterDataPersist item in electricityList)
                {
                    item.RandomTimeMidNigh = date;
                    item.ForceData = true;//Force the midNight hour for check cost halfPrice
                    listChargable.Add(item.chargable);
                }

            }

            Assert.IsTrue(listChargable.SequenceEqual(listChargableAssert));

        }

        [TestMethod]
        public void GetPriceElectricMeterOutMidnight_IsFalse()
        {
            var m1 = new Meter { Id = Guid.NewGuid().ToString(), MeterType = MeterTypeEnum.Electricity, Readings = new List<int> { 97, 50 } };            
            var list1 = new List<Meter> { m1};

            //Object to comparation
            var listChargableAssert = new List<int> { 97 * 2, 50 * 2 };
            List<int> listChargable = new List<int>();
            
            //Date to force for Check this scenario: normalPrice
            DateTime date = new DateTime(2023, 02, 08, 16, 0, 0);

            //Get Datas
            MeterChargeSaver meterSaver = new MeterChargeSaver(_testService);
            var data = meterSaver.GetData(list1);
            IEnumerable<List<MeterDataPersist>> dataField = data.Select(x => x.DataFields);
            
            //Get Chargable
            foreach (List<MeterDataPersist> list in dataField)
            {
                var electricityList = list.Where(x => x.MeterType == MeterTypeEnum.Electricity);
                foreach (MeterDataPersist item in electricityList)
                {
                    item.RandomTimeMidNigh = date;
                    item.ForceData = true; //Force the out midNight hour for check cost normal
                    listChargable.Add(item.chargable);
                }
            }
            Assert.IsFalse(listChargable.SequenceEqual(listChargableAssert));
        }
    }
}
