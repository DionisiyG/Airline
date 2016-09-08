using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Airline
{
    class RandomPassport
    {
        private static Random _rnd =  RandomProvider.GetThreadRandom();

        public static string GetRandomPassport()
        {
            int charPassportlength = 3;
            int intPassportlength = 5;
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < charPassportlength; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _rnd.NextDouble() + 65)));
                builder.Append(ch);
            }
            builder.Append('-');
            for (int i = 0; i < intPassportlength; i++)
            {
                builder.Append(_rnd.Next(10));
            }
            return builder.ToString();
        }
    }
}
