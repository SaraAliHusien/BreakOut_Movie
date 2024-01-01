using BreakOut_Movie.Models;
using BreakOut_Movie.Models.Repo.Absrtraction;
using BreakOut_Movie.Setting;
using BreakOut_Movie.ViewModel.HomeViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BreakOut_Movie.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMovieRepo _movieRepository;
        public HomeController(IMovieRepo movieRepository )
        {
            _movieRepository = movieRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("UserHome");
            
            var movies = _movieRepository.GetAll();
           
            return View(movies);
        }
       
        public IActionResult UserHome()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return BadRequest();
            var movies = _movieRepository.GetAllForUser(userId);
            return View(movies); 
        }
        public IActionResult DisplayVideo(int? id)
        {
            if (id == null) return BadRequest();
            var movie = _movieRepository.GetById(id.Value);
            if (movie == null)
                return NotFound();
            return PartialView(movie);
        }
     
        public IActionResult Download(int? id)
        {
            if (id is null)
                return BadRequest();

            Movie? movie = _movieRepository.GetById(id.Value);
            if (movie is null)
                return NotFound();
            var path = Path.Combine(FileSetting.videosPath, movie.VideoName);
            var ext = Path.GetExtension(movie.VideoName);
            return File(path, $"videos/{ext}", $"{movie.Title}{ext}");
        }

        public IActionResult AddRate(int userRate, int movieId)
        {
            if (userRate > 0 && movieId > 0)
            {
                var movie = _movieRepository.GetById(movieId);
                if (movie is null)
                    return BadRequest();
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    var result = _movieRepository.AddRate(userRate, movieId, userId);
                    return result ? Ok() : BadRequest();
                }
            }
            return BadRequest();
        }
        public IActionResult AddFavoriteMovie( int movieId)
        {
            if ( movieId > 0)
            {
                var movie = _movieRepository.GetById(movieId);
                if (movie is null)
                    return BadRequest();
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    var result = _movieRepository.AddFavoriteMovie( movieId, userId);
                    return result ? Ok() : BadRequest();
                }
            }
            return BadRequest();
        }

        public IActionResult RemoveFavoriteMovie(int movieId)
        {
            if (movieId > 0)
            {
                var movie = _movieRepository.GetById(movieId);
                if (movie is null)
                    return BadRequest();
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    var result = _movieRepository.RemoveFavoriteMovie(movieId, userId);
                    return result ? Ok() : BadRequest();
                }
            }
            return BadRequest();
        }
    }
}