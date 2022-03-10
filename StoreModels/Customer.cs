global using System.Data;
namespace Models;
using System.Text.RegularExpressions;
using CustomExceptions;


public class Customer
{
    public Customer()
    {
        this.Orders = new List<Order>();
    }
    public Customer(DataRow row)
    {
        this.CID = (int) row["CustomerId"];
        this.UserName = row["UserName"].ToString() ?? "";
        this.Password = row["PassWord"].ToString() ?? "";
    }
    public int CID { get; set; }
    // public string UserName { get; set; }
    private string _UserName;
    public string UserName 
    {
        get => _UserName;
        set
        {
            Regex pattern = new Regex("^[a-zA-Z0-9 !?']+$");
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new InputInvalidException("Name can't be empty");
            }
            else if(!pattern.IsMatch(value))
            {
                throw new InputInvalidException("Username can only have alphanumeric characters, white space, !, ?, and '.");
            }
            else
            {
                this._UserName = value;
            }
        }
    }
    public string Password { get; set; }
    public List<Order> Orders { get; set; }
}