using BreakOut_Movie.ViewModel.HomeViewModel;
using BreakOut_Movie.ViewModel.MovieViewModel;
using Microsoft.EntityFrameworkCore;

namespace BreakOut_Movie.Models.Repo.Absrtraction
{
    public interface IMovieRepo
    {
        public IEnumerable<HomeIndexVM> GetAll();
        public IEnumerable<UserHomeVM> GetAllForUser(string userId);
        public Movie? GetById(int id);
        public Task Insert(CreateFormVM VModel);
        public Task<bool> Update( EditFormVM newMovie);
        public  bool Delete(int id);
        public bool AddRate(int userRate, int movieId, string userId);
        public int GetTotalRateMovie(int movieId);
        public int GetUserRate(int movieId,string userId);
        public bool AddFavoriteMovie(int movieId, string userId);
        public bool RemoveFavoriteMovie(int movieId, string userId);

        Task<string> CreateVideoPathAsync(IFormFile file);
        public void DeleteFile(string fileName);
    }
}
