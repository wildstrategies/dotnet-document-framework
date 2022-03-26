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

    public sealed class RestaurantAddress
    {
        [Required] public string building { get; init; } = null!;
        [Required] public string street { get; init; } = null!;
        [Required] public string zipcode { get; init; } = null!;
        [Required] public IEnumerable<double> coord { get; set; } = Array.Empty<double>();
    }

    public sealed class RestaurantGrade
    {
        [Required] public DateTime date { get; init; }
        [Required] public string grade { get; init; } = null!;
        public int? score { get; init; }
    }
}
