using System.ComponentModel.DataAnnotations;

namespace Core_MyProject_Api.DAL.Entity
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
