using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStatusAlert.Web.Models {
    public class IndexModel {
        public Dictionary<string, string> Regions { get; set; }
        public Dictionary<string, string> CellProviders { get; set; }
        public IndexModel(Dictionary<string, string> regions, Dictionary<string, string> cellProviders) {
            Regions = regions;
            CellProviders = CellProviders;
        }
    }
}