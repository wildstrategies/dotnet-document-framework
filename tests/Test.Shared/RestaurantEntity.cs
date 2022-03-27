using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildStrategies.DocumentFramework;

namespace Test.Shared
{
    public sealed class RestaurantEntity : Entity
    {
        [Required] public string cuisine { get; init; } = null!;
        [Required] public string name { get; init; } = null!;
        [Required] public string restaurant_id { get; init; } = null!;
        [Required] public string borough { get; init; } = null!;
        [Required] public RestaurantAddress address { get; init; } = null!;
        [Required] public IEnumerable<RestaurantGrade> grades { get; init; } = Array.Empty<RestaurantGrade>();
    }
}
