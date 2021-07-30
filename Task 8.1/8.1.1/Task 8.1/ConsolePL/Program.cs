using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dependencies;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            new PresentationLayer(DependencyResolver.Instance.ProjectBLL);
        }
    }
}
