//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace ProductApi.model
{
    public interface IDb
    {
        void InsertUpdateToDb(DataTable table);
        void TruncateTable();
    }

    public class Db: IDb
    {
        private readonly IConfiguration _config;
        private readonly string _connectString;
        public Db(IConfiguration config) {
            _config = config;
            _connectString = _config.GetConnectionString("DefaultConnetion");
        }
        public void InsertUpdateToDb(DataTable table)
        {
            try
            {
                using SqlConnection sc = new SqlConnection(_connectString);
                using SqlBulkCopy bulkCopy = new SqlBulkCopy(sc);
                sc.Open();
                // 設定目標資料表的名稱
                bulkCopy.DestinationTableName = "Merchandise";

                // 對應欄位名稱
                bulkCopy.ColumnMappings.Add("Title", "Title");
                bulkCopy.ColumnMappings.Add("Price", "Price");
                bulkCopy.ColumnMappings.Add("Brand", "Brand");
                bulkCopy.ColumnMappings.Add("Category", "Category");
                bulkCopy.ColumnMappings.Add("Thumbnail", "Thumbnail");

                // 執行批次插入
                bulkCopy.WriteToServer(table);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void TruncateTable() {
            string query = "TRUNCATE TABLE dbo.Merchandise";
            try
            {
                using SqlConnection sc = new SqlConnection(_connectString);
                using SqlCommand command = new SqlCommand(query, sc);
                sc.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
