using CarpatiansWalksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarpatiansWalksAPI.Data
{
    public class CarpatianWalksDbContext : DbContext
    {
        public CarpatianWalksDbContext(DbContextOptions dbContextOptions) : base (dbContextOptions)
        {
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Region> regions = new List<Region>()
            {
                new Region()
                {
                    Id = 1,
                    Name = "Lazeshchyna (Yasinna)",
                    HowToGetHere = "Using train from Lviv or Ivano-Frankivsk is very conveniet and preferable",
                    Places="You can get to mountais Kukul, Petros, Drahobrat, it`s waterfall, lake Ivor and waterfall Trufanets, "
                },
                new Region()
                {
                    Id = 2,
                    Name = "Dzembronya",
                    HowToGetHere = "Using regular (12:00, 15:00 am) bus from Verhovyna, Verhovyna can be reached from Ivano-Frankivsk using bus",
                    Places = "Able to reach awesome mountain Pip Ivan, Dzembronya and it`s waterfall and even second biggest mountain -- Brebenescyl"
                },
                new Region()
                {
                    Id = 3,
                    Name = "Zaroslak",
                    HowToGetHere = "Using train or bus to Vorohta, then catching taxi or unregular bus to base Zaroslak",
                    Places = "Visit Hoverla, polonuna Pozhyzhevska, lake Nesamovute, mountains Rebra, Hytun-Tomnatyk, Shputzi"
                },
                new Region()
                {
                    Id = 4,
                    Name = "Yaremche",
                    HowToGetHere = "Using bus from IvanoFrankivsk or train from Frankivsk, Lviv, other cities",
                    Places = "You can visit various waterfalls, old aqueducs, polonynas and small mountain"
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
