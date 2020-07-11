using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanFrancisco.Api.Movie.Db
{
    /// <summary>
    /// DB context for data
    /// </summary>
    public class MovieDBContext:DbContext
    {
        
        public DbSet<Movie> Movies { get; set; }

        public MovieDBContext(DbContextOptions options):base(options)
        {

        }
    }
}
