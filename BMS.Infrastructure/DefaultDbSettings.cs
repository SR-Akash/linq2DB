using LinqToDB.Configuration;
using System.Collections.Generic;

namespace BMS.Infrastructure
{
    public class DefaultDbSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders
        {
            get { yield break; }
        }
        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public string ConnectionString { get; set; }

        private readonly IConnectionStringSettings[] _connectionStrings;

        public DefaultDbSettings(IConnectionStringSettings[] connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }
        public IEnumerable<IConnectionStringSettings> ConnectionStrings => _connectionStrings;

    }
}
