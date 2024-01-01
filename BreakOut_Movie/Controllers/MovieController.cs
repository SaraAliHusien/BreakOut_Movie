using BreakOut_Movie.Models;
using BreakOut_Movie.Models.Repo.Absrtraction;
using BreakOut_Movie.ViewModel.MovieViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NToastNotify;

namespace BreakOut_Movie.Controllers
{
    [Authorize(Roles ="Admin,MainAdmin")]
    public class MovieController : Controller
    {
      
        private readonly IToastNotification _toasNotification;
        private readonly IMovieRepo _movieRepository;
        private readonly IGenreRepo _genreRepository;
        public MovieController( IToastNotification toasNotification, IGenreRepo genreRepository,IMovieRepo movieRepository
           )
        {
            _toasNotification = toasNotification;
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;

        }
        public IActionResult Index()
        {
          var movies = _movieRepository.GetAll();

            return View(movies);
        }
        
        public IActionResult Create()
        {
            var viewModel = new CreateFormVM()
            {
                Genres = _genreRepository.GetAll(),
            };
            return View("CreateMovieForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFormVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _genreRepository.GetAll();
                return View("CreateMovieForm", model);
            }
          
            await _movieRepository.Insert(model);

            _toasNotification.AddSuccessToastMessage("Movie created successfully");
            return RedirectToAction(nameof(Index),"Movie");
        }

       
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var movie = _movieRepository.GetById(id.Value);
            if (movie == null)
                return NotFound();

            var viewModel = new EditFormVM
            {
                Id = movie.Id,
                Title = movie.Title,
                GenreId = movie.GenreId,
                //Rate = movie.Rate,
                Year = movie.Year,
                StoryLine = movie.StoryLine,
                OldPoster = movie.Poster,
                CurrentVideoName = movie.VideoName,
                Genres = _genreRepository.GetAll()

            };

            return View("EditMovieForm", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFormVM model)
        {

            var movie = _movieRepository.GetById(model.Id);
            if (movie == null)
                return NotFound();

            if (!ModelState.IsValid)
            { 
                if(model.Video is not null)
                {
                    if (ModelState["Video"]!.ValidationState==ModelValidationState.Valid)
                    {
                        if (model.CurrentVideoName != movie.VideoName)
                        {
                            _movieRepository.DeleteFile(model.CurrentVideoName!);
                        }
                        model.CurrentVideoName = await _movieRepository.CreateVideoPathAsync(model.Video);
                    }
                        
                }
                model.Genres = _genreRepository.GetAll();
                return View("EditMovieForm", model);
            }

            

            var result= await _movieRepository.Update(model);
            if(!result)
                return BadRequest();

            _toasNotification.AddSuccessToastMessage("Movie updated successfully");
            return RedirectToAction(nameof(Index), "Movie");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            Movie? movie = _movieRepository.GetById(id.Value);
            if (movie == null)
                return NotFound();
           var result= _movieRepository.Delete(id.Value);
            return result? Ok():BadRequest();
        }



    }
}
