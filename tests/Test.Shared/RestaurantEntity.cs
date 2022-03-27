using System.ComponentModel.DataAnnotations;
using WildStrategies.DocumentFramework;

namespace Test.Shared
{
    public sealed class RestaurantEntity : Entity
    {
#pragma warning disable IDE1006 // Naming Styles
        [Required] public string cuisine { get; init; } = null!;
        [Required] public string name { get; init; } = null!;
        [Required] public string restaurant_id { get; init; } = null!;
        [Required] public string borough { get; init; } = null!;
        [Required] public RestaurantAddress address { get; init; } = null!;
        [Required] public IEnumerable<RestaurantGrade> grades { get; init; } = Array.Empty<RestaurantGrade>();
#pragma warning restore IDE1006 // Naming Styles
    }
}
