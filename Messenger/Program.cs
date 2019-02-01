using Messenger;
using System;
using System.Collections.Generic;
using System.IO;

namespace Messenger
{
    class Program
    {
        public static bool Work = true;

        static void Main(string[] args)
        {

            using (FunctionSet.UsersContDB)
            {
                do
                {
                 FunctionSet.FirstMenu();
                } while (Work);

                //Console.ReadKey();
            }
        }
    }
}

