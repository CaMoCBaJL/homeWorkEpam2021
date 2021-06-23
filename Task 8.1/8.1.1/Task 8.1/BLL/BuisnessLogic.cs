using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using JsonDAL;


namespace BLL
{
    public class BuisnessLogic
    {
        public static List<string> GetListOfUsers()
        {
            var dal =  new DAL();

            List<string> result = new List<string>();

            foreach(var user in dal.GetUsers())
            {
                result.Add(user.ToString());
            }

            return result;
        }

        public static List<string> GetListOfAwards()
        {
            var dal = new DAL();

            List<string> result = new List<string>();

            foreach (var user in dal.GetAwards())
            {
                result.Add(user.ToString());
            }

            return result;
        }

    }
}
