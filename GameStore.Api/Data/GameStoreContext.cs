using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext (DbContextOptions<GameStoreContext> options) 
: DbContext (options) //a db context represents a session with the database and can be used to query and save instances of your entities
{
public DbSet<Game> Games => Set<Game>();
public DbSet<Genre> Genres=> Set<Genre>();
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name = "Action RPG"},
            new {Id = 2, Name = "MOBA"},
            new {Id = 3, Name = "First-Person Shooter"},
            new {Id = 4, Name = "Racing"},
            new {Id = 5, Name = "Puzzle"},
            new {Id = 6, Name = "Adventure"}
        );
    }

}
