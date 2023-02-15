using MeterCharge.Entity.Type;
using System.Collections.Generic;

namespace MeterCharge.Entity
{
    public class Meter
    {
        public string Id { get; set; }
        public MeterTypeEnum MeterType { get; set; }
        public IEnumerable<int> Readings { get; set; }
    }
}