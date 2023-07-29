using AutoMapper;
using CarpatiansWalksAPI.Models;
using CarpatiansWalksAPI.Models.Dto;

namespace CarpatiansWalksAPI.Mappings
{   // class for mapping
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionDTO, Region>().ReverseMap();
            CreateMap<AddWalkDTO, Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<UpdateWalkDTO, Walk>().ReverseMap();


        }
    }
}
