using BreakOut_Movie.ViewModel.GenreViewModel;

namespace BreakOut_Movie.Models.Repo.Absrtraction
{
    public interface IGenreRepo
    {
        List<Genre> GetAll();
        Genre? GetById(int id);
        Task InsertAsync(CreateGenreVm genre);
        Task<bool> UpdateAsync(EditGenreVm newGenre);
        bool Delete(int id);
    }
}
