using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Core.Constants
{
    public static class ApiConstants
    {
        public const string ApiTitle = "AE Customer API";
        public const string ApiDescription = "AE Customer API is a simple CRUD API for customers";
        public const string ApiPrefix = "api";
        public const string ApiBaseRoute = ApiPrefix + "/" + ApiVersion.Current;
        
        public static class ApiVersion
        {
            public const string Current = "v" + Major;
            public const string Major = "1";
            public const string Minor = "0";
            public const string Patch = "0";
        }
    }
}
