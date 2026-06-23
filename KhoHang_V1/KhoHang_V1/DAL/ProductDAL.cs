using System;
using System.Data;
using System.Data.SqlClient;

namespace KhoHang_V1.DAL
{
    public class ProductDAL
    {
        // Chuỗi kết nối - Đảm bảo giống với chuỗi bạn đang dùng ở các Form
        private static string connStr = @"Data Source=.\SQLEXPRESS01;Initial Catalog=WMS_Project;Integrated Security=True;TrustServerCertificate=True";

        // ==========================================
        // 1. CÁC HÀM CƠ BẢN (THÊM, SỬA, XÓA, XEM)
        // ==========================================

        public static DataTable GetAllProducts()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT PSKU, PName, PCategory, PStock FROM ProductTbl", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static int CountByColumn(string column, string value)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Lưu ý: Tên cột (column) ở đây phải khớp với tên trong DB
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM ProductTbl WHERE {column} = @val", conn);
                cmd.Parameters.AddWithValue("@val", value);
                return (int)cmd.ExecuteScalar();
            }
        }

        public static void AddProduct(string sku, string name, string cat, int qty)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ProductTbl (PSKU, PName, PCategory, PStock) VALUES (@sku, @name, @cat, @qty)", conn);
                cmd.Parameters.AddWithValue("@sku", sku);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@cat", cat);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.ExecuteNonQuery();
            }
        }

        public static int UpdateProduct(string sku, string name, string cat, int qty)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ProductTbl SET PName = @name, PCategory = @cat, PStock = @qty WHERE PSKU = @sku", conn);
                cmd.Parameters.AddWithValue("@sku", sku);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@cat", cat);
                cmd.Parameters.AddWithValue("@qty", qty);
                return cmd.ExecuteNonQuery();
            }
        }

        public static int DeleteProduct(string sku)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM ProductTbl WHERE PSKU = @sku", conn);
                cmd.Parameters.AddWithValue("@sku", sku);
                return cmd.ExecuteNonQuery();
            }
        }

        public static DataTable SearchProducts(string keyword, string category)
        {
            string sql = "SELECT PSKU, PName, PCategory, PStock FROM ProductTbl WHERE PName LIKE @keyword";
            if (!string.IsNullOrWhiteSpace(category) && category != "Tất cả") sql += " AND PCategory = @category";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                if (!string.IsNullOrWhiteSpace(category) && category != "Tất cả") cmd.Parameters.AddWithValue("@category", category);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetLowStock()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT PSKU, PName, PCategory, PStock FROM ProductTbl WHERE PStock <= 5 ORDER BY PStock ASC", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ==========================================
        // 2. CÁC HÀM HỖ TRỢ ĐỒNG BỘ EXCEL
        // ==========================================

        public static void IncrementStock(string sku, int qty)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ProductTbl SET PStock = PStock + @qty WHERE PSKU = @sku", conn);
                cmd.Parameters.AddWithValue("@sku", sku);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.ExecuteNonQuery();
            }
        }

        public static string GetSkuByName(string name)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 PSKU FROM ProductTbl WHERE PName = @name", conn);
                cmd.Parameters.AddWithValue("@name", name);
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : null;
            }
        }

        public static bool CategoryExists(string category)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ProductTbl WHERE PCategory = @cat", conn);
                cmd.Parameters.AddWithValue("@cat", category);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
    }
}