using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanFrancisco.Api.Movie.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SanFrancisco.Api.Movie.Controllers
{
    /// <summary>
    /// Movie Controller
    /// </summary>
    [ApiController]
    [Route("api/searchmovie")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieProvider MovieProvider;
        public MoviesController(IMovieProvider movieProvider)
        {
            this.MovieProvider = movieProvider;
        }

        /// <summary>
        /// Search moview by Title, Director Actor, Year etc
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SearchMovieAsync(string searchText)
        {
            var result = await MovieProvider.SeachMovieAsync(searchText);
            if (result.IsSucess)
                return Ok(result.Movies);

            return NotFound();

        }

        //[HttpGet("{id}")]
       // public async Task<IActionResult> SearchMovieAsync(int id)
        
    }
}
