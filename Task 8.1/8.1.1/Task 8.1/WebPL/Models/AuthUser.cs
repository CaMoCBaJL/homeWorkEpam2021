using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPL.Models
{
    public static class AuthUser
    {
        public static string UserName { get; set; }

        public static string Password { get; set; }


        public static void ResetData()
        {
            UserName = null;

            Password = null;
        }
    }
}