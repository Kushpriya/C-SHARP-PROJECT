using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project3
{
    public static class global
    {
        public static decimal Tprice = 0;
        public static DataGridView? table;
        public static int ID = 0;
        public static string loginEmail = "";
        public static bool isGuestModeOn;
        public static int foodCategory(String s)
        {
            if (s == "veg")
            {
                return 1;
            }
            else if (s == "non_veg")
            {
                return  2;
            }
            else
            {
                return 3;
            }
        }
    }
}
