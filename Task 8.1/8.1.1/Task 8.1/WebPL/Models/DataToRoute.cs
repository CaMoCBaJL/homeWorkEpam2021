using Entities;
using System.Collections.Generic;
using BL;
using Dependencies;

namespace WebPL.Models
{
    public class DataToRoute
    {
        public static List<string> Data { get; set; }

        public static List<string> AdditionalData { get; set; }

        public static int EntityToUpdateId { get; set; }

    }
}