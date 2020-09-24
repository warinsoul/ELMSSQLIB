using Borland.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBibliary.Utils
{
    public class TAdoDbxDataCommandExtension : TAdoDbxCommand
    {
        public TAdoDbxDataCommandExtension(string sql, TAdoDbxConnection connection)
        {
            base.Connection = connection;
            base.CommandText = sql;
        }

    }
}
