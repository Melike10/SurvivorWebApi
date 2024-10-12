using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SurvivorWebApi.Models
{
    public class Competitor:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [JsonIgnore]
        public Category Category { get; set; }
    }
}
