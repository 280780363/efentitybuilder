using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generator.Utils;
using Generator.Models;
using Generator.Utils;
namespace Generator.Core
{
    public class DefaultGenerator : IGenerator
    {

        ITypeMapper mapper = Factory.TypeMapper();

        public string GenerateEntity(TableInfo table, All all)
        {
            var entityContent = File.ReadAllText(Constant.EntityTemplateFile);

            var columns = all.Columns.Where(r => r.TableName.EqualsIgnoreCase(table.Name)).ToList();
            var baseForeignKeys = all.ForeignKeys.Where(r => r.BaseTableName.EqualsIgnoreCase(table.Name)).ToList();
            var referencesForeignKeys = all.ForeignKeys.Where(r => r.ReferencesTableName.EqualsIgnoreCase(table.Name));
            StringBuilder propertys = new StringBuilder();
            foreach (var item in columns)
            {
                propertys.Append(getEntityPropertyByColumn(item));
            }
            foreach (var item in baseForeignKeys)
            {
                propertys.AppendLine(getEntityPropertyByBaseForeignKey(item));
            }
            foreach (var item in referencesForeignKeys)
            {
                propertys.AppendLine(getEntityPropertyByReferencesForeignKey(item));
            }

            var content = entityContent.Replace("@TABLE_DES", (table.Desciption.IsNullOrWhiteSpace() ? table.Name : table.Desciption).Replace("\r\n", " ").Replace("\n", " "))
                 .Replace("@ENTITY_CLASS_NAME", table.Name)
                 .Replace("@ENTITY_PROPERTYS", propertys.ToString())
                 .Replace("@CONFIG_CONTENT", getEntityConfiurationContent(table, columns, baseForeignKeys));

            return content;
        }

        public string GenrateContext(All all, string contextName)
        {
            var configContent = File.ReadAllText(Constant.ContextTemplateFile);
            StringBuilder propertys = new StringBuilder();
            StringBuilder entityConfigs = new StringBuilder();
            foreach (var item in all.Tables)
            {
                propertys.AppendLine(getContextProperty(item));
                entityConfigs.AppendLine(getContextEntityConfig(item));
            }
            var content = configContent.Replace("@CONTEXT_PROPERTYS", propertys.ToString())
                 .Replace("@CONTEXT_ENTITY_CONFIG", entityConfigs.ToString())
                 .Replace("@CONTEXT_NAME", contextName);

            return content;
        }

        #region 实体类内容生成

        private string getEntityPropertyByColumn(ColumnInfo column)
        {
            StringBuilder sb = new StringBuilder();
            if (!column.Desciption.IsNullOrWhiteSpace())
            {
                sb.AppendLine("\t\t/// <summary>");
                sb.AppendLine($"\t\t/// {column.Desciption.Replace("\r\n", " ").Replace("\n", " ")}");
                sb.AppendLine("\t\t/// </summary>");
            }
            //TODO:
            sb.AppendLine($"\t\tpublic {mapper.GetType(column.ColumnDbType, column.IsNullable) ?? column.ColumnDbType} {column.Name} {{ get; set; }}");

            return sb.ToString();
        }

        private string getEntityPropertyByBaseForeignKey(ForeignKeyInfo foreignKey)
        {
            string baseColumnName = foreignKey.BaseColumnName;
            if (foreignKey.BaseColumnName.EndsWith("id", StringComparison.OrdinalIgnoreCase))
                baseColumnName = foreignKey.BaseColumnName.Substring(0, foreignKey.BaseColumnName.IndexOf("id", StringComparison.OrdinalIgnoreCase));
            return $"\t\tpublic virtual {foreignKey.ReferencesTableName} {baseColumnName}_{foreignKey.ReferencesTableName} {{ get; set; }}";
        }

        private string getEntityPropertyByReferencesForeignKey(ForeignKeyInfo foreignKey)
        {
            string baseColumnName = foreignKey.BaseColumnName;
            if (foreignKey.BaseColumnName.EndsWith("id", StringComparison.OrdinalIgnoreCase))
                baseColumnName = foreignKey.BaseColumnName.Substring(0, foreignKey.BaseColumnName.IndexOf("id", StringComparison.OrdinalIgnoreCase));
            return $"\t\tpublic virtual IList<{foreignKey.ReferencesTableName}> {baseColumnName}_{foreignKey.BaseTableName}s {{ get; set; }}";
        }

        private string getEntityConfiurationContent(TableInfo table, List<ColumnInfo> columns, List<ForeignKeyInfo> baseForeignKeys)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t\t\tbuilder.ToTable(\"{table.Name}\");");

            if (columns.Any(r => r.IsPrimaryKey))
            {

                var keys = columns.Where(r => r.IsPrimaryKey).Select(r => "r." + r.Name).Join(" ,");
                sb.AppendLine($"\t\t\tbuilder.HasKey(r => new {{ { keys } }});");
            }

            foreach (var item in columns)
            {
                if (mapper.GetType(item.ColumnDbType, item.IsNullable)?.EqualsIgnoreCase("string") == true)
                {
                    sb.AppendLine($"\t\t\tbuilder.Property(r => r.{item.Name}).HasMaxLength({item.Length ?? int.MaxValue}).IsRequired({(item.IsNullable ? "false" : "true")});");
                }
            }

            foreach (var item in baseForeignKeys)
            {
                sb.AppendLine($"\t\t\tbuilder.HasOne(r => r.{item.BaseTableName}_{item.BaseColumnName}).WithMany().HasForeignKey(r => r.{item.BaseColumnName})");
            }

            return sb.ToString();
        }

        #endregion

        #region 上下文类内容生成
        private string getContextProperty(TableInfo table)
        {
            return $"\t\tpublic DbSet<{table.Name}> {table.Name} {{ get; set; }}";
        }

        private string getContextEntityConfig(TableInfo table)
        {
            return $"\t\t\tmodelBuilder.ApplyConfiguration(new config_{table.Name}());";
        }
        #endregion
    }
}
