using System.ComponentModel.DataAnnotations;

namespace Test.Shared.Entities
{
    public sealed class RestaurantGrade
    {
#pragma warning disable IDE1006 // Naming Styles
        [Required] public DateTime date { get; init; }
        [Required] public string grade { get; init; } = null!;
        public int? score { get; init; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
