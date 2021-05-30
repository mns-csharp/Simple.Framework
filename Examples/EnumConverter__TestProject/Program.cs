using Simple.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumConverter__TestProject
{
    class Program
    {
        private enum MyEnum{YES, NO }

        static void Main(string[] args)
        {
            MyEnum myEnum = new MyEnum();
            myEnum = MyEnum.NO;

            int output = EnumConverter<MyEnum>.ToInt32(myEnum);

            Console.WriteLine("Value = {0}", output);

            Console.ReadLine();
        }
    }
}
