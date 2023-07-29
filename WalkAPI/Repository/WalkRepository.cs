using CarpatiansWalksAPI.Data;
using CarpatiansWalksAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarpatiansWalksAPI.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly CarpatianWalksDbContext dbContext;

        public WalkRepository(CarpatianWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(int id)
        {
            var deleteWalk = await dbContext.Walks.FirstOrDefaultAsync(obj => obj.Id == id);
            if (deleteWalk == null)
            {
                return null;
            }

            dbContext.Remove(deleteWalk);
            await dbContext.SaveChangesAsync();
            return deleteWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string filterOn = null, string filterQuery = null, string sortBy = null, bool isDesending = true, int pageNum =  1, int pageSize = 10)
        {
            var walks = dbContext.Walks.Include("Region").AsQueryable();

            // FILTERING
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("name", StringComparison.OrdinalIgnoreCase))
                {  
                    walks = walks.Where(m => m.Name.Contains(filterQuery));
                }
                
            }

            // SORTING
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Distance", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isDesending ? walks.OrderByDescending(m => m.Distance) : walks.OrderBy(m => m.Distance);
                }
                else if (sortBy.Equals("MaxHeight", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isDesending ? walks.OrderByDescending(m => m.MaxHeight) : walks.OrderBy(m => m.MaxHeight);
                }
            }

            var skip = (pageNum - 1) * pageSize; // HOW MUCH TO SKIP
            return await walks.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(int id)
        {
            return await dbContext.Walks.Include("Region").FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<Walk> UpdateAsync(int id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.Include("Region").FirstOrDefaultAsync(obj => obj.Id == id);

            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Region = walk.Region;
            existingWalk.Name = walk.Name;
            existingWalk.MaxHeight = walk.MaxHeight;

            existingWalk.RegionId = walk.RegionId;
            existingWalk.Distance = walk.Distance;
            existingWalk.JsonMoutains = walk.JsonMoutains;
            existingWalk.JsonImageURLs = walk.JsonImageURLs;
            existingWalk.Distance = existingWalk.Distance;
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
