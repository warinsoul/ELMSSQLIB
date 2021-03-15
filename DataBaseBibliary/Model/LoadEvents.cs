using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBibliary.Model
{
    public class LoadEvents
    {
        public int Id { get; set; }
        public DateTime DateTimeEvent { get; set; }
        public bool Flag {get;set;}
        public int CountRow { get; set; }
    }
}
