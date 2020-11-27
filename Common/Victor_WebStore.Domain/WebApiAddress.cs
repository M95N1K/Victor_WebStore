using System;
using System.Collections.Generic;
using System.Text;

namespace Victor_WebStore.Domain
{
    public static class WebApiAddress
    {
        public const string Employees = "api/employees";
        public const string Products = "api/products";
        public const string Orders = "api/orders";

        public static class Identity
        {
            public const string User = "api/users";
            public const string Roles = "api/roles";
        }
    }
}
