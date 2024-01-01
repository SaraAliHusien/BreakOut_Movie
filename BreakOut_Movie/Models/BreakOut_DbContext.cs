using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BreakOut_Movie.Models
{
    public class BreakOut_DbContext:IdentityDbContext<ApplicationUser>
    {
        public BreakOut_DbContext(DbContextOptions<BreakOut_DbContext> options)
    : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movie>().HasOne(m => m.Genre).WithMany().HasForeignKey(m=>m.GenreId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Rate>(
               Er => Er.HasKey(rates => new { rates.MovieId, rates.UserId })
               );
            builder.Entity<FavoriteMovie>(
              Er => Er.HasKey(rates => new { rates.MovieId, rates.UserId })
              );
            builder.Entity<Genre>()
             .HasIndex(g => g.Name)
            .IsUnique();

            base.OnModelCreating(builder);
        }
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<Rate> Rates { get; set; } = default!;
        public DbSet<FavoriteMovie> FavoriteMovies { get; set; } = default!;


    }
}
