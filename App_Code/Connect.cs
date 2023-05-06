﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceBit.App_Code
{
    public class Connect
    {
        const string FileName = "WebServiceBit.mdb";

        public static string getconnectionString()
        {
            string location = HttpContext.Current.Server.MapPath("~/App_Data/" + FileName);
            string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;
                                         data source=" + location;
            return ConnectionString;
        }
    }
}