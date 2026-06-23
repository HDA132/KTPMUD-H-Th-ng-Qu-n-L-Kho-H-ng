using System;
using System.Data;
using System.Windows.Forms;
using KhoHang_V1.DAL;

namespace KhoHang_V1
{
    public partial class FormPreviewExcel : Form
    {
        private DataTable _dt;

        public FormPreviewExcel(DataTable data)
        {
            InitializeComponent();
            _dt = data;

            // Ép dữ liệu vào bảng ngay lập tức khi mở Form để tránh lỗi khung xám
            if (_dt != null && _dt.Rows.Count > 0)
            {
                dgvPreview.AutoGenerateColumns = true;
                dgvPreview.DataSource = _dt;
                dgvPreview.AllowUserToAddRows = false;
            }
        }

        // NÚT LƯU VÀO KHO
        private void btnLuu_Click(object sender, EventArgs e)
        {
            int dongThemMoi = 0;
            int dongCongDon = 0;
            int dongBoQua = 0;

            foreach (DataGridViewRow row in dgvPreview.Rows)
            {
                if (row.IsNewRow) continue;

                // Lấy dữ liệu từng cột
                string sku = row.Cells["PSKU"].Value?.ToString().Trim();
                string name = row.Cells["PName"].Value?.ToString().Trim();
                string category = row.Cells["PCategory"].Value?.ToString().Trim();
                string qtyStr = row.Cells["PStock"].Value?.ToString().Trim();

                // Bỏ qua nếu cột số lượng không phải là số hợp lệ
                if (!int.TryParse(qtyStr, out int qty))
                {
                    dongBoQua++;
                    continue;
                }

                // ==========================================
                // TRƯỜNG HỢP 1: TRÙNG SKU -> CỘNG DỒN TỰ ĐỘNG
                // ==========================================
                if (ProductDAL.CountByColumn("PSKU", sku) > 0)
                {
                    ProductDAL.IncrementStock(sku, qty);
                    dongCongDon++;
                    continue; // Xong dòng này, chuyển sang dòng tiếp theo trong Excel
                }

                // --- TỪ ĐÂY TRỞ XUỐNG LÀ XỬ LÝ CHO SKU MỚI ---
                bool choPhepThem = true;

                // ==========================================
                // TRƯỜNG HỢP 2: TRÙNG TÊN (SKU MỚI NHƯNG TÊN CŨ)
                // ==========================================
                string skuCuCuaTenNay = ProductDAL.GetSkuByName(name);
                if (!string.IsNullOrEmpty(skuCuCuaTenNay))
                {
                    DialogResult result = MessageBox.Show(
                        $"Cảnh báo: Sản phẩm '{name}' đã có sẵn trong kho với mã '{skuCuCuaTenNay}'.\n\nBạn có chắc chắn muốn tạo thêm một mã SKU mới là '{sku}' cho mặt hàng này không?",
                        "Cảnh báo trùng tên", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        choPhepThem = false; // Bỏ qua dòng này
                        dongBoQua++;
                    }
                }

                // ==========================================
                // TRƯỜNG HỢP 3: TRÙNG LOẠI (TÊN MỚI NHƯNG LOẠI CŨ)
                // ==========================================
                if (choPhepThem && ProductDAL.CategoryExists(category))
                {
                    DialogResult result = MessageBox.Show(
                        $"Danh mục '{category}' đã có sẵn trong hệ thống.\n\nBạn có muốn tự động xếp sản phẩm '{name}' vào danh mục này không?",
                        "Hỏi xác nhận danh mục", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        choPhepThem = false; // Từ chối thêm
                        dongBoQua++;
                    }
                }

                // ==========================================
                // TRƯỜNG HỢP 4: DỮ LIỆU MỚI HOÀN TOÀN (Hoặc đã ấn Yes ở trên)
                // ==========================================
                if (choPhepThem)
                {
                    ProductDAL.AddProduct(sku, name, category, qty);
                    dongThemMoi++;
                }
            }

            // Hiển thị báo cáo chi tiết sau khi chạy xong toàn bộ file
            MessageBox.Show(
                $"Quá trình nhập dữ liệu hoàn tất!\n\n" +
                $"- Thêm mới hoàn toàn: {dongThemMoi} mã\n" +
                $"- Cộng dồn (Trùng SKU): {dongCongDon} mã\n" +
                $"- Bỏ qua / Lỗi: {dongBoQua} dòng",
                "Báo cáo nhập kho", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close(); // Tự động đóng Form Preview
        }

        // NÚT HỦY BỎ
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}