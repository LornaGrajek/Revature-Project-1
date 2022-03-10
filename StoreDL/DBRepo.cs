using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Models;
using System.Data;
using Serilog;
namespace StoreDL;


public class DBRepo : IRepo
{
    private string _connectionString;
    public DBRepo(string connectionString)
    {
        _connectionString = connectionString;
    }


//---------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Adds A new Customer to the database
    /// </summary>
    /// <param name="newCustomer">The new customer signing up</param>
    public void AddCustomer(Customer newCustomer)
    {
        Random rand = new Random();
        int CID = rand.Next(1, 1001);
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Customer (CustomerId, UserName, PassWord) VALUES (@p1, @p2, @p3)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", CID));
                cmd.Parameters.Add(new SqlParameter("@p2", newCustomer.UserName));
                cmd.Parameters.Add(new SqlParameter("@p3", newCustomer.Password));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public List<Customer> SearchCustomer(string username, string password)
    {
        string searchQuery = $"SELECT * FROM Customer WHERE UserName LIKE '%{username}%' AND PassWord LIKE '%{password}%'";

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand(searchQuery, connection);
        using SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet customerSet = new DataSet();
        adapter.Fill(customerSet, "Customer");
        DataTable customerTable = customerSet.Tables["Customer"];
        List<Customer> searchResult = new List<Customer>();
        foreach(DataRow row in customerTable.Rows)
        {
            Customer customer = new Customer(row);
            searchResult.Add(customer);
        }

        return searchResult;
    }
    public List<Customer> GetAllCustomers()
    {
        List<Customer> allCustomers = new List<Customer>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = "SELECT * FROM Customer";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer cust = new Customer();
                        cust.CID = reader.GetInt32(0);
                        cust.UserName = reader.GetString(1);
                        cust.Password = reader.GetString(2);

                        allCustomers.Add(cust);
                    }
                }
            }
            connection.Close();
        }
        return allCustomers;
    }
    public Customer GetCustomerById(int custId)
    {
        string query = "SELECT * FROM Customer WHERE CustomerId = @custId";
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand(query, connection);
        SqlParameter param = new SqlParameter("@custId", custId);
        cmd.Parameters.Add(param);
        using SqlDataReader reader = cmd.ExecuteReader();
        Customer customer = new Customer();
        if (reader.Read())
        {
            customer.CID = reader.GetInt32(0);
            customer.UserName = reader.GetString(1);
            customer.Password = reader.GetString(2);
        }
        connection.Close();
        return customer;
    }


//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void AddStore(Storefront storetoAdd)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO StoreFront (StoreId, Name, Address) VALUES (@p1, @p2, @p3)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", storetoAdd.StoreID));
                cmd.Parameters.Add(new SqlParameter("@p2", storetoAdd.Name));
                cmd.Parameters.Add(new SqlParameter("@p3", storetoAdd.Address));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    /// <summary>
    /// Gets a list of every customer that has signed up
    /// </summary>
    /// <returns>a list of all customers</returns>
    public List<Storefront> GetAllStores()
    {
        List<Storefront> allStores = new List<Storefront>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = "SELECT * FROM StoreFront";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Storefront store = new Storefront();
                        store.StoreID = reader.GetInt32(0);
                        store.Name = reader.GetString(1);
                        store.Address = reader.GetString(2);

                        allStores.Add(store);
                    }
                }
            }
            connection.Close();
        }
        return allStores;
    }
    public Storefront GetStorefrontById(int storeID)
    {
        string query = "SELECT * FROM StoreFront WHERE StoreId = @storeID";  
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand(query, connection);
        SqlParameter param = new SqlParameter("@storeID", storeID);
        cmd.Parameters.Add(param);
        using SqlDataReader reader = cmd.ExecuteReader();
        Storefront store = new Storefront();
        if(reader.Read())
        {
            store.StoreID = reader.GetInt32(0);
            store.Name = reader.GetString(1);
            store.Address = reader.GetString(2);
        }
        connection.Close();
        return store;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------
    public List<Product> GetAllProducts()
    {
        List<Product> allProducts = new List<Product>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = "SELECT * FROM Product";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ProductID = reader.GetInt32(0);
                        product.ProductName = reader.GetString(1);
                        product.Description = reader.GetString(2);
                        product.Price = reader.GetInt32(3);

                        allProducts.Add(product);
                    }
                }
            }
            connection.Close();
        }
        return allProducts;
    }
    public List<Product> GetAllProductsByStoreId(int storeId)
    {
        List<Product> allProducts = new List<Product>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string prodSelect = $"SELECT p.ProductID, p.Name, p.Description, p.Price, i.StoreId, i.Quantity\nFROM Product p\nINNER JOIN Inventory i ON p.ProductID = i.ProductId\n WHERE i.StoreId = {storeId}\nORDER BY p.ProductID";
        DataSet ProdSet = new DataSet();
        using SqlDataAdapter prodAdapter = new SqlDataAdapter(prodSelect, connection);
        prodAdapter.Fill(ProdSet, "Product");
        DataTable ?ProductTable = ProdSet.Tables["Product"];
        foreach(DataRow row in ProductTable.Rows)
        {
            Product prod = new Product();
            prod.ProductID = (int) row["ProductID"];
            prod.ProductName = row["Name"].ToString();
            prod.Description = row["Description"].ToString();
            prod.Price = (int) row["Price"];
            allProducts.Add(prod);
        }
        return allProducts;
    }
    public int GetProductIdByName(string name)
    {
        int productId = 0;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string query = $"SELECT * FROM Product WHERE Name = {name}";
            using(SqlCommand cmd = new SqlCommand(query, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ProductID = reader.GetInt32(0);

                        productId = product.ProductID;
                    }
                }
            }
            connection.Close();
        }
        return productId;
    }
    public void AddProduct(Product productToAdd)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Product (Name, Description, Price) VALUES (@p1, @p2, @p3)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", productToAdd.ProductName));
                cmd.Parameters.Add(new SqlParameter("@p2", productToAdd.Description));
                cmd.Parameters.Add(new SqlParameter("@p3", productToAdd.Price));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
            Log.Information("Product added {name}{description}{price}", productToAdd.ProductName,productToAdd.Description,productToAdd.Price);
        }
    }
    public void RemoveProduct(int prodID)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM Product WHERE ProductID = @p1";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", prodID));
                
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public void RemoveProductFromInventory(int prodId)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM Inventory WHERE ProductId = @p1";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", prodId));
                
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public List<Inventory> GetInventoryByStoreId(int storeId)
    {
        List<Inventory> allInventory = new List<Inventory>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = $"SELECT * FROM Inventory WHERE StoreId = {storeId}";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inventory inventory = new Inventory();
                        inventory.InventoryID = reader.GetInt32(0);
                        inventory.StoreId = reader.GetInt32(1);
                        inventory.ProductID = reader.GetInt32(2);
                        inventory.Quantity = reader.GetInt32(3);

                        allInventory.Add(inventory);
                    }
                }
            }
            connection.Close();
        }
        return allInventory;
    }
    public List<Inventory> GetAllInventories()
    {
        List<Inventory> allInventories = new List<Inventory>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = "SELECT * FROM Inventory";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inventory inventory = new Inventory();
                        inventory.InventoryID = reader.GetInt32(0);
                        inventory.StoreId = reader.GetInt32(1);
                        inventory.ProductID = reader.GetInt32(2);
                        inventory.Quantity = reader.GetInt32(3);
                        allInventories.Add(inventory);
                    }
                }
            }
            connection.Close();
        }
        return allInventories;
    }
    /// <summary>
    /// Adds a new product to the database
    /// </summary>
    /// <param name="productToAdd">the new product you want to add</param>
    public void AddProductToInventory(int prodId, int storeId, int quantity)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Inventory (StoreId, ProductId, Quantity) VALUES (@p1, @p2, @p3)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", storeId));
                cmd.Parameters.Add(new SqlParameter("@p2", prodId));
                cmd.Parameters.Add(new SqlParameter("@p3", quantity));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public void RestockInventory(int prodId, int quantity, int storeId)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Inventory SET Quantity = @p0 WHERE ProductId = @p1 AND WHERE StoreId = @p3";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", quantity);
                cmd.Parameters.AddWithValue("@p1", prodId);
                cmd.Parameters.AddWithValue("@p3", storeId);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }


    //--------------------------------------------------------------------------------------------------------------------------------------
    public void AddOrder(Order orderToAdd)
    {
        DataSet OrderSet = new DataSet();
        string selectCmd = "SELECT * FROM Orders";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection))
            {
                dataAdapter.Fill(OrderSet, "Orders");
                DataTable ?orderTable = OrderSet.Tables["Orders"];
                DataRow newRow = orderTable.NewRow();
                orderToAdd.ToDataRow(ref newRow);

                orderTable.Rows.Add(newRow);
                string insertCmd = $"INSERT INTO Orders (OrderId, CustomerId, StoreId, Total, OrderDate) VALUES ('{orderToAdd.OrderNumber}', '{orderToAdd.CustomerId}', '{orderToAdd.StoreId}', '{orderToAdd.Total}', '{orderToAdd.OrderDate}')";

                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
                dataAdapter.Update(orderTable);
                // dataAdapter.Insert(orderTable);
                Log.Information("Order added {OrderId}{CustomerId}{StoreId}{Total}{OrderDate}", orderToAdd.OrderNumber,orderToAdd.CustomerId,orderToAdd.StoreId,orderToAdd.Total,orderToAdd.OrderDate);
            }
        }
    }
    public List<Order> GetAllStoreOrders(int storeId)
    {
        List<Order> allOrders = new List<Order>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = $"SELECT * FROM Orders WHERE StoreId = {storeId} ORDER BY Total DESC";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.OrderNumber = reader.GetInt32(0);
                        order.CustomerId = reader.GetInt32(1);
                        order.StoreId = reader.GetInt32(2);
                        order.Total = reader.GetInt32(3);
                        order.OrderDate = reader.GetDateTime(4);

                        allOrders.Add(order);
                    }
                }
            }
            connection.Close();
        }
        return allOrders;
    }
    public List<Order> GetAllCustomerOrders(int custId)
    {
        List<Order> allOrders = new List<Order>();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryTxt = $"SELECT * FROM Orders WHERE CustomerId = {custId} ORDER BY Total DESC";
            using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.OrderNumber = reader.GetInt32(0);
                        order.CustomerId = reader.GetInt32(1);
                        order.StoreId = reader.GetInt32(2);
                        order.Total = reader.GetInt32(3);
                        order.OrderDate = reader.GetDateTime(4);

                        allOrders.Add(order);
                    }
                }
            }
            connection.Close();
        }
        return allOrders;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void AddLineItem(LineItem newLI, int orderID)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO LineItem (Product, OrderId, Quantity) VALUES (@p1, @p2, @p3)";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.Add(new SqlParameter("@p1", newLI.ProductID));
                cmd.Parameters.Add(new SqlParameter("@p2", orderID));
                cmd.Parameters.Add(new SqlParameter("@p3", newLI.Quantity));
                cmd.ExecuteNonQuery();
            }
            connection.Close();
            Log.Information("LineItem added {ProductID}{OrderID}{quantity}", newLI.ProductID,newLI.OrderId,newLI.Quantity);
        }
    }
}
