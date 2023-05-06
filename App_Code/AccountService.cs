using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace WebServiceBit.App_Code
{
    public class AccountService
    {
        private OleDbConnection myConnection;
        private OleDbTransaction objTransaction;
        public AccountService()
        {
            string conn = Connect.getconnectionString();
            myConnection = new OleDbConnection(conn);
        }

        public Decimal GetBalance(string phoneNumber)
        {
            OleDbCommand myCommand = new OleDbCommand("RetrieveBalance", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@PhoneNumber", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = phoneNumber;

            Object obj;
            try
            {
                myConnection.Open();
                obj = myCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
            return (Decimal)obj;
        }
        public string GetUserName(string phoneNumber)
        {
            OleDbCommand myCommand = new OleDbCommand("GetUserName", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@PhoneNumber", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = phoneNumber;

            Object obj;
            try
            {
                myConnection.Open();
                obj = myCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
            return (string)obj;
        }
        public string GetPhoneNumber(string username, string pass)
        {
            OleDbCommand myCommand = new OleDbCommand("GetAccountByUserNameAndPass", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@UserName", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = username;

            objParam = myCommand.Parameters.Add("@UserPassword", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = pass;

            Object obj;
            try
            {
                myConnection.Open();
                obj = myCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
            return (string)obj;
        }
        public DataSet GetTransaction(string phoneNumber)
        {
            OleDbCommand myCommand = new OleDbCommand("GetTransactions", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@PhoneNumber", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = phoneNumber;

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = myCommand;
            DataSet dataset = new DataSet();
            try
            {
                adapter.Fill(dataset, "Transactions");
                dataset.Tables["Transactions"].PrimaryKey = new DataColumn[] {dataset.Tables["Transactions"].Columns["TransactionID"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataset;
        }
        public void InsertTransaction(TransactionDetails transactionDetails)
        {
            OleDbCommand myCommand = new OleDbCommand("InsertNewTransaction", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            OleDbParameter objParam;
            myCommand.Transaction = objTransaction;

            objParam = myCommand.Parameters.Add("@DatePosted", OleDbType.DBDate);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = transactionDetails.DatePosted;

            objParam = myCommand.Parameters.Add("@amount", OleDbType.Decimal);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = transactionDetails.Amount;

            objParam = myCommand.Parameters.Add("@payee", OleDbType.BSTR);
            objParam.Direction = System.Data.ParameterDirection.Input;
            objParam.Value = transactionDetails.Payee;

            objParam = myCommand.Parameters.Add("@PhonePayMoney", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = transactionDetails.PhonePayMoney;

            objParam = myCommand.Parameters.Add("@TransactionStatus", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = transactionDetails.TransactionStatus;

            objParam = myCommand.Parameters.Add("@PhoneGetMoney", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = transactionDetails.PhoneGetMoney;


            try
            {
                 myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateBalance(string phoneNumber, Decimal newamount)
        {
            OleDbCommand myCommand = new OleDbCommand("UpdateBalance", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Transaction = objTransaction;
            OleDbParameter objParam;

            objParam = myCommand.Parameters.Add("@newamount", OleDbType.Decimal);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = newamount;

            objParam = myCommand.Parameters.Add("@PhoneNumber", OleDbType.BSTR);
            objParam.Direction = ParameterDirection.Input;
            objParam.Value = phoneNumber;

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void PayThatBill(TransactionDetails transactionDetails, int newbalancePay, int newbalanceGet)
        {
            try
            {
                myConnection.Open();
                objTransaction = myConnection.BeginTransaction();
                this.UpdateBalance(transactionDetails.PhonePayMoney, newbalancePay);
                this.UpdateBalance(transactionDetails.PhoneGetMoney, newbalanceGet);
                this.InsertTransaction(transactionDetails);
                objTransaction.Commit();
            }
            catch (Exception ex)
            {
                objTransaction.Rollback();
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}