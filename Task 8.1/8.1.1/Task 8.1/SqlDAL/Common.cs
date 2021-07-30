using System.Collections.Generic;
using System.Configuration;
using System.Data;


namespace SqlDAL
{
    class Common
    {
        internal static string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

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
