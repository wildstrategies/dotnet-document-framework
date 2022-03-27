using System.ComponentModel.DataAnnotations;

namespace Test.Shared
{
    public sealed class RestaurantGrade
    {
        [Required] public DateTime date { get; init; }
        [Required] public string grade { get; init; } = null!;
        public int? score { get; init; }
    }
}
