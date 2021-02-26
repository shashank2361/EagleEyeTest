using BOL.Models;
using CsvHelper.Configuration;
 

namespace DAL
{
    public sealed  class MovieMap : ClassMap <MetaData>
    {
        public MovieMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.MovieId).Name("MovieId");
            Map(x => x.Title).Name("Title");
            Map(x => x.Language).Name("Language");
            Map(x => x.Duration).Name("Duration");
            Map(x => x.ReleaseYear).Name("ReleaseYear");
        }
    }

    public sealed class StatsMap : ClassMap<Stats>
    {
        public StatsMap()
        {
            Map(x => x.MovieId).Name("movieId");
            Map(x => x.WatchDurationMs).Name("watchDurationMs");
        }
    }
}

 