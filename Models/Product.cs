using System.ComponentModel.DataAnnotations;

namespace Refrigrator.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
