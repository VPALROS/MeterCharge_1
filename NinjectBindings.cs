using MeterCharge.BussinesLogic.Utils.Files;

namespace MeterCharge.BusinessLogic
{
    public class NinjectBindings :Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IPersistMeter>().To<WriteMeterFiles>();
        }
    }
}
