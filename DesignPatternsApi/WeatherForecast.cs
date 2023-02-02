using System.ComponentModel.DataAnnotations;

namespace DesignPatternsApi
{
    public class DemoItem
    {
        [Required]
        public string LocationName { get; set; }

        [Required]
        public int TemperatureC { get; set; }

        public string? Summary { get; set; }
    }
}