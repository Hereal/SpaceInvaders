using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Manager
{
    class Utils
    {
        public static Random rand = new Random(Guid.NewGuid().GetHashCode());

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public static int min(int a,int b)
        {
            if (a < b)
                return a;
            return b;
        }
    }
}
