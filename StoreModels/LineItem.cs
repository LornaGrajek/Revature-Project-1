namespace Models;

public class LineItem
{
    public LineItem(){}
    public LineItem(Product item, int quantity, int orderid, int ProductID)
    {
        this.ProductID = ProductID;
        this.Item = item;
        this.Quantity = quantity;
    }

    public Product Item { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public int ProductID { get; set; }
    
}