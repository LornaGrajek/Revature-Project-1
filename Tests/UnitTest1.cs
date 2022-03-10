using Xunit;
using Models;
using CustomExceptions;
using System.Collections.Generic;


namespace Tests;

public class UnitTest1
{
    [Fact]
    public void CustomerShouldCreate()
    {
        Customer testCustomer = new Customer();
        Assert.NotNull(testCustomer);
    }

    [Fact]
    public void CustomerShouldSetValidData()
    {
        Customer testCustomer = new Customer();
        string username = "Test Name";
        string password = "Test password";

        testCustomer.UserName = username;
        testCustomer.Password = password;

        Assert.Equal(username, testCustomer.UserName);
        Assert.Equal(password, testCustomer.Password);
    }

    [Theory]
    [InlineData("#$%^@#$%#@")]
    [InlineData("     ")]
    [InlineData(null)]
    [InlineData("")]
    public void CustomerShouldNotSetInvalidName(string input)
    {
        Customer testCustomer = new Customer();
        Assert.Throws<InputInvalidException>(() => testCustomer.UserName = input);
    }

    [Fact]
    public void CustomerOrdersShouldBeAbleToSet()
    {
        Customer testCustomer = new Customer();
        List<Order> testOrders = new List<Order>();
        int testOrderCount = 0;

        testCustomer.Orders = testOrders;

        Assert.NotNull(testCustomer.Orders);
        Assert.Equal(testOrderCount, testCustomer.Orders.Count);
    }

    [Fact]
    public void OrderShouldCreate()
    {
        Order testOrder = new Order();
        Assert.NotNull(testOrder);
    }

    [Fact]
    public void ProductShouldCreate()
    {
        Product testProduct = new Product();
        Assert.NotNull(testProduct);
    }

    [Fact]
    public void LineItemShouldCreate()
    {
        LineItem testLineItem = new LineItem();
        Assert.NotNull(testLineItem);
    }
}