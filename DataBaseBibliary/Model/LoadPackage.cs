using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseBibliary
{
    public class LoadPackage
    {
        public int Id { get; set;}
        public string DbName { get; set; }
        public string TableName { get; set; }
        public string SqlSctript { get; set; }
        public string PrKey { get; set; }
        public bool PowerOn { get; set; }
    }
}
