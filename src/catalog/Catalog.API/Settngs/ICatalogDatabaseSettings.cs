using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Settngs
{
    public interface ICatalogDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        public string CollectionName { get; set; }
    }

}
