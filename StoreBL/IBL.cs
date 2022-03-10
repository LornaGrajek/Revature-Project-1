namespace StoreBL;

public interface IBL
{
    List<Customer> GetAllCustomers();
    List<Customer> SearchCustomer(string username, string password);
    void AddCustomer(Customer newCustomer);
    Customer GetCustomerById(int custId);
    //-----------------------------------------------------------------------------------------------------------------
    List<Storefront> GetAllStores();
    Storefront GetStorefrontById(int storeID);
    void AddStore(Storefront storetoAdd);
    //-----------------------------------------------------------------------------------------------------------------
    List<Product> GetAllProductsByStoreId(int storeId);
    void AddProduct(Product productToAdd);
    void RemoveProduct(int prodID);
    List<Product> GetAllProducts();
    List<Inventory> GetInventoryByStoreId(int storeId);
    void RestockInventory(int prodId, int quantity, int storeId);
    void AddProductToInventory(int prodId, int storeId, int quantity);
    List<Inventory> GetAllInventories();
    int GetProductIdByName(string name);
    void RemoveProductFromInventory(int prodId);
    //-------------------------------------------------------------------------------------------
    void AddLineItem(LineItem newLI, int orderID);
    void AddOrder(Order orderToAdd);
    List<Order> GetAllStoreOrders(int storeId);
    List<Order> GetAllCustomerOrders(int custId);

}