using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace WebServiceBit.App_Code
{
    public class UserService
    {
        protected OleDbConnection myConnection;
        public UserService()
        {
            string conn = Connect.getconnectionString();
            myConnection = new OleDbConnection(conn);
        }
    }
}