using BOL.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface IMovieDB  
    {
        IEnumerable<MetaData> GetAll();
        IEnumerable<MetaData> GetMetaDataById(int id);
        bool Create(MetaData metadata);


    }
    public class MovieDB : IMovieDB  
    {
        string MetadataFile = Path.GetFullPath("../DAL/Files/metadata.csv");
        private IGenericFileRepository<MetaData> GenericFileRepository;

        public MovieDB(IGenericFileRepository<MetaData> GenericFileRepository)
        {
            this.GenericFileRepository = GenericFileRepository;
        }

        public IEnumerable<MetaData> GetAll()
        {
            return GenericFileRepository.ReadCSVFile(MetadataFile) ;
        }



        public IEnumerable<MetaData> GetMetaDataById(int id)
        {

            var response = GenericFileRepository.ReadCSVFile(MetadataFile).Where(x => x.MovieId == id);
            return response;
        }


        public bool Create(MetaData metadata)
        {
            return GenericFileRepository.WriteCSVFile(MetadataFile, metadata); ;
        }

         


        //public List<MetaData> ReadCSVFile()
        //{
        //    try
        //    {
        //        using (var reader = new StreamReader(MetadataFile, new UTF8Encoding(true)))
        //        using (CsvReader csv = new CsvReader(reader))
        //        {
        //            csv.Configuration.RegisterClassMap <MovieMap>();

        //            var records = csv.GetRecords<MetaData>();

        //            return records.ToList()  ;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}



        //public void WriteRangeCSVFile(string path, List<MetaData> metaData)
        //{
        //    using (StreamWriter sw = new StreamWriter(path, true, new UTF8Encoding(true)))
        //    using (CsvWriter cw = new CsvWriter(sw))
        //    {
        //        //cw.WriteHeader<MetaData>();
        //        cw.NextRecord();
        //        foreach (MetaData mdt in metaData)
        //        {
        //            cw.WriteRecord<MetaData>(mdt);
        //            cw.NextRecord();
        //        }
        //    }
        //}



        //public bool WriteCSVFile( MetaData  metaData)
        //{
        //    using (StreamWriter sw = new StreamWriter(MetadataFile, true, new UTF8Encoding(true)))
        //    using (CsvWriter cw = new CsvWriter(sw))
        //    {
        //        //cw.WriteHeader<MetaData>();
        //        cw.NextRecord();
        //        cw.WriteRecord<MetaData>(metaData);
        //    }
        //    return true;
        //}


    }
}
