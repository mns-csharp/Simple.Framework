using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework
{
    public class EnumConverter<T> where T : struct, IConvertible
    {
        public static T ToEnum(string str)
        {
            T item = default(T);

            item = (T)Enum.Parse(typeof(T), str, true);

            return item;
        }

        public static T ToEnum(int val)
        {
            T item = default(T);

            if (Enum.IsDefined(typeof(T), val))
            {
                item = (T)(object)val;
            }

            return item;
        }

        public static string ToString(T item)
        {
            string str = string.Empty;

            str = Enum.GetName(typeof(T), item);

            return str;
        }

        public static int ToInt32(T item)
        {
            int val = 0;

            val = Convert.ToInt32(item);

            return val;
        }
    }
}