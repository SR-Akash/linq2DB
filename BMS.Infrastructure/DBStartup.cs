using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Infrastructure
{
    public static class DBStartup
    {
        private static bool _started;
        public static void Init(List<ConnectionStringSettings> items)
        {
            if (!_started)
            {
                DataConnection.DefaultSettings = new DefaultDbSettings(items.ToArray());
                _started = true;
            }
        }
    }
}
