using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitnessTracker.WebAPI.ViewModels
{
    public class ViewUnitVM
    {
        [DataType(DataType.Text)]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [JsonPropertyName("unitType")]
        public string UnitType { get; set; }

        [DataType(DataType.Text)]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
