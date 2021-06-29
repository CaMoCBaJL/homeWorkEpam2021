using Entities;
using System.Collections.Generic;
using BLL;

namespace WebPL.Models
{
    public static class DataToRoute
    {
        public static List<string> Data { get; set; }

        public static EntityType DataType { get; set; }

        public static int EntityToUpdateId { get; set; }


        public static void UpdateData()
        {
            Data = new BuisnessLogic().GetListOfEntities(DataType, false);
        }
    }
}