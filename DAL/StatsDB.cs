using BOL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface IStatsDB
    {
        IEnumerable<Stats> GetAllStats();
        IEnumerable<Stats> GetStatsDataById(int id);
        
    }

    public class StatsDB : IStatsDB
    {
        string StatsdataFile = Path.GetFullPath("../DAL/Files/stats.csv");
        private IGenericFileRepository<Stats> GenericFileRepository;

        public StatsDB(IGenericFileRepository<Stats> GenericFileRepository)
        {
            this.GenericFileRepository = GenericFileRepository;
        }
        public IEnumerable<Stats> GetAllStats()
        {

            return GenericFileRepository.ReadCSVFile(StatsdataFile);
        }

        public IEnumerable<Stats> GetStatsDataById(int id)
        {
            var response = GenericFileRepository.ReadCSVFile(StatsdataFile).Where(x => x.MovieId == id);
            return response;
        }
    }
}
