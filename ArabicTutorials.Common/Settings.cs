using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicTutorials.Common
{
    public class Settings
    {
        public string WebUrl { get; set; }
        public string QueryServiceUrl { get; set; }
        public string CommandServiceUrl { get; set; }
        public string MongoDbName { get; set; }
        public string MongoDbConnectionString { get; set; }
    }
}
