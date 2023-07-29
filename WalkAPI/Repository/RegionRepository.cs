using CarpatiansWalksAPI.Data;
using CarpatiansWalksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarpatiansWalksAPI.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly CarpatianWalksDbContext dbContext;

        public RegionRepository(CarpatianWalksDbContext dbContext)
        {
            this.dbContext = dbContext; // CONSTRUCTOR I SUPPOSE
        }
        
        public async Task<List<Region>> GetRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }
    }
}
