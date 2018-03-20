using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generator.Models;
using Generator.Utils;

namespace Generator.Core
{
    public class DefaultQueryProvider : IQueryProvider
    {
        public All GetAll(IDbConnection connection) {
            if (connection.State == ConnectionState.Closed) {
                connection.Open();
            }

            using (var cmd = connection.CreateCommand()) {
                var sql = File.ReadAllText(Constant.CurrentProviderQueryFile);
                if (sql.IsNullOrWhiteSpace())
                    throw new Exception($"{Constant.CurrentProviderQueryFile}:查询语句为空!");
                sql = sql.Replace("@DATABASE", connection.Database);
                cmd.CommandText = sql;
                var reader = cmd.ExecuteReader();

                DataSet dataSet = new DataSet();
                DataTable table1 = new DataTable();
                DataTable table2 = new DataTable();
                DataTable table3 = new DataTable();
                dataSet.Tables.Add(table1);
                dataSet.Tables.Add(table2);
                dataSet.Tables.Add(table3);

                dataSet.Load(reader, LoadOption.OverwriteChanges, table1, table2, table3);


                return new All {
                    Tables = GetTables(table1).ToList(),
                    Columns = GetColumns(table2).ToList(),
                    ForeignKeys = GetForeignKeys(table3).ToList()
                };
            }
        }


        private IEnumerable<TableInfo> GetTables(DataTable dataTable) {
            foreach (DataRow row in dataTable.Rows) {

                var table = new TableInfo();
                table.Name = getStringOrNull(row[0]);
                table.Desciption = getStringOrNull(row[1]);
                yield return table;
            }
        }

        private IEnumerable<ColumnInfo> GetColumns(DataTable dataTable) {
            foreach (DataRow row in dataTable.Rows) {
                var col = new ColumnInfo();
                col.TableName = getStringOrNull(row[0]);
                col.Name = getStringOrNull(row[1]);
                col.Desciption = getStringOrNull(row[2]);
                col.ColumnDbType = getStringOrNull(row[3]);
                col.IsPrimaryKey = Convert.ToBoolean(row[4]);
                col.Nullable = Convert.ToBoolean(row[5]);
                col.IsIdentity = Convert.ToBoolean(row[6]);
                col.Length = row[7] == null || row[7] == DBNull.Value ? null : (long?)Convert.ToInt64(row[7]);
                yield return col;
            }
        }

        private IEnumerable<ForeignKeyInfo> GetForeignKeys(DataTable dataTable) {
            foreach (DataRow row in dataTable.Rows) {
                var foreignKey = new ForeignKeyInfo();
                foreignKey.ReferencesTableName = getStringOrNull(row[0]);
                foreignKey.ReferencesColumnName = getStringOrNull(row[1]);
                foreignKey.BaseTableName = getStringOrNull(row[2]);
                foreignKey.BaseColumnName = getStringOrNull(row[3]);
                yield return foreignKey;
            }
        }


        private string getStringOrNull(object value) {
            if (value == null || value == DBNull.Value)
                return null;
            return value.ToString();
        }
    }
}
