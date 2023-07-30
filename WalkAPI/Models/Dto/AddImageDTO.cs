using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpatiansWalksAPI.Models
{
    public class AddImageDTO
    {
        [Required]
        public IFormFile File { get; set; } 
        [Required]
        public string FileName { get; set; }
        public string FileDescription { get; set; }
    }
}
