using BOL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{

    public interface IMetaDataService
    {
        IEnumerable<MetaData> GetAllMetaData();
        IEnumerable<MetaData> GetMetaDataById(int Id);
        bool Create(MetaData metadata);
        IEnumerable<object> GetAllStats();
    }


    public class MetaDataService : IMetaDataService
    {
        IMovieDB MovieDB;
        IStatsDB  StatsDB;

        public MetaDataService(IMovieDB _MovieDB, IStatsDB _StatsDB )
        {
            MovieDB = _MovieDB;
            StatsDB = _StatsDB;
        }

        public bool Create(MetaData metadata)
        {
            return  MovieDB.Create(metadata);
            
        }

        public IEnumerable<MetaData> GetAllMetaData()
        {
            var metadatas = MovieDB.GetAll();
            return metadatas;
        }

        public IEnumerable<MetaData> GetMetaDataById(int Id)
        {
            var metadatas = MovieDB.GetMetaDataById(Id).OrderBy(x=>x.ReleaseYear);
            
            var LatestMovies = metadatas.GroupBy(x => new { x.Id, x.MovieId , x.Language } ).Select(
                cl => new
                {
                    MovieId = cl.First().MovieId,
                    Language = cl.First().Language,
                    NumRecords = cl.Select(l => l.MovieId).Count()                    
                });
            
             var returnval = LatestMovies.Select(x => new MetaData
            {
                MovieId = x.MovieId,
                Title = metadatas.Where(c => c.MovieId == x.MovieId).LastOrDefault().Title,
                Duration = metadatas.Where(c => c.MovieId == x.MovieId).LastOrDefault().Duration,
                ReleaseYear = metadatas.Where(c => c.MovieId == x.MovieId).LastOrDefault().ReleaseYear,
                Language = x.Language
            }).OrderBy(c => c.ReleaseYear);

            return returnval;
        }

        //
        public IEnumerable<object> GetAllStats( )
        {
            var metadatas = MovieDB.GetAll().ToList().GroupBy( x=>x.MovieId ).Select(
                cl => new MetaData()
                {
                    MovieId = cl.First().MovieId,
                    Title = cl.First().Title,
                    ReleaseYear = cl.First().ReleaseYear  
                } ).ToList() ;

            var stats = StatsDB.GetAllStats().GroupBy(x => x.MovieId).Select(cl => new 
            {
                MovieId = cl.First().MovieId,
                AverageWatchDurations =  Convert.ToInt32(cl.Sum(c => ( c.WatchDurationMs)/ 1000)),
                Watches = cl.Count()

            }).ToList();

            var metastats = stats.Select(x => new  
            {
                MovieId = x.MovieId,
                Title = metadatas.Where(a => a.MovieId == x.MovieId).Count() > 0 ? metadatas.Where(c => c.MovieId == x.MovieId)?.FirstOrDefault().Title : null,
                Watches = x.Watches ,
                ReleaseYear = metadatas.Where(a => a.MovieId == x.MovieId).Count() > 0 ? metadatas.Where(c => c.MovieId == x.MovieId).FirstOrDefault().ReleaseYear : 0,
                AverageWatchDurations  = x.AverageWatchDurations,
                
            }).Where(x=>!string.IsNullOrEmpty(x.Title)).OrderBy( x=>x.ReleaseYear).ToList();

            return metastats;
        }



       

        
    }
}
