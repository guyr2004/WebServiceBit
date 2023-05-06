using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace WebServiceBit.App_Code
{
    public class TransactionDetails
    {
        private int _transactionID;
        private DateTime _datePosted;
        private Decimal _amount;
        private string _payee;
        private string _phonePayMoney;
        private string _transactionStatus;
        private string _phoneGetMoney;

        public int TransactionID
        {
            get { return this._transactionID; }
            set { this._transactionID = value; }
        }
        public DateTime DatePosted
        {
            get { return this._datePosted; }
            set { this._datePosted = value; }
        }
        public Decimal Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }
        public string Payee
        {
            get { return this._payee; }
            set { this._payee = value; }
        }
        public string PhonePayMoney
        {
            get { return this._phonePayMoney; }
            set { this._phonePayMoney = value; }
        }
        public string TransactionStatus
        {
            get { return this._transactionStatus; }
            set { this._transactionStatus = value; }
        }
        public string PhoneGetMoney
        {
            get { return this._phoneGetMoney; }
            set { this._phoneGetMoney = value; }
        }

        public TransactionDetails()
        {

        }

        public TransactionDetails(int transactionID, Decimal amount, string payee, string phonePayMoney, string transactionStatus, string phoneGetMoney)
        {
            this._transactionID = transactionID;
            this._datePosted = DateTime.Now;
            this._amount = amount;
            this._payee = payee;
            this._phonePayMoney = phonePayMoney;
            this._transactionStatus = transactionStatus;
            this._phoneGetMoney = phoneGetMoney;
        }
    }
}