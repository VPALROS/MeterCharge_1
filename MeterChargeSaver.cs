using MeterCharge.BusinessLogic;
using MeterCharge.BussinesLogic.Entities;
using MeterCharge.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeterCharge.BussinesLogic
{
  // We are not sure if we want to save meterdata to text files going forward.
  public class MeterChargeSaver
  {
        readonly IPersistMeter _persistData;
        public MeterChargeSaver(IPersistMeter persist)
        {
            _persistData = persist;
        }
        public void CalculateChargeForMeterReadingsAndSave(IEnumerable<Meter> meters)
        {
            if (meters == null || !meters.Any())
            {
                throw new ArgumentException("The parameter 'meters' musn't be null");
            }

            List<MeterObjectPersist> list = GetData(meters);
            if (list.ToList().Any())
            {
                try
                {
                    PersistData(list);
                }catch(Exception ex)
                {
                    //TODO:Add log
                    throw new Exception("Tha data couldn't be persist");
                }
            }
        }       

        public List<MeterObjectPersist> GetData(IEnumerable<Meter> meters)
        {
            List<MeterObjectPersist> listMeterReader = new List<MeterObjectPersist>();
            foreach (var meter in meters.Where(x=>x.Id!=null))
            {
                List<MeterDataPersist> data = new List<MeterDataPersist>();
                
                foreach (var reading in meter.Readings)
                {                    
                    data.Add(new MeterDataPersist
                    {
                        MeterType = meter.MeterType,                        
                        Reading = reading                        
                    });

                }
                listMeterReader.Add(new MeterObjectPersist { Id = meter.Id, DataFields = data });
            }

            return listMeterReader;
        }
        public void PersistData(List<MeterObjectPersist> list)
        {
             _persistData.PersistData(list);
        }
       
    }

}
