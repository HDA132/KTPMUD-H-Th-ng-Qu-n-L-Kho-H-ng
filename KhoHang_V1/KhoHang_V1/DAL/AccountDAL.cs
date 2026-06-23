using System.Data;
using System.Data.SqlClient;

namespace KhoHang_V1.DAL
{
    public class AccountDAL
    {
        // Chuỗi kết nối - Bạn nhớ kiểm tra xem đã khớp với máy bạn chưa nhé
        private static string connStr = @"Data Source=.\SQLEXPRESS01;Initial Catalog=WMS_Project;Integrated Security=True;TrustServerCertificate=True";

        // 1. Hàm xác thực đăng nhập
        // Trả về DataTable chứa thông tin nếu đúng, hoặc DataTable rỗng nếu sai
        public static DataTable Authenticate(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT FullName, UserRole, IsLocked FROM UsersTbl WHERE Username = @user AND Password = @pass";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 2. Lấy danh sách toàn bộ tài khoản
        public static DataTable GetAllUsers()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT Username, FullName, UserRole, IsLocked FROM UsersTbl", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 3. Kiểm tra tên đăng nhập đã tồn tại chưa
        public static bool UserExists(string username)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM UsersTbl WHERE Username = @user", conn);
                cmd.Parameters.AddWithValue("@user", username);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        // 4. Thêm tài khoản mới
        public static void AddUser(string username, string password, string fullName, string role)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO UsersTbl (Username, Password, FullName, UserRole, IsLocked) VALUES (@user, @pass, @name, @role, 0)", conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);
                cmd.Parameters.AddWithValue("@name", fullName);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.ExecuteNonQuery();
            }
        }

        // 5. Cập nhật thông tin tài khoản
        public static void UpdateUser(string username, string fullName, string role, string newPassword)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Nếu newPassword có nhập thì mới cập nhật, không thì giữ nguyên
                string sql = "UPDATE UsersTbl SET FullName = @name, UserRole = @role";
                if (!string.IsNullOrWhiteSpace(newPassword)) sql += ", Password = @pass";
                sql += " WHERE Username = @user";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@name", fullName);
                cmd.Parameters.AddWithValue("@role", role);
                if (!string.IsNullOrWhiteSpace(newPassword)) cmd.Parameters.AddWithValue("@pass", newPassword);

                cmd.ExecuteNonQuery();
            }
        }

        // 6. Khóa / Mở khóa tài khoản
        public static void ToggleLockUser(string username)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Dùng toán tử đảo bit (~) để đổi 0 thành 1 hoặc 1 thành 0
                SqlCommand cmd = new SqlCommand("UPDATE UsersTbl SET IsLocked = ~IsLocked WHERE Username = @user", conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.ExecuteNonQuery();
            }
        }
    }
}