using System.ComponentModel.DataAnnotations;
namespace Models;

public class Storefront
{
    public Storefront()
    {
        this.Products = new List<Inventory>();
    }
    public int StoreID { get; set; }
    public string Address { get; set; }
    [Required]
    public string Name { get; set; }
    public List<Inventory> Products { get; set; }
    public List<Order> Orders { get; set; }
}