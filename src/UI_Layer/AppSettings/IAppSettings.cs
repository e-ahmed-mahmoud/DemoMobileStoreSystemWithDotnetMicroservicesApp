using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI_Layer.AppSettings
{
    public interface IAppSettings
    {
        string BaseAddress { get; set; }

        string CatalogPath { get; set; }

        string BasketPath { get; set; }

        string OrderPath { get; set; }
    }
}