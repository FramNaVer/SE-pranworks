using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SE_pranworks.Models
{
    public class Modelecom
    {
        public class Customers
        {
            [Key]
            public int CustomerId { get; set; } 
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
            public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

        }
        public class Orders
        {
            [Key]
            public int OrderId { get; set; } 
            public decimal TotalAmount { get; set; }
            public DateTime OrderDate { get; set; }
            public bool IsPaid { get; set; }        
            public int CustomerId { get; set; }
            [JsonIgnore]
            public Customers Customer { get; set; }
            public ICollection<Products> Products { get; set; }
        }

        public class Products
        {
            [Key]
            public int ProductId { get; set; } 
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public bool IsInStock { get; set; }
            public DateTime CreatedDate { get; set; }

            public int OrderId { get; set; } 
            public Orders Order { get; set; }
        }
    }
}
