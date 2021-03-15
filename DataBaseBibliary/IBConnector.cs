using Borland.Data;
using DataBaseBibliary.Utils;
using System;
using System.Data;
using System.Linq;


namespace DataBaseBibliary
{
    public class IBconnector
    {
        public string ConectionString { get;  }
        public TAdoDbxDataAdapter DataAdapter { get; set; }
        public DataTable DataTable { get; set; }

        private TAdoDbxConnection conn;
        private TAdoDbxCommand upsert;

      
        public IBconnector(string TableName,DataColumnCollection columnCollection,string prKey)
        {
            ConectionString = "****";
            conn = getConnection(ConectionString);
            string columnName="";
            string upsertColumn="";

            foreach (DataColumn column in columnCollection)
            {
                if (column.Ordinal==0)
                {
                    columnName =column.ColumnName;
                    upsertColumn = column.ColumnName + "=?";
                }else {
                    columnName += " ," + column.ColumnName;
                    upsertColumn+=" ,"+column.ColumnName + "=?";
                }
            }

            string sql = "select rdb$field_name from rdb$relation_constraints" +
                            " left join rdb$index_segments on rdb$relation_constraints.rdb$index_name = rdb$index_segments.rdb$index_name" +
                            " where rdb$constraint_type = 'PRIMARY KEY' and RDB$RELATION_NAME ='" + TableName+"'";
            TAdoDbxDataCommandExtension cmd = new TAdoDbxDataCommandExtension(sql, conn);
            conn.Open();
            var PrimaryKey = cmd.ExecuteScalar();
            
            Console.WriteLine(columnName);
            string pKey = PrimaryKey.ToString().Trim();
            string zvezda = "*";
            if (PrimaryKey != null) { PrimaryKey= pKey + ","  ; }

            sql = "select " + PrimaryKey +  columnName  + " from " + TableName;
            cmd = new TAdoDbxDataCommandExtension(sql, conn);

            DataTable = new DataTable(TableName);
            DataAdapter = new TAdoDbxDataAdapter(cmd);

            DataAdapter.ReturnProviderSpecificTypes = true;
            DataAdapter.AcceptChangesDuringUpdate = true;
            DataAdapter.FillLoadOption = LoadOption.OverwriteChanges;
            DataAdapter.SelectCommand.Transaction = conn.BeginTransaction();


            
            DataAdapter.Fill(DataTable);

            sql = "update " + TableName + " set " + pKey+"=? ," + upsertColumn + " where " + pKey + "=?";

            TAdoDbxCommandBuilder commandBuilder = new TAdoDbxCommandBuilder(DataAdapter);

            DataAdapter.InsertCommand = (TAdoDbxCommand)commandBuilder.GetInsertCommand();

            upsert= (TAdoDbxCommand)commandBuilder.GetUpdateCommand(true);
            for (int i = upsert.Parameters.Count-1; i >DataTable.Columns.Count ; i--)
            {
                upsert.Parameters.RemoveAt(i);
            }
            upsert.CommandText = sql;
            DataAdapter.UpdateCommand = upsert; //(TAdoDbxCommand)commandBuilder.GetUpdateCommand(true);
            DataAdapter.UpdateCommand.UpdateRowSource = UpdateRowSource.Both;
            DataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.Both;

            var sd = DataAdapter.GetFillParameters();
           //conn.Close();
        }


        public void Update()
        {
            //conn.Open();
            try
            {
                DataAdapter.InsertCommand.Transaction = conn.BeginTransaction();
                DataAdapter.Update(DataTable.Select(null, null, DataViewRowState.Added));
                DataAdapter.InsertCommand.Transaction.Commit();
                DataAdapter.ContinueUpdateOnError = true;


                foreach (var item in DataTable.Select(null, null, DataViewRowState.ModifiedCurrent))
                {
                    upsert.Transaction = conn.BeginTransaction();
                    for (int i = 0; i <= item.ItemArray.Count(); i++)
                    {
                        if (i== item.ItemArray.Count())
                        {
                            upsert.Parameters[i].Value = item.ItemArray[0];
                        }
                        else
                        {
                            upsert.Parameters[i].Value = item.ItemArray[i];
                        }
                    }
                    upsert.ExecuteNonQuery();
                    upsert.Transaction.Commit();
                }         
            }
            catch (Exception ex )
            {
                foreach (TAdoDbxParameter item in upsert.Parameters)
                {
                    Console.Write("\t{0}", item.Value);
                }
                Console.WriteLine(ex.Message);
            }
        }

        private TAdoDbxConnection getConnection(string ConnectionString)
        {
            return new TAdoDbxConnection(ConnectionString);
        }
    }
}
