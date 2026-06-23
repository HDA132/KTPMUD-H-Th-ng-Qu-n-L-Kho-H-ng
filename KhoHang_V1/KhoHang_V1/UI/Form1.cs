using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using KhoHang_V1.DAL;

namespace KhoHang_V1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadKho();
            try { btnQuanLyTaiKhoan.Visible = (LoginForm.CurrentRole.Trim().ToLower() == "admin"); }
            catch { btnQuanLyTaiKhoan.Visible = false; }
        }

        private void LoadKho()
        {
            dgvKho.DataSource = ProductDAL.GetAllProducts();
        }

        // --- CÁC NÚT XỬ LÝ HÀNG HÓA ---
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtQty.Text, out int qty)) { MessageBox.Show("Số lượng phải là số!"); return; }
            try
            {
                if (ProductDAL.CountByColumn("PSKU", txtSKU.Text.Trim()) > 0) { MessageBox.Show("Trùng SKU!"); return; }
                ProductDAL.AddProduct(txtSKU.Text.Trim(), txtName.Text.Trim(), cbDanhMuc.Text.Trim(), qty);
                LoadKho();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtQty.Text, out int qty)) { MessageBox.Show("Số lượng phải là số!"); return; }
            try
            {
                ProductDAL.UpdateProduct(txtSKU.Text.Trim(), txtName.Text.Trim(), cbDanhMuc.Text.Trim(), qty);
                LoadKho();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKho.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Xóa các dòng đã chọn?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgvKho.SelectedRows)
                    {
                        if (!row.IsNewRow) ProductDAL.DeleteProduct(row.Cells["PSKU"].Value.ToString());
                    }
                    LoadKho();
                }
            }
        }

        // --- NÚT EXCEL ---
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV Files (*.csv)|*.csv", FileName = "KhoHang.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("PSKU,PName,PCategory,PStock");
                foreach (DataGridViewRow row in dgvKho.Rows)
                {
                    if (!row.IsNewRow) sb.AppendLine($"{row.Cells[0].Value},{row.Cells[1].Value},{row.Cells[2].Value},{row.Cells[3].Value}");
                }
                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Xuất thành công!");
            }
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "CSV Files (*.csv)|*.csv" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PSKU"); dt.Columns.Add("PName"); dt.Columns.Add("PCategory"); dt.Columns.Add("PStock");
                string[] lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] c = lines[i].Split(',');
                    if (c.Length >= 4) dt.Rows.Add(c[0].Trim(), c[1].Trim(), c[2].Trim(), c[3].Trim());
                }
                new FormPreviewExcel(dt).ShowDialog();
                LoadKho();
            }
        }

        private void btnQuanLyTaiKhoan_Click(object sender, EventArgs e) => new FormQuanLyTaiKhoan().ShowDialog();
        private void btnDangXuat_Click(object sender, EventArgs e) { this.Close(); new LoginForm().Show(); }
        private void btnTimKiem_Click(object sender, EventArgs e) => dgvKho.DataSource = ProductDAL.SearchProducts(txtTimKiem.Text, cbTimKiemDanhMuc.Text);
        private void btnCanhBao_Click(object sender, EventArgs e) => dgvKho.DataSource = ProductDAL.GetLowStock();

        private void dgvKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKho.Rows[e.RowIndex];
                txtSKU.Text = row.Cells[0].Value?.ToString();
                txtName.Text = row.Cells[1].Value?.ToString();
                cbDanhMuc.Text = row.Cells[2].Value?.ToString();
                txtQty.Text = row.Cells[3].Value?.ToString();
            }
        }
    }
}