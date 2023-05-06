﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.OleDb;
using WebServiceBit.App_Code;

namespace WebServiceBit
{
    /// <summary>
    /// Summary description for ClientBankService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ClientBankService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string PostBillService(string phoneNumberPay, string payee, decimal amount, string phoneNumberGet)
        {
            AccountService accountService = new AccountService();
            try
            {
                Decimal balancePay = accountService.GetBalance(phoneNumberPay);
                Decimal balanceGet = accountService.GetBalance(phoneNumberGet);
                if (amount <= balancePay)
                {
                    Decimal newbalancePay = balancePay - amount;
                    Decimal newbalanceGet = balanceGet + amount;
                    string transactionStatus = "מאושר";
                    accountService.PayThatBill(phoneNumberPay, newbalancePay, payee, amount, DateTime.Now, transactionStatus, phoneNumberGet, newbalanceGet);
                    return "success";
                }
                else
                {
                    return "can't pay";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
