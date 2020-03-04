using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomWebApp.Models {
    public class ConnectionManager { // Could implement a variety of solutions, HashSet/Dictionary for storing of different connection paramters, then build it with either SqlConnectionStringBuilder or String Intepolation
        public const string Default = "Data Source=ServerName;Initial Catalog=DataBaseName;User id=UserName;Password=Secret;";
    }
}
