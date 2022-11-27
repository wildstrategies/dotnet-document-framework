using System.ComponentModel.DataAnnotations;
using WildStrategies.DocumentFramework;

namespace Test.Shared.Models
{
    public class TestSubentity : Entity
    {
        [Required] public string RequiredString { get; set; } = null!;
        [Range(0, 10)] public int MaxStringLength { get; set; } = 12;
    }
}
