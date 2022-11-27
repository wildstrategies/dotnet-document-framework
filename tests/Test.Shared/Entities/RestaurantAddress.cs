using System.ComponentModel.DataAnnotations;

namespace Test.Shared.Entities
{
    public sealed class RestaurantAddress
    {
#pragma warning disable IDE1006 // Naming Styles
        [Required] public string building { get; init; } = null!;
        [Required] public string street { get; init; } = null!;
        [Required] public string zipcode { get; init; } = null!;
        [Required] public IEnumerable<double> coord { get; set; } = Array.Empty<double>();
#pragma warning restore IDE1006 // Naming Styles
    }
}
