using CarpatiansWalksAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarpatiansWalksAPI.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string filterOn = null, string filterQuery = null, string sortBy = null, bool isDesending = true, int pageNum = 1, int pageSize = 10);
        Task<Walk> GetByIdAsync(int id);
        Task<Walk> UpdateAsync(int id, Walk walk);
        Task<Walk> DeleteAsync(int id);
    }
}
