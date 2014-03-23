using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AnimalStore.Data.Helpers
{
    public static class ConnectionStringHelper
    {
        public static string ConnectionStringName
        {
            get
            {
                if (ConfigurationManager.AppSettings["AnimalsContextConnectionString"] != null)
                {
                    return ConfigurationManager.
                        AppSettings["AnimalsContextConnectionString"];
                }
                return "DefaultConnection";
            }
        }
    }
}
