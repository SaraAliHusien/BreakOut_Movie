using BreakOut_Movie.Models.Repo.Absrtraction;
using BreakOut_Movie.Setting;
using BreakOut_Movie.ViewModel.HomeViewModel;
using BreakOut_Movie.ViewModel.MovieViewModel;
using Microsoft.EntityFrameworkCore;

namespace BreakOut_Movie.Models.Repo.Implementation
{
    public class MovieRepo:IMovieRepo
    {
        private readonly BreakOut_DbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _FullVideosPath;
        public MovieRepo(BreakOut_DbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _FullVideosPath = $"{_webHostEnvironment.WebRootPath}{FileSetting.videosPath}";
        }
        public IEnumerable<HomeIndexVM> GetAll()
        {
            var movies= _context.Movies.Include(m=>m.Genre).
                AsNoTracking().ToList();
            List<HomeIndexVM> HomeMovies = new List<HomeIndexVM>();
            foreach (var movie in movies)
            {
                var homeMovie = new HomeIndexVM()
                {
                    MovieNoRate = movie,
                    TotalRate = GetTotalRateMovie(movie.Id),

                };
                HomeMovies.Add(homeMovie);
            }
            return HomeMovies;
        }

        public IEnumerable<UserHomeVM> GetAllForUser(string userId)
        {
            var movies = GetAll();
            List<UserHomeVM> UserHomeMovies = new List<UserHomeVM>();

            foreach (var movie in movies)
            {
                var homeMovie = new UserHomeVM()
                {
                    Movie = movie,
                    userRate = GetUserRate(movie.MovieNoRate.Id, userId),
                    IsFavorite=IsFavoriteMovie(movie.MovieNoRate.Id, userId)
                };
                UserHomeMovies.Add(homeMovie);
            }
            return UserHomeMovies;
        }
        public Movie? GetById(int id)
        {
            Movie? movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            return movie;
        }

        public async Task Insert(CreateFormVM VModel)
        {
       

            using var dataStream = new MemoryStream();
            await VModel.Poster.CopyToAsync(dataStream);
            Movie movie = new ()
            {
                Title = VModel.Title,
                Year = VModel.Year,
                Poster = dataStream.ToArray(),
                Cast=VModel.Cast,
                StoryLine = VModel.StoryLine,
                VideoName = await CreateVideoPathAsync(VModel.Video),
                GenreId = VModel.GenreId,

            };
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public async Task<bool> Update( EditFormVM newMovie)
        {
            Movie oldMovie = GetById(newMovie.Id)!;
           
            if(oldMovie == null)
                return false;
            
            oldMovie!.Title = newMovie.Title;
        
            oldMovie.StoryLine = newMovie.StoryLine;
            oldMovie.GenreId = newMovie.GenreId;
            oldMovie.Year = newMovie.Year;
            oldMovie.Cast = newMovie.Cast;
            string oldVideo= oldMovie.VideoName;
            if (newMovie.Video is not null)
            {
                oldMovie.VideoName = await CreateVideoPathAsync(newMovie.Video);
                var path = Path.Combine(_FullVideosPath, newMovie.CurrentVideoName!);
                File.Delete(path);
            }
            else
                oldMovie.VideoName = newMovie.CurrentVideoName!;
            
            if(newMovie.Poster is not null)
            {
                using var dataStream = new MemoryStream();
                await newMovie.Poster.CopyToAsync(dataStream);
                oldMovie.Poster= dataStream.ToArray();
            }
            else 
                oldMovie.Poster = newMovie.OldPoster!;
            

            var trackers= _context.ChangeTracker.Entries<Movie>().ToList();
            EntityState trackerState;
            if (trackers is not null)
            {
                trackerState = trackers[0].State;

                if(trackerState ==EntityState.Modified)
                {
                    var countRowEff = _context.SaveChanges();
                    if (countRowEff > 0)
                    {
                        if (newMovie.Video is not null)
                        {
                            var path = Path.Combine(_FullVideosPath, oldVideo);
                            File.Delete(path);
                        }
                        else if (oldVideo != newMovie.CurrentVideoName)
                        {
                            var path = Path.Combine(_FullVideosPath, oldVideo);
                            File.Delete(path);
                        }

                        return true;
                    }


                    if (newMovie.Video is not null)
                    {
                        var path = Path.Combine(_FullVideosPath, oldMovie.VideoName!);
                        File.Delete(path);
                    }
                    else if (oldVideo != newMovie.CurrentVideoName)
                    {
                        var path = Path.Combine(_FullVideosPath, newMovie.CurrentVideoName!);
                        File.Delete(path);
                    }
                    return false;
                }

            }

            return true;
           
        }
        public  bool Delete(int id)
        {
            Movie? movie = GetById(id);
            if (movie is null)
                return false;

            _context.Movies.Remove(movie);
           var rowEff= _context.SaveChanges();
            if(rowEff> 0)
            {
                DeleteFile(movie.VideoName);
                return true;

            }
            return false;
        }

        public bool AddRate(int userRate ,int movieId, string userId )
        {
            byte userRatebyByte= (byte)userRate;
           var existedRate= _context.Rates.Where(r => r.MovieId == movieId && r.UserId == userId).FirstOrDefault();
            if (existedRate is null)
            {
                Rate newRate = new Rate()
                {
                    RateValue = (byte)userRate,
                    UserId = userId,
                    MovieId= movieId

                };
                _context.Rates.Add(newRate);
               var rowEffected= _context.SaveChanges(); 
                return rowEffected>0? true:false;
            }
            else
            {
                if(existedRate.RateValue != userRatebyByte)
                {
                    existedRate.RateValue = userRatebyByte;
                    _context.Rates.Update(existedRate);
                    _context.Entry(existedRate).Property(r => r.MovieId).IsModified = false;
                    _context.Entry(existedRate).Property(r => r.UserId).IsModified = false;
                    var rowEffected = _context.SaveChanges();
                    return rowEffected > 0 ? true : false;
                }
                return true;
            }
            
            
        }
        public int GetTotalRateMovie(int movieId)
        {
            var rates = _context.Rates.Where(r => r.MovieId == movieId).ToList();
            if(rates.Any())
            {
                var TotalRate = _context.Rates.Where(r => r.MovieId == movieId).Average(r => r.RateValue);
                var rateByInt = (int)TotalRate;
                var x = TotalRate - rateByInt;
                if (x >= 0.5)
                    rateByInt++;
                return rateByInt;

            }
            return 0;
           
        }
        public int GetUserRate(int movieId, string userId)
        {
            var rate = _context.Rates.Where(r => r.MovieId == movieId&& r.UserId==userId).FirstOrDefault();
            if (rate is not null)
                return rate.RateValue;
           return 0;

        }
        public bool AddFavoriteMovie(int movieId,string userId)
        {
           var favoriteMovie= new FavoriteMovie() { MovieId= movieId, UserId = userId };
            _context.FavoriteMovies.Add(favoriteMovie);
            var rowEffected = _context.SaveChanges();
            return rowEffected > 0 ? true : false;
        }
        public bool RemoveFavoriteMovie(int movieId, string userId)
        {
            var favoriteMovie = _context.FavoriteMovies.Where(r => r.MovieId == movieId && r.UserId == userId).FirstOrDefault();
            if(favoriteMovie is null)
                return false;
            _context.FavoriteMovies.Remove(favoriteMovie);
            var rowEffected = _context.SaveChanges();
            return rowEffected > 0 ? true : false;
        }
        private bool IsFavoriteMovie(int movieId, string  userId) {
        var isFavorite= _context.FavoriteMovies.Where(r => r.MovieId == movieId && r.UserId == userId).FirstOrDefault();
           if(isFavorite is null)
                return false;
           return true;
        }
        public async Task<string> CreateVideoPathAsync(IFormFile file)
        {
            var videoFileExten = Path.GetExtension(file.FileName);
            var videoFileName = $"{Guid.NewGuid()}{videoFileExten}";
            var videoPath = Path.Combine(_FullVideosPath, videoFileName);

            using var stream = File.Create(videoPath);
            await file.CopyToAsync(stream);
            return videoFileName;

        }
        public void DeleteFile(string fileName)
        {
           var path = Path.Combine(_FullVideosPath, fileName);
            File.Delete(path);  
        }

    }
}
