using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    class ReadRecord
    {
        private static string fileName = System.IO.Path.Combine(Environment.CurrentDirectory, "Recepients.txt");
        public static string dilapidated = " ";
        public static void ReadingInf(string fName,List<String[]> recordInf)
        {
            List<string> arrr=new List<string>();

            for(int i = 0; i < recordInf.Count(); i++)
            {
                string ares =  "" ;
               
                for (int j = 0; j < recordInf[i].Length; j++)
                {
                    
                   ares += recordInf[i][j];
                }
                arrr.Add(ares);
            }

            for (int i = 0; i < recordInf.Count(); i++)
            {
                File.AppendAllLines(fName,recordInf[i]);
            }
        }
        public static void ReadingInf(string fName, String[] recordInf)
        {
            for (int i = 0; i < recordInf.Count(); i++)
            {
                File.WriteAllLines(fName, recordInf);
            }
        }

        public static List<string[]> ReadingInf(string fName)
         {
            fName+=".txt";
            List<string[]> outRes = new List<string[]>();
            // string Information;
            Console.WriteLine("The file is in the folder: " + Environment.CurrentDirectory);

           
            using (StreamReader sr = new StreamReader(fName, System.Text.Encoding.Default))
            {
                string[] res;
                
                string line;
               
                while ((line = sr.ReadLine()) != null)
                {
                    res = line.Split(new[] { dilapidated }, StringSplitOptions.RemoveEmptyEntries);

                    outRes.Add(res);

                }
                for (int i = 0; i < outRes.Count; i++)
                { string str2 = "";
                    for(int j = 0; j < outRes[i].Length; j++)
                    {
                        str2 += outRes[i][j]+" ";
                    }
                    Console.WriteLine(str2);                  
                   
                        Console.ReadKey();
                    
                }
               

            }

            return outRes;
         }


    }
}
