using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace BestBuyCorporate
{
    class Updater
    {
        public string ConnStr { get; private set; }
        public Updater()
        {
            ConnStr = "";
        }
        public Updater(string connStr)
        {
            ConnStr = connStr;
        }
    }
}
