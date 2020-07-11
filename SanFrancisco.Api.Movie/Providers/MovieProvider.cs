using AutoMapper;
using SanFrancisco.Api.Movie.Db;
using SanFrancisco.Api.Movie.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Transactions;

namespace SanFrancisco.Api.Movie.Providers
{
    /// <summary>
    /// Repository, to provide data for search
    /// </summary>
    public class MovieProvider : IMovieProvider
    {
        private readonly MovieDBContext dbContext;
        private readonly ILogger<Db.Movie> logger;
        private readonly IMapper mapper;
        public MovieProvider(MovieDBContext dbContext, ILogger<Db.Movie> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        public async Task<(bool IsSucess, IEnumerable<Models.Movie> Movies, string ErrMessage)> SeachMovieAsync(string searchText)
        {
            try
            {
                searchText = searchText?.ToLower();

                var movies = await dbContext.Movies
                      .Where(p => p.Actor_1.ToLower().Contains(searchText) || p.Actor_2.ToLower().Contains(searchText)
                    || p.Actor_2.ToLower().Contains(searchText) || p.Release_Year == searchText ||
                     p.Director.ToLower().Contains(searchText) || p.Locations.ToLower().Contains(searchText) ||
                     p.Title.ToLower().Contains(searchText) || p.Production_Company.ToLower().Contains(searchText))
                     .ToListAsync();


                if (movies != null && movies.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Movie>, IEnumerable<Models.Movie>>(movies);
                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
          
        }



        private void SeedData()
        {
            if (!dbContext.Movies.Any())
            {
                List<Db.Movie> movieList = new List<Db.Movie>();
                int rowId = 1;
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("https://data.sfgov.org/resource/yitu-d5am.json").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseStream = response.Content.ReadAsStringAsync().Result;
                    var movies = JsonConvert.DeserializeObject<List<Db.Movie>>(responseStream);
                    foreach (var movie in movies)
                    {
                        movie.MovieId = rowId;
                        movieList.Add(movie);
                        rowId++;
                    }
                    dbContext.AddRange(movieList);
                    dbContext.SaveChanges();

                }
            }
        }

    }
}
