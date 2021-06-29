using System;
namespace Shoption.Catalog.Dto
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public DateTime CratedTime { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
