using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext (DbContextOptions<GameStoreContext> options) : DbContext (options) //a db context represents a session with the database and can be used to query and save instances of your entities
{

public DbSet<Game> Games => Set<Game>();

public DbSet<Genre> Genres=> Set<Genre>();

}
