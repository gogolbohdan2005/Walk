    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarpatiansWalksAPI.Models.Dto;
using AutoMapper;
using CarpatiansWalksAPI.Models;
using CarpatiansWalksAPI.Repository;

namespace CarpatiansWalksAPI.Controllers
{
    [Route("api/walk")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetWalks(
            [FromQuery] string? filterOnParameter = null, // NULL BY DEFAULT
            [FromQuery] string? query = null, 
            [FromQuery] string? sortByParameter = null, 
            [FromQuery] bool isDecending = true,
            [FromQuery] int pageNum = 1,
            [FromQuery] int pageSize = 10) // THIS WILL BE FALSE BY DEFAULT, CHANGING THAT
        {
            var walks = await walkRepository.GetAllAsync(filterOnParameter, query, sortByParameter, isDecending, pageNum, pageSize);
            return Ok(mapper.Map<List<WalkDTO>>(walks));
        }
         
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetWalkById([FromRoute] int id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walkDomain));
        }

        [HttpPost] // REMOVE WAL DOMAIN MODEL AS PARAMETER
        public async Task<IActionResult> Create([FromBody] AddWalkDTO addWalkDTO)
        {
            if (ModelState.IsValid)
            {
                var walkDomain = mapper.Map<Walk>(addWalkDTO);
                await walkRepository.CreateAsync(walkDomain);
                WalkDTO walkDTO = mapper.Map<WalkDTO>(walkDomain);
                return CreatedAtAction(nameof(GetWalkById), new { id = walkDomain.Id }, walkDTO);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateWalkDTO updateWalkDTO)
        {
            if (ModelState.IsValid)
            {
                var walkDomain = mapper.Map<Walk>(updateWalkDTO);
                var returnedSameWalkDomain = await walkRepository.UpdateAsync(id, walkDomain);
                if (returnedSameWalkDomain == null) // WHEN NOT FOUND RETURN NULL
                {
                    return NotFound();
                }
                return Ok(mapper.Map<WalkDTO>(returnedSameWalkDomain));
            }
            return BadRequest();
            
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleteWalk = await walkRepository.DeleteAsync(id);
            if (deleteWalk == null)
            {
                return NotFound();
            }
            return Ok(deleteWalk);
        }


    }
}
