namespace RetailOrdering.API.Models
{
    public class Packaging
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty; 
        public string? Description { get; set; }
    }
}
