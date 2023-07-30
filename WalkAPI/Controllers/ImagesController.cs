using AutoMapper;
using Azure.Core;
using CarpatiansWalksAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkAPI.Repository;

namespace WalkAPI.Controllers
{
    [Route("api/img")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost] // REMOVE WAL DOMAIN MODEL AS PARAMETER
        [Route("upload")]
        public async Task<IActionResult> Create([FromForm] AddImageDTO addImageDTO)
        {
            ValidateFileUpload(addImageDTO); 
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = addImageDTO.File,
                    FileExtention = Path.GetExtension(addImageDTO.File.FileName),
                    FileSizeInBytes = addImageDTO.File.Length,
                    FileName = addImageDTO.FileName,
                    FileDescription = addImageDTO.FileDescription,
                };
                await imageRepository.Upload(imageDomainModel);
            }
            return BadRequest();
        }

        private void ValidateFileUpload(AddImageDTO addImageDTO)
        {
            var allowedExtensions = new string[] {".jpg", ".png", ".jpeg" };
            if (!allowedExtensions.Contains(Path.GetExtension(addImageDTO.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (addImageDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Too big file");

            }
        }
    }
}
