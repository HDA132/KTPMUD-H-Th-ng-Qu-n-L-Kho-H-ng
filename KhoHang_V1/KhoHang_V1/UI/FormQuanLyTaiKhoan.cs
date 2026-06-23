using System;
using System.Windows.Forms;
using KhoHang_V1.DAL; // <--- Quan trọng!

namespace KhoHang_V1
{
    public partial class FormQuanLyTaiKhoan : Form
    {
        public FormQuanLyTaiKhoan()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            dgvUsers.DataSource = AccountDAL.GetAllUsers();
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtFullName.Text = row.Cells["FullName"].Value.ToString();
                cbRole.Text = row.Cells["UserRole"].Value.ToString();

                bool isLocked = Convert.ToBoolean(row.Cells["IsLocked"].Value);
                btnKhoa.Text = isLocked ? "MỞ KHÓA TÀI KHOẢN" : "KHÓA TÀI KHOẢN";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập và Mật khẩu!");
                return;
            }

            if (AccountDAL.UserExists(txtUsername.Text))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại!");
                return;
            }

            AccountDAL.AddUser(txtUsername.Text, txtPassword.Text, txtFullName.Text, cbRole.Text);
            LoadUsers();
            MessageBox.Show("Đã cấp quyền tài khoản mới thành công!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text)) return;

            AccountDAL.UpdateUser(txtUsername.Text, txtFullName.Text, cbRole.Text, txtPassword.Text);
            LoadUsers();
            MessageBox.Show("Đã cập nhật thông tin thành công!");
            txtPassword.Clear();
        }

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text)) return;

            if (txtUsername.Text.ToLower() == "admin")
            {
                MessageBox.Show("Không thể khóa tài khoản Admin!");
                return;
            }

            AccountDAL.ToggleLockUser(txtUsername.Text);
            LoadUsers();
            MessageBox.Show("Đã cập nhật trạng thái tài khoản!");
        }
    }
}