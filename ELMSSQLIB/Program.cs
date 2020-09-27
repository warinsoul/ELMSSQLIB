using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Borland.Data;
using DataBaseBibliary;
using DataBaseBibliary.Model;
using DataBaseBibliary.Utils;

namespace ELMSSQLIB
{
    class Program
    {
        static void Main(string[] args)
        {

            var loadPackages = new MSConnector("obmen_rn").GetLoadPackages();

            foreach (var package in loadPackages)
            {
                if (!package.PowerOn) continue;
                var MS = new MSConnector(package.DbName).GetDataFromDataAdapter(package.SqlSctript, package.TableName);
    
              
                Parallel.For(0, MS.Rows.Count / 1000>1? MS.Rows.Count / 1000 : 1, j =>
                {
                    var IB = new IBconnector(package.TableName, MS.Columns, package.PrKey);
                    foreach (DataRow item in MS.Select().Skip((j)*1000).Take(1000))
                    {
                        string filter = "";
                        foreach (var kay in package.PrKey.Split(','))
                        {
                            if (MS.Columns[kay].DataType == typeof(string))
                            {
                                filter += kay + "=" + "'" + item.ItemArray[MS.Columns[kay].Ordinal] + "'";
                            }
                            else
                            {
                                filter += kay + "=" + item.ItemArray[MS.Columns[kay].Ordinal];
                            }
                            if (package.PrKey.Split(',').LastOrDefault() != kay)
                            {
                                filter += " AND ";
                            }

                        }
                        IB.DataTable.CaseSensitive = true;
                        var r = IB.DataTable.Select(filter).FirstOrDefault();

                        if (r == null)
                        {
                            var inserted = IB.DataTable.NewRow();
                            for (int i = 0; i < item.ItemArray.Length; i++)
                            {
                                if (item.Table.Columns[i].DataType == typeof(string)) 
                                    inserted[item.Table.Columns[i].ColumnName] = item.ItemArray[i].ToString().Trim();
                                else
                                    inserted[item.Table.Columns[i].ColumnName] = item.ItemArray[i];
                            }
                            IB.DataTable.Rows.Add(inserted);
                        }
                        else
                        {
                            for (int i = 0; i < item.ItemArray.Length; i++)
                            {

                                var right = item.ItemArray[i].GetHashCode();
                                var left = r[item.Table.Columns[i].ColumnName].GetHashCode();
                                if (!left.Equals(right))
                                {
                                    r.BeginEdit();
                                    r[item.Table.Columns[i].ColumnName] = item.ItemArray[i];
                                    r.EndEdit();
                                }
                            }
                        }
                        
                    }

                    IB.Update();
                }
                );
            }
            Console.WriteLine("Окончание...");
            Console.ReadKey();
        }
    }
}
