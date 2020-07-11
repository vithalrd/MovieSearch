using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanFrancisco.Api.Movie.Profile
{
    /// <summary>
    /// Map repsonse movie object
    /// </summary>
    public class MovieProfile : AutoMapper.Profile
    {
        public MovieProfile()
        {
            CreateMap<Db.Movie, Models.Movie>();
        }
    }
}
