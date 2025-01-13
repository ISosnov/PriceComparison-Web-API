using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.DBModels
{
    public class CategoryDBModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? ImageUrl { get; set; }

        [JsonIgnore]
        public int? ParentCategoryId { get; set; }
        public virtual CategoryDBModel? ParentCategory { get; set; }

        public virtual ICollection<CategoryDBModel> Subcategories { get; set; }
        public virtual ICollection<CategoryCharacteristicDBModel> CategoryCharacteristics { get; set; }
        public virtual ICollection<RelatedCategoryDBModel> RelatedCategories { get; set; }
        public virtual ICollection<ProductDBModel> Products { get; set; }
    }
}
