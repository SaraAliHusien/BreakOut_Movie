using BreakOut_Movie.Models;
using BreakOut_Movie.Models.Repo.Absrtraction;
using BreakOut_Movie.ViewModel.GenreViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BreakOut_Movie.Controllers
{
    [Authorize(Roles = "Admin,MainAdmin")]
    public class GenreController : Controller
    {
        private readonly IGenreRepo _genreRepo;
       
        private readonly IToastNotification _toasNotification;
        public GenreController(IGenreRepo genreRepo , IToastNotification toastNotification)
        {
            _genreRepo = genreRepo;
          _toasNotification = toastNotification;    

        }
        public IActionResult Index()
        {
            List<Genre> genres=_genreRepo.GetAll();
            return View(genres);
        }

        public IActionResult Create()
        {
            return View("CreateGenre");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGenreVm model)
        {
            if(!ModelState.IsValid) {
                return View("CreateGenre", model);
            }
            await _genreRepo.InsertAsync(model);
            _toasNotification.AddSuccessToastMessage("The Genre Added Successfully");
            return RedirectToAction("Index","Genre");
        }
        public IActionResult Edit(int? id)
        {  if (id == null)
                return BadRequest();
            Genre? genre = _genreRepo.GetById(id.Value);
            if (genre == null)
                return NotFound();

            EditGenreVm model= new () { Id=genre.Id,Name=genre.Name,currentImage=genre.Image};
            return View("EditGenre", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(EditGenreVm model) { 
        if(!ModelState.IsValid) {
                return View("EditGenre", model);
            }
           Genre? genre=_genreRepo.GetById(model.Id);
            if (genre == null)
                return NotFound();
            
            var result=await _genreRepo.UpdateAsync( model);
            if(!result)
                return BadRequest();
            _toasNotification.AddSuccessToastMessage("Genre Updated Successfully");
            return RedirectToAction("Index","Genre");   
        
        }

    
        public IActionResult Delete(int id)
        {
            
            Genre? genre = _genreRepo.GetById(id);
            if (genre == null)
                return NotFound();
            var result =_genreRepo.Delete(id);
            
            return result?Ok():BadRequest();
        }

    }
}
