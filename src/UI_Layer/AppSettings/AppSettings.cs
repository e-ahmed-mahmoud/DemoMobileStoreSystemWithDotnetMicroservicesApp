using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI_Layer.AppSettings
{
    public class AppSettings : IAppSettings
    {
        public string BaseAddress { get; set; }

        public string CatalogPath { get; set; }

        public string BasketPath { get; set; }

        public string OrderPath { get; set; }

    }
}