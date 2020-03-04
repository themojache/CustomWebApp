using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomWebApp.Models {
    public class Results {
        public string Message { get; set; }
        public IEnumerable<string> Items { get; set; }
        //public override string ToString() {}
    }
}
