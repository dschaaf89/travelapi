using System.ComponentModel.DataAnnotations;

namespace TravelApi.Models
{
  public class Review
  {

    public string Destination { get; set; }
    public int ReviewId { get; set; }
    [Required]
    public string ReviewDetails { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    [StringLength(20)]
    public string City { get; set; }
    [Required]
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public int Rating { get; set; }
  }
}