using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface IGenericFileRepository<T> where T : class
    {
        List<T> ReadCSVFile(string file);
        void WriteRangeCSVFile(string file, List<T> t);
         bool WriteCSVFile(string file, T t);
     }


    public class GenericFileRepository<T > : IGenericFileRepository<T  > where T : class   
    {
        public List<T> ReadCSVFile(string file)
        {
            try
            {
                using (var reader = new StreamReader(file, new UTF8Encoding(true)))
                using (CsvReader csv = new CsvReader(reader))
                {

                    if (file.ToLower().EndsWith("stats.csv"))
                    {
                        csv.Configuration.RegisterClassMap<StatsMap>();


                    }
                    if (file.ToLower().EndsWith("metadata.csv"))
                    {
                        csv.Configuration.RegisterClassMap<MovieMap>();

                    }

                    var records = csv.GetRecords<T>();

                    return records.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



         }

        public bool WriteCSVFile(string file,  T t)
        {
            using (StreamWriter sw = new StreamWriter(file, true, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw))
            {
                //cw.WriteHeader<MetaData>();
                cw.NextRecord();
                cw.WriteRecord<T>(t);
            }
            return true;
        }




        public void WriteRangeCSVFile(string file, List<T> t)
        {
            using (StreamWriter sw = new StreamWriter(file, true, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw))
            {
                //cw.WriteHeader<MetaData>();
                cw.NextRecord();
                foreach (T mdt in t)
                {
                    cw.WriteRecord<T>(mdt);
                    cw.NextRecord();
                }
            }
        }
    }



}
