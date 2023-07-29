using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpatiansWalksAPI.Models
{
    public class Walk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JsonMoutains { get; set; }
        public int MaxHeight { get; set; }
        public int Distance { get; set; }
        public int Elevation { get; set; }
        public string Path { get; set; } 
        public string JsonImageURLs { get; set; }
        
        public int RegionId { get; set; }

        public Region Region { get; set; }
    }
}
