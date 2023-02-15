using MeterCharge.BussinesLogic.Entities;
using System.Collections.Generic;

namespace MeterCharge.BusinessLogic
{
    public interface IPersistMeter
    {
        void PersistData(List<MeterObjectPersist> list);
    }
}
