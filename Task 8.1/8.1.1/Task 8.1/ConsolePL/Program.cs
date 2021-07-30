using Dependencies;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            new PresentationLayer(DependencyResolver.Instance.ProjectBLL).Start();
        }
    }
}
