using MeterCharge.BusinessLogic;
using MeterCharge.BussinesLogic.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace MeterCharge.BussinesLogic.Utils.Files
{
     public class WriteMeterFiles: IPersistMeter
    {       
        /// <summary>
        /// Create file on path informed in app.config
        /// </summary>
        /// <param name="list"></param>
        private  void CreateFile(List<MeterObjectPersist> list)
        {
            var path= ConfigurationManager.AppSettings["FilePath"];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach(MeterObjectPersist meter in list)
            {
                var filename = "Meter" + meter.Id + ".log";
                StreamWriter writer = File.AppendText(path + filename);
                WriteLines(writer,meter.DataFields);
            }           
            
        }

        /// <summary>
        /// Write each line to File
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="meter"></param>
        private  void WriteLines(StreamWriter writer, List<MeterDataPersist> meter)
        {
            //Header
            writer.Write("Timestamp".PadRight(20, ' ') + "\t" + "Meter Type".PadRight(15, ' ') + "\t" + "Consumption".PadRight(15, ' ') + "\t" + "Cost".PadRight(15, ' ') + "\t" + "Charge".PadRight(15, ' ') + "\t" + Environment.NewLine);
            writer.AutoFlush = true;

            //Body
            StringBuilder sb = new StringBuilder();
            foreach (MeterDataPersist item in meter)
            {                
                sb.Append(item.Date);
                sb.Append("\t");
                sb.Append(item.MeterType.ToString().PadRight(15, ' '));
                sb.Append("\t");
                sb.Append(item.Cost.ToString().PadRight(15, ' '));
                sb.Append("\t");
                sb.Append(item.Reading.ToString().PadRight(15, ' '));
                sb.Append("\t");
                sb.Append(item.Chargable.ToString().PadRight(15, ' '));
                sb.Append("\t");
                sb.Append(Environment.NewLine);
            }            

            writer.Write(sb.ToString());
        }

        public void PersistData(List<MeterObjectPersist> list)
        {
            CreateFile(list);
        }
    }
}
