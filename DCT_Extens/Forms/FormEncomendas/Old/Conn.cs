using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Encomendas
{
    internal class Conn
    {
        private static string server = @"192.168.8.7\PRIMAVERA";
        private static string database = "PRIDECANTE";
        private static string user = "sa";
        private static string password = "Pri-2018";

        public static string StrCon
        {
            get { return "Data Source=" + server + "; Integrated Security=False;Initial Catalog=" + database + "; User ID=" + user + "; Password=" + password ; }
        }
    }
}
