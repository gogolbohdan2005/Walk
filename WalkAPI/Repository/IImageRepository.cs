using CarpatiansWalksAPI.Models;

namespace WalkAPI.Repository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
