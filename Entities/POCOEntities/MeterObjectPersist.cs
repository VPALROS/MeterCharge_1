using System;
using System.Collections.Generic;

namespace MeterCharge.BussinesLogic.Entities
{
   public class MeterObjectPersist
    {
        public string Id { get; set; }
        public List<MeterDataPersist> DataFields { get; set; }
    }
}
