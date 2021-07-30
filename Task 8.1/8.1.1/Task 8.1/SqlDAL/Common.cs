using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDAL
{
    class Common
    {
        public const string _connectionString = "Data Source=DESKTOP-VHEEB1U;Initial Catalog=DAL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static DataTable ParseConnectedIds(List<int> connectedIds)
        {
            var result = new DataTable();

            DataColumn idColumn = new DataColumn("Id", typeof(int));

            result.Columns.Add(idColumn);

            for (int i = 0; i < connectedIds.Count; i++)
            {
                DataRow row = result.NewRow();

                row.ItemArray = new object[] { connectedIds[i] };

                result.Rows.Add(row);
            }

            return result;
        }
    }
}
