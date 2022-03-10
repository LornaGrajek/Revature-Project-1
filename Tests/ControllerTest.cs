using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Models;
using Moq;
using WebApplication1.Controllers;
using StoreBL;

namespace Tests;

public class ControllerTest
{
    [Fact]  
    public void StoreControllerGetShouldGetAllStores()
    {
        var MockBL = new Mock<IBL>();
        MockBL.Setup(x => x.GetAllStores()).Returns(
            new List<Storefront>
            {
                new Storefront
                {
                    Address = "Universe",
                    Name = "Some Place"
                },
                new Storefront
                {
                    Name =  "Test Name",
                    Address = "Test Place"
                }
            }
            );
        var storeCtrller = new StoreController(MockBL.Object);
        var result = storeCtrller.Get();
        Assert.NotNull(result);
        Assert.IsType<List<Storefront>>(result);
    }

    [Fact]
    public void StoreControllerGetShouldGetStoreFrontById()
    {
        var mockBL = new Mock<IBL>();
        int i = 1;
        mockBL.Setup(x => x.GetStorefrontById(i)).Returns(
            new Storefront
            {
                 StoreID = 1,
                 Name =  "Test",
                 Address = "Test"
            }
        );
        var storeCtrller = new StoreController(mockBL.Object);
        var result = storeCtrller.Get(1);
        Assert.NotNull(result);
    }
    [Fact]
    public void CustomerControllerGetShouldSearchCustomer()
    {
        var mockBL = new Mock<IBL>();
        string username = "test";
        string password = "test";
        mockBL.Setup(x => x.SearchCustomer(username, password)).Returns(
            new List<Customer>
            {
                new Customer
                {
                    UserName = username,
                    Password = password
                }
            }
        );
        var customerCtrllr = new CustomerController(mockBL.Object);
        var result = customerCtrllr.SearchCustomer(username, password);
        Assert.NotNull(result);
    }

    [Fact]
    public void CustomerControllerShouldAddNewCustomer()
    {
        var mockBL = new Mock<IBL>();
        Customer customer = new Customer
        { CID = 1,
          UserName = "Test",
          Password = "test"
        };
        mockBL.Setup(x => x.AddCustomer(customer));
        var customerCtrllr = new CustomerController(mockBL.Object);
        var result = customerCtrllr.Post(customer);
        Assert.NotNull(result);
    }

    [Fact]
    public void ProductControllerShouldAddProduct()
    {
        var mockBL =  new Mock<IBL>();
        Product product = new Product
        {
            ProductName = "Test Product",
            Description = "Test",
            Price = 5
        };
        mockBL.Setup(x => x.AddProduct(product));
        var productCtrllr = new ProductController(mockBL.Object);
        var result = productCtrllr.Post(product);
        Assert.NotNull(result);
    }

    [Fact]
    public void InventoryControllerShouldAddProductToInventory()
    {
        var mockBL = new Mock<IBL>();
        int prodId = 1;
        int storeId = 1;
        int qunatity = 5;
        mockBL.Setup(x => x.AddProductToInventory(prodId, storeId, qunatity));
        var inventoryCtrllr = new InventoryController(mockBL.Object);
        var result = inventoryCtrllr.Post(prodId, storeId, qunatity);
        Assert.NotNull(result);
    }

    [Fact]
    public void InventoryControllerShouldRestockInventory()
    {
        var mockBL = new Mock<IBL>();
        int prodId = 1;
        int storeId = 1;
        int qunatity = 5;
        mockBL.Setup(x => x.RestockInventory(prodId, storeId, qunatity));
        var inventoryCtrllr = new InventoryController(mockBL.Object);
        var result = inventoryCtrllr.Post(prodId, storeId, qunatity);
        Assert.NotNull(result);
    }

    [Fact]
    public void StoreOrderControllerShouldGetAllStoreOrders()
    { 
        var mockBl = new Mock<IBL>();
        int storeId = 1;
        mockBl.Setup(x => x.GetAllStoreOrders(storeId)).Returns(
            new List<Order>
            {
                new Order
                {
                    StoreId = storeId,
                    CustomerId = 1,
                    OrderNumber = 1
                },
                new Order
                {
                    OrderNumber = 2,
                    CustomerId= 2,
                    StoreId = storeId
                }
            }
            ); ;
        var storeCtrller = new StoreOrderController(mockBl.Object);
        var result = storeCtrller.Get(storeId);
        Assert.NotNull(result);
        Assert.IsType<List<Order>>(result);
    }

    [Fact]
    public void CustomerOrderController1shouldgetallorders()
    {
        var mockBl = new Mock<IBL>();
        int custId = 1;
        mockBl.Setup(x => x.GetAllCustomerOrders(custId)).Returns(
            new List<Order>
            {
                new Order
                {
                    StoreId = 1,
                    CustomerId = custId,
                    OrderNumber = 1
                },
                new Order
                {
                    OrderNumber = 2,
                    CustomerId= custId,
                    StoreId = 2
                }
            }
            ); ;
        var CustOrderCtrller = new CustomerOrderController1(mockBl.Object);
        var result = CustOrderCtrller.Get(custId);
        Assert.NotNull(result);
        Assert.IsType<List<Order>>(result);
    }
}
