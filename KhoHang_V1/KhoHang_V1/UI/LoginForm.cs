using System;
using System.Data;
using System.Windows.Forms;
using KhoHang_V1.DAL; // Đừng quên dòng này

namespace KhoHang_V1
{
    public partial class LoginForm : Form
    {
        public static string CurrentRole = "";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gọi DAL để xác thực
                DataTable dt = AccountDAL.Authenticate(txtUsername.Text.Trim(), txtPassword.Text.Trim());

                if (dt.Rows.Count > 0) // Đăng nhập thành công
                {
                    bool isLocked = Convert.ToBoolean(dt.Rows[0]["IsLocked"]);

                    if (isLocked)
                    {
                        MessageBox.Show("Tài khoản của bạn đã bị khóa. Liên hệ Admin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string fullName = dt.Rows[0]["FullName"].ToString();
                    CurrentRole = dt.Rows[0]["UserRole"].ToString();

                    MessageBox.Show($"Đăng nhập thành công!\nXin chào {CurrentRole}: {fullName}", "Chào mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở Form1
                    Form1 mainForm = new Form1();
                    this.Hide();
                    mainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc Mật khẩu không đúng!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát phần mềm?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}