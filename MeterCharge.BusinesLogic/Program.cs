using MeterCharge.BusinessLogic;
using MeterCharge.Entity;
using MeterCharge.Entity.Type;
using Ninject;
using System;
using System.Collections.Generic;

namespace MeterCharge.BussinesLogic
{
  class Program
  {
        static void Main(string[] args)
        {
            StandardKernel _kernal = new StandardKernel();
            _kernal.Load(System.Reflection.Assembly.GetExecutingAssembly());
            IPersistMeter _persist = _kernal.Get<IPersistMeter>();

            var m1 = new Meter { Id = Guid.NewGuid().ToString(), MeterType = MeterTypeEnum.Electricity, Readings = new List<int> { 97, 50 } };
            var m2 = new Meter { Id = Guid.NewGuid().ToString(), MeterType = MeterTypeEnum.Heating, Readings = new List<int> { 55, 87 } };
            var m3 = new Meter { Id = Guid.NewGuid().ToString(), MeterType = MeterTypeEnum.Water, Readings = new List<int> { 98, 86 } };

            var list1 = new List<Meter> { m1, m2, m3 };
            new MeterChargeSaver(_persist).CalculateChargeForMeterReadingsAndSave(list1);
           
        }
    }
}