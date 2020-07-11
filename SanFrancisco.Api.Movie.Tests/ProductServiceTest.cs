using AutoMapper;
using SanFrancisco.Api.Movie.Db;
using SanFrancisco.Api.Movie.Profile;
using SanFrancisco.Api.Movie.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace SanFrancisco.Api.Movie.Tests
{
    public class MovieServiceTest
    {

        [Fact]
        public async Task GetMoviesBySearchText()
        {
            var options = new DbContextOptionsBuilder<MovieDBContext>().UseInMemoryDatabase(nameof(GetMoviesBySearchText)).Options;
            var dbContext = new MovieDBContext(options);
            CreateProducts(dbContext);

            var productProfile = new MovieProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var movieProvider = new MovieProvider(dbContext, null, mapper);

            var result = await movieProvider.SeachMovieAsync("Siddarth");
            Assert.True(result.IsSucess);
        }

        private void CreateProducts(MovieDBContext dbContext)
        {
                dbContext.Movies.Add(new Db.Movie
                {
                    MovieId =1,
                     Title =  "180",
                    Release_Year = "2011",
                    Locations= "Randall Museum",
                    Production_Company =  "SPI Cinemas",
                    Director = "Jayendra",
                    Writer= "Hemant",
                    Actor_1 = "Siddarth",
                    Actor_2 = "Nithya Menon",
                    Actor_3 = "Priya Anand"
                });
                dbContext.SaveChanges();
            dbContext.Movies.Add(new Db.Movie
            {
                MovieId = 2,
                Title = "180",
                Release_Year = "2012",
                Locations = "Randall Museum",
                Production_Company = "SPI Cinemas",
                Director = "Rajendra",
                Writer = "Hemant",
                Actor_1 = "Siddarth",
                Actor_2 = "Anushka",
                Actor_3 = "Urmila"
            });
            dbContext.SaveChanges();
            dbContext.Movies.Add(new Db.Movie
            {
                MovieId = 3,
                Title = "180",
                Release_Year = "2013",
                Locations = "Singapore",
                Production_Company = "SPI Cinemas",
                Director = "Upendra",
                Writer = "Ramakant",
                Actor_1 = "Anil Kapur",
                Actor_2 = "Katrina",
                Actor_3 = "Tapsee"
            });
            dbContext.SaveChanges();
        }
    }
}
