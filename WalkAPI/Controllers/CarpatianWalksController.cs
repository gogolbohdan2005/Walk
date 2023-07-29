using AutoMapper;
using CarpatiansWalksAPI.Data;
using CarpatiansWalksAPI.Models;
using CarpatiansWalksAPI.Models.Dto;
using CarpatiansWalksAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarpatiansWalksAPI.Controllers
{
    [Route("api/region")]
    [ApiController]
    //[Authorize]
    public class CarpatianWalksController : ControllerBase
    {
        private readonly CarpatianWalksDbContext _dbContext; // DEPENDANCY INJECTION
        private readonly IRegionRepository _regionRepository; // DEPENDANCY INJECTION
        private readonly IMapper _mapper; // DEPENDANCY INJECTION


        public CarpatianWalksController(
            CarpatianWalksDbContext dbContext, 
            IRegionRepository regionRepository, 
            IMapper mapper)
        {
            _dbContext = dbContext; // CONSTRUCTOR
            _regionRepository = regionRepository;
            _mapper = mapper;

        }

        [HttpGet]
        //ROUTING IF YOU WANT TO CHANGE ADRESS
        public async Task<IActionResult> GetRegions()
        {
            // Retrieve all regions from the database
            List<Region> regions = await _regionRepository.GetRegionsAsync();
            // Domain to DTO
                List<RegionDTO> regionsDTO = _mapper.Map<List<RegionDTO>>(regions); // VERY EFFISIENT, WITHOUT ANY LOOPS, etc...

            return Ok(regionsDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRegionsById(int id)
        {
            // Retrieve all regions from the database
            //var listing = _dbContext.Regions.ToList();
            Region? region = await _dbContext.Regions.FirstOrDefaultAsync(obj => obj.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDTO>(region));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody]AddRegionDTO addRegionDTO)
        {
            if (ModelState.IsValid) // CHECK WITH MODEL IF EVERYTHING IS OKAY
            {
                Region region = _mapper.Map<Region>(addRegionDTO);

                await _dbContext.Regions.AddAsync(region);
                await _dbContext.SaveChangesAsync();

                RegionDTO regionDTO = _mapper.Map<RegionDTO>(region);
                return CreatedAtAction(nameof(GetRegionsById), new { id = region.Id }, regionDTO);
            }
            return BadRequest("Invalid region data.");
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            //Region region = new Region { HowToGetHere = updateRegionDTO.HowToGetHere, };
            Region? region = await _dbContext.Regions.FirstOrDefaultAsync(obj => obj.Id == id);

            if (region == null)
            {
                return NotFound();
            }
            region.HowToGetHere = updateRegionDTO.HowToGetHere;
            region.Places = updateRegionDTO.Places;
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<RegionDTO>(region));
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Region? region = await _dbContext.Regions.FirstOrDefaultAsync(obj => obj.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();
            return Ok(_mapper.Map<RegionDTO>(region));
        }
    }
}
