using FitnessTracker.WebAPI.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitnessTracker.WebAPI.ViewModels
{
    public class CreateUnitVM
    {
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        [DataType(DataType.Text)]
        [RegularExpression("^(?!\\d+$)[a-zA-Z0-9]{1,2}$")]
        [JsonPropertyName("unitType")]
        [SwaggerSchema("Imperial unit abbreviation")]
        [SwaggerSchemaExample("KG")]
        public string UnitType { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        [DataType(DataType.Text)]
        [JsonPropertyName("description")]
        [SwaggerSchema("Imperial unit")]
        [SwaggerSchemaExample("Kilograms")]
        public string Description { get; set; }
    }
}
