using DataBaseBibliary.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataBaseBibliary
{
    public class MSConnector
    {
        public string ConnectionString { get; }


        public DataAdapter DataAdapter { get; }
        
        public MSConnector(string dbName)
        {
            ConnectionString = "*****";
        }

        private SqlConnection Connecting()
        {
            SqlConnection connect = new SqlConnection(ConnectionString);
            return  connect;
        }

        public SqlDataReader GetAllData(string TableName)
        {
            SqlConnection conn = Connecting();
            string sql = "select * from " + TableName+" order by id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            return cmd.ExecuteReader();
        }

        public SqlDataReader getDataFromEtl (string SqlCmd)
        {
            var con = Connecting();
            SqlCommand command = new SqlCommand(SqlCmd, con);
            con.Open();
            return command.ExecuteReader();
        }

        public DataTable GetDataFromDataAdapter(string SqlCmd,string tableName)
        {
            var con = Connecting();
            DataTable dataTable = new DataTable(tableName);
            SqlCommand command = new SqlCommand(SqlCmd, con);
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.SelectCommand.Transaction = con.BeginTransaction();
            dataAdapter.Fill(dataTable);

            con.Close();
            return dataTable;

        }

        public List<LoadPackage> GetLoadPackages()
        {
            SqlDataReader packages = GetAllData("LoadPackage");
            List<LoadPackage> loadPackages = new List<LoadPackage>();
            if (packages.HasRows)
            {
                while (packages.Read())
                {
                    loadPackages.Add(
                        new LoadPackage
                        {
                            Id = packages.GetInt32(packages.GetOrdinal("id")),
                            DbName = packages.GetString(packages.GetOrdinal("dbname")),
                            TableName = packages.GetString(packages.GetOrdinal("TableName")),
                            SqlSctript = packages.GetString(packages.GetOrdinal("SQLScript")),
                            PrKey=packages.GetString(packages.GetOrdinal("PrKey")),
                            PowerOn=packages.GetBoolean(packages.GetOrdinal("PowerOn"))
                        }

                    );
                }

            }
            packages.Close();
            return loadPackages;
        }

        public List<LoadEvents> GetLoadEvents()
        {
            SqlDataReader events = GetAllData("LoadEvents");
            List<LoadEvents> eventList = new List<LoadEvents>();
            while (events.Read())
            {
                eventList.Add(
                    new LoadEvents
                    {
                        Id = events.GetInt32(events.GetOrdinal("Id")),
                        DateTimeEvent = events.GetDateTime(events.GetOrdinal("DateTimeEvent")),
                        Flag = events.GetBoolean(events.GetOrdinal("Flag")),
                        CountRow=events.GetInt32(events.GetOrdinal("CountRow"))
                    }
                );
            }
            events.Close();
            return eventList;
        }

      
    }


}
