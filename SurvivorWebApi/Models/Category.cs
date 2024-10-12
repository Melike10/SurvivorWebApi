namespace SurvivorWebApi.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<Competitor> Competitors { get; set;} 

    }
}
