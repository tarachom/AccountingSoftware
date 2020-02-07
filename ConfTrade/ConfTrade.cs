using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;

//Конфігурація Торгівля
namespace ConfTrade
{
    class MyGenericClass<T>
    {
        public void genericMethod(T[] genericParameter)
        {
            

            foreach (T a in genericParameter)
            {
                Console.WriteLine(a);
            }

        }
    }

    public class ConfTrade
    {
        static void Main(string[] args)
        {
            Conf.Config.Kernel = new Kernel();
            Conf.Config.Kernel.Open();

            string[] mas_a = new string[] { "a", "b" };
            int[] mas_b = new int[] { 1, 2, 3 };

            MyGenericClass<string> a = new MyGenericClass<string>();
            a.genericMethod(mas_a);


            Console.ReadLine();
        }

        static string print_array(string[] arr)
        {
            bool is_first = true;

            string concat = "{";

            foreach (string e in arr)
            {
                if (!is_first)
                    concat += ", ";
                 else
                    is_first = false;

                concat += e;
            }

            concat += "}";

            return concat;
        }
    }
}
