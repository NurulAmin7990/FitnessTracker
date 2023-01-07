using FitnessTracker.WebAPI.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitnessTracker.WebAPI.ViewModels
{
    public class ViewUnitVM
    {
        [DataType(DataType.Text)]
        [JsonPropertyName("id")]
        [SwaggerSchema("Unique identifier for unit")]
        [SwaggerSchemaExample("1")]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [JsonPropertyName("unitType")]
        [SwaggerSchema("Imperial unit abbreviation")]
        [SwaggerSchemaExample("KG")]
        public string UnitType { get; set; }

        [DataType(DataType.Text)]
        [JsonPropertyName("description")]
        [SwaggerSchema("Imperial unit")]
        [SwaggerSchemaExample("Kilograms")]
        public string Description { get; set; }
    }
}
