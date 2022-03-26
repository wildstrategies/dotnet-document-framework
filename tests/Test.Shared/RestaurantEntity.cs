using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildStrategies.DocumentFramework;

namespace Test.Shared
{


    public sealed class RestaurantEntity : Entity
    {
        public string cuisine { get; init; } = null!;
        public string name { get; init; } = null!;
        public string restaurant_id { get; init; } = null!;
        public string borough { get; init; } = null!;
        public RestaurantAddress address { get; init; } = null!;
        public IEnumerable<RestaurantGrade> grades { get; init; } = Array.Empty<RestaurantGrade>();
    }

    public sealed class RestaurantAddress
    {
        public string building { get; init; } = null!;
        public string street { get; init; } = null!;
        public string zipcode { get; init; } = null!;
        public IEnumerable<double> coord { get; set; } = Array.Empty<double>();
    }

    public sealed class RestaurantGrade
    {
        public DateTime date { get; init; }
        public string grade { get; init; } = null!;
        public int? score { get; init; }
    }
}
