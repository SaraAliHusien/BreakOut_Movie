using BreakOut_Movie.Models.Repo.Absrtraction;
using BreakOut_Movie.ViewModel.GenreViewModel;
using Microsoft.EntityFrameworkCore;

namespace BreakOut_Movie.Models.Repo.Implementation
{
    public class GenreRepo:IGenreRepo
    {
        private readonly BreakOut_DbContext _Context;
        public GenreRepo(BreakOut_DbContext context)
        {
            _Context = context;
        }
        public List<Genre> GetAll()
        {
            return _Context.Genres.OrderByDescending(g => g.Name).AsNoTracking().ToList();

        }
        public Genre? GetById(int id)
        {
            return _Context.Genres.FirstOrDefault(g=> g.Id==id);
        }
       public async Task InsertAsync(CreateGenreVm model)
        {
            using var dataStream = new MemoryStream();
            await model.Image.CopyToAsync(dataStream);

            Genre genre = new () {Name=model.Name, Image=dataStream.ToArray()};  
            _Context.Genres.Add(genre);
            _Context.SaveChanges();
        }

        public async Task<bool> UpdateAsync(EditGenreVm newGenre)
        {
            Genre? oldGenre = GetById(newGenre.Id);
            if(oldGenre is null) 
                return false;

            oldGenre.Name = newGenre.Name;
            if(newGenre.Image is  null)
            {
                oldGenre.Image = newGenre.currentImage!;
            }
            else
            {
                using var dataStream = new MemoryStream();
                await newGenre.Image.CopyToAsync(dataStream);
                oldGenre.Image=dataStream.ToArray();
            }
            var trackers = _Context.ChangeTracker.Entries<Genre>().ToList();
            EntityState trackerState;
            if (trackers != null)
            {
                trackerState = trackers[0].State;

                if (trackerState == EntityState.Modified)
                {
                    var rowEff=_Context.SaveChanges();
                    if(rowEff == 0)
                        return false;  
                }
                   
            }
            return true;
        }
        public bool Delete(int id)
        {

            Genre? genre = GetById(id);
            if(genre is null) 
            return false;
           
            var movies= _Context.Movies.Where(m => m.GenreId == id).FirstOrDefault();
            if(movies is not null)
            {
                return false;
            }
            _Context.Genres.Remove(genre);
            var rowEffected=_Context.SaveChanges();
            if(rowEffected>0)
                return true;    
            return false;   

        }
    }
}
