using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComaCuras.web.Models
{
    public static class Utility
    {
        /*
        ** returns the day corresponding to a number
        */
        public static string GetDay(int i)
        {
            string day = i switch
            {
                1 => "Monday",
                2 => "Tuesday",
                3 => "Wednesday",
                4 => "Thursday",
                5 => "Friday",
                6 => "Saturday",
                7 => "Sunday",
                _ => "Error",
            };
            return day;
        }

        public static int returnDay(string str)
        {
            int res = str switch
            {
                "Monday" => 1,
                "Tuesday" => 2,
                "Wednesday" => 3,
                "Thursday" => 4,
                "Friday" => 5,
                "Saturday" => 6,
                "Sunday" => 7,
                _ => -1,
            };
            return res;
        }
    }
}
