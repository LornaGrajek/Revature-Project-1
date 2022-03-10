global using System.Text.Json;
global using Models;
namespace StoreDL;

public interface IRepo
{
    List<Customer> GetAllCustomers();
    List<Customer> SearchCustomer(string username, string password);
    void AddCustomer(Customer newCustomer);
    Customer GetCustomerById(int custId);
    //---------------------------------------------------------------------------------------------------------------------------------
    List<Storefront> GetAllStores();
    void AddStore(Storefront storetoAdd);
    Storefront GetStorefrontById(int storeID);
    //-----------------------------------------------------------------------------------
    List<Product> GetAllProductsByStoreId(int storeId);
    void AddProduct(Product productToAdd);
    void RemoveProduct(int prodID);
    void RemoveProductFromInventory(int prodId);
    List<Product> GetAllProducts();
    void RestockInventory(int prodId, int quantity, int storeId);
    void AddProductToInventory(int prodId, int storeId, int quantity);
    List<Inventory> GetInventoryByStoreId(int storeId);
    List<Inventory> GetAllInventories();
    int GetProductIdByName(string name);
    //---------------------------------------------------------------------------------
    void AddLineItem(LineItem newLI, int orderID);
    void AddOrder(Order orderToAdd);
    // List<Order> GetAllOrders(int CID);
    List<Order> GetAllStoreOrders(int storeId);
    List<Order> GetAllCustomerOrders(int custId);
}