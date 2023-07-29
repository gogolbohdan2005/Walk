using System.ComponentModel.DataAnnotations;

namespace CarpatiansWalksAPI.Models
{
    public class Region
    {
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string HowToGetHere { get; set; }
        public string Places { get; set; }
    }
}
