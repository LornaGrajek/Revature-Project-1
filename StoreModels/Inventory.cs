namespace Models;

public class Inventory 
{
    public int InventoryID { get; set; }
    public int StoreId { get; set; }
    public int Quantity { get; set; }
    public int ProductID { get; set; }
    public Product Item { get; set; }
}