using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyGit
{
    static class MySVC
    {
        private static readonly string dirPath = "./CurDir";

        public static void InitializeMySVC(int mode)
        {

            switch (mode)
            {
                case 1:
                    Console.WriteLine("Viewer mode enabled.");
                    break;

                case 2:
                    Console.WriteLine("Rollback mode enabled.");
                    break;
            }

            try
            {
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to create working directory. See:" +Environment.NewLine + ex.Message);
            }
        }

        public static void CloseMySVC()
        {

        }

    }
}
