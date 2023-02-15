using MeterCharge.BusinessLogic;
using MeterCharge.BussinesLogic.Utils.Files;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterCharge.UnitTest
{
    public class DependencyInjectionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPersistMeter>().To<WriteMeterFiles>();
        }
    }
}
