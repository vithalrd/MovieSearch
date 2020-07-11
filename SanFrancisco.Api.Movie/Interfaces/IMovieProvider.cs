using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanFrancisco.Api.Movie.Interfaces
{
    public interface IMovieProvider
    {
        Task<(bool IsSucess, IEnumerable<Models.Movie> Movies, string ErrMessage)> SeachMovieAsync(string searchText);
    }
}
