using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruchKlasse
{
    class Program
    {
        static void Main(string[] args)
        {
            Bruch a = new Bruch(5, 3);
            Bruch b = new Bruch(16, 4);
            Bruch c = new Bruch(3, 5);

            Bruch f = Bruch.Parse("26/32");
            Console.WriteLine(f);
            Console.WriteLine(f.Dezimal);
            Console.WriteLine();

            IComparer qCompi = Bruch.CreateComparer("Dezimal");

            ArrayList aList = new ArrayList();
            aList.Add(a);
            aList.Add(b);
            aList.Add(c);

            aList.Sort(qCompi);

            foreach (Bruch lB in aList)
            {
                Console.WriteLine(lB);
            }
            Console.ReadKey();
        }
    }
}