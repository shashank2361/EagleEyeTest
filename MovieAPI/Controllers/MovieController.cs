using BLL;
  
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    //[Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMetaDataService metaDataServices;
         public MovieController(IMetaDataService _metaDataServices )
        {
            metaDataServices = _metaDataServices;
           
        }
        [HttpGet("GetAllMetadata")]

        public IActionResult GetAllMetadata()
        {
            var response = metaDataServices.GetAllMetaData();
             return Ok(response);
        }
        
    
        [HttpGet("metadata/:{id}")]

        public IActionResult Get(int id)
        {
       
            var response = metaDataServices.GetMetaDataById(id) ;
            if (response.Count() == 0)
            {
                 return NotFound(); 
            }
            return Ok(response)  ;
        }


 
       [HttpGet("movies/stats")]

        public IActionResult GetMoviesStats()
        {
            var response = metaDataServices.GetAllStats();
            if (response.Count() == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }




        [HttpPost("metadata")]
        public IActionResult Create([FromBody] MetaData  metadata)
        {
            var Metadata = new BOL.Models.MetaData()
            {
                Id = metadata.Id,
                MovieId = metadata.MovieId,
                Duration = metadata.Duration,
                Title = metadata.Title,
                Language = metadata.Language,
                ReleaseYear = metadata.ReleaseYear,
            };
            
            var response = metaDataServices.Create(Metadata) ;
            return Ok(response);
        }

    }
}
