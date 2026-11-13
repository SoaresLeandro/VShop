using System.Text.Json.Serialization;

namespace Store.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public long Stock { get; set; }
        public string? ImageURL { get; set; }

        [JsonIgnore]
        public CategoryDTO CategoryDTO { get; set; }
    }
}