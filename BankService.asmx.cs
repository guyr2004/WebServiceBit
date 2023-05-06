using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebServiceBit.App_Code;
using System.Data;
using System.Data.OleDb;

namespace WebServiceBit
{
    /// <summary>
    /// Summary description for BankService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BankService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public Decimal GetBalance(string phoneNumber)
        {
            AccountService accountService = new AccountService();
            Decimal balance = accountService.GetBalance(phoneNumber);
            return balance;
        }

        [WebMethod]
        public string GetUserName(string phoneNumber)
        {
            AccountService accountService = new AccountService();
            string userName = accountService.GetUserName(phoneNumber);
            return userName;
        }

        [WebMethod]
        public DataSet GetTransaction(string phoneNumber)
        {
            AccountService accountService = new AccountService();
            DataSet dataSet = accountService.GetTransaction(phoneNumber);
            return dataSet;
        }

        [WebMethod]
        public string GetPhoneNumber(string username, string pass)
        {
            AccountService accountService = new AccountService();
            string phone = accountService.GetPhoneNumber(username, pass);
            return phone;
        }
    }
}
