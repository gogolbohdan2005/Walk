using CarpatiansWalksAPI.Models;

namespace CarpatiansWalksAPI.Repository
{
    public interface IRegionRepository
    {
        public Task<List<Region>> GetRegionsAsync();
    }
}
