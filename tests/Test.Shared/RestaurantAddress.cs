using System.ComponentModel.DataAnnotations;

namespace Test.Shared
{
    public sealed class RestaurantAddress
    {
        [Required] public string building { get; init; } = null!;
        [Required] public string street { get; init; } = null!;
        [Required] public string zipcode { get; init; } = null!;
        [Required] public IEnumerable<double> coord { get; set; } = Array.Empty<double>();
    }
}
