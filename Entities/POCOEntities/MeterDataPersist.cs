
using MeterCharge.Entity.Type;
using System;

namespace MeterCharge.BussinesLogic.Entities
{
    public class MeterDataPersist
    {
        public int cost;
        public int chargable;
        public int reading;
        public bool forceMidNight;
        public bool ForceData {
            get { return forceMidNight; }
            set
            {
                forceMidNight = value;
                cost = SetCost();
                chargable = SetChargable();
            }
        }
        public DateTime RandomTimeMidNigh { get; set; }
        public string Date
        {
            get => System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:SS");           
        }       
        public MeterTypeEnum MeterType { get; set; }
        
        public int Cost {
            get { return cost; }
            set { cost = value; }
        }
        public int Reading {
            get { return reading; }
            set { reading = value; }
        }
        public int Chargable
        {
            get { return chargable; }
            set { chargable = SetChargable(); }
        }


        public int SetCost()
        {
            int cost = 0;
            DateTime dateTime = ForceData? RandomTimeMidNigh : DateTime.Now;
            switch (this.MeterType)
            {
                case MeterTypeEnum.Electricity: cost = (dateTime.TimeOfDay.Hours < 8 || dateTime.TimeOfDay.Hours > 20) ? 2 : 4; break;
                case MeterTypeEnum.Heating: cost = 5; break;
                case MeterTypeEnum.Water: cost = 3; break;
            }
            return cost;
        }


        public int SetChargable()
        {
            return cost* reading;
        }
    }

    
}
