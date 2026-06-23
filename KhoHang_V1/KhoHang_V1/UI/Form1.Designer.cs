using KhoHang_V1.DAL;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace KhoHang_V1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSKU = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.MaskedTextBox();
            this.txtQty = new System.Windows.Forms.MaskedTextBox();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.dgvKho = new System.Windows.Forms.DataGridView();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cbDanhMuc = new System.Windows.Forms.ComboBox();
            this.cbTimKiemDanhMuc = new System.Windows.Forms.ComboBox();
            this.btnCanhBao = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnQuanLyTaiKhoan = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.btnNhapExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKho)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSKU
            // 
            this.txtSKU.Location = new System.Drawing.Point(304, 228);
            this.txtSKU.Name = "txtSKU";
            this.txtSKU.Size = new System.Drawing.Size(132, 20);
            this.txtSKU.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(304, 255);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(132, 20);
            this.txtName.TabIndex = 1;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(431, 228);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(132, 20);
            this.txtQty.TabIndex = 2;
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.Location = new System.Drawing.Point(304, 289);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(133, 23);
            this.btnThemMoi.TabIndex = 3;
            this.btnThemMoi.Text = "THÊM MÃ MỚI";
            this.btnThemMoi.UseVisualStyleBackColor = true;
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // dgvKho
            // 
            this.dgvKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKho.Location = new System.Drawing.Point(595, 200);
            this.dgvKho.Name = "dgvKho";
            this.dgvKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKho.Size = new System.Drawing.Size(429, 241);
            this.dgvKho.TabIndex = 6;
            this.dgvKho.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKho_CellClick);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(431, 289);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(132, 23);
            this.btnSua.TabIndex = 7;
            this.btnSua.Text = "SỬA THÔNG TIN";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(304, 318);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(133, 23);
            this.btnXoa.TabIndex = 8;
            this.btnXoa.Text = "XÓA MÃ HÀNG";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(595, 460);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(121, 20);
            this.txtTimKiem.TabIndex = 9;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(904, 458);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(120, 23);
            this.btnTimKiem.TabIndex = 10;
            this.btnTimKiem.Text = "TÌM KIẾM";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1428, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cbDanhMuc
            // 
            this.cbDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDanhMuc.FormattingEnabled = true;
            this.cbDanhMuc.Items.AddRange(new object[] {
            "Điện tử",
            "Gia dụng ",
            "Thực phẩm",
            "Văn phòng phẩm"});
            this.cbDanhMuc.Location = new System.Drawing.Point(431, 255);
            this.cbDanhMuc.Name = "cbDanhMuc";
            this.cbDanhMuc.Size = new System.Drawing.Size(132, 21);
            this.cbDanhMuc.TabIndex = 12;
            // 
            // cbTimKiemDanhMuc
            // 
            this.cbTimKiemDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimKiemDanhMuc.FormattingEnabled = true;
            this.cbTimKiemDanhMuc.Items.AddRange(new object[] {
            "Tất cả",
            "Điện tử",
            "Gia dụng ",
            "Thực phẩm ",
            "Văn phòng phẩm"});
            this.cbTimKiemDanhMuc.Location = new System.Drawing.Point(748, 460);
            this.cbTimKiemDanhMuc.Name = "cbTimKiemDanhMuc";
            this.cbTimKiemDanhMuc.Size = new System.Drawing.Size(121, 21);
            this.cbTimKiemDanhMuc.TabIndex = 13;
            // 
            // btnCanhBao
            // 
            this.btnCanhBao.Location = new System.Drawing.Point(431, 318);
            this.btnCanhBao.Name = "btnCanhBao";
            this.btnCanhBao.Size = new System.Drawing.Size(132, 23);
            this.btnCanhBao.TabIndex = 14;
            this.btnCanhBao.Text = "CẢNH BÁO HẾT HÀNG";
            this.btnCanhBao.UseVisualStyleBackColor = true;
            this.btnCanhBao.Click += new System.EventHandler(this.btnCanhBao_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Location = new System.Drawing.Point(304, 347);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(132, 23);
            this.btnXuatExcel.TabIndex = 15;
            this.btnXuatExcel.Text = "XUẤT EXCEL";
            this.btnXuatExcel.UseVisualStyleBackColor = true;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnQuanLyTaiKhoan
            // 
            this.btnQuanLyTaiKhoan.Location = new System.Drawing.Point(304, 397);
            this.btnQuanLyTaiKhoan.Name = "btnQuanLyTaiKhoan";
            this.btnQuanLyTaiKhoan.Size = new System.Drawing.Size(132, 23);
            this.btnQuanLyTaiKhoan.TabIndex = 16;
            this.btnQuanLyTaiKhoan.Text = "QUẢN LÝ TÀI KHOẢN";
            this.btnQuanLyTaiKhoan.UseVisualStyleBackColor = true;
            this.btnQuanLyTaiKhoan.Click += new System.EventHandler(this.btnQuanLyTaiKhoan_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Location = new System.Drawing.Point(431, 397);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(132, 23);
            this.btnDangXuat.TabIndex = 17;
            this.btnDangXuat.Text = "ĐĂNG XUẤT";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // btnNhapExcel
            // 
            this.btnNhapExcel.Location = new System.Drawing.Point(431, 347);
            this.btnNhapExcel.Name = "btnNhapExcel";
            this.btnNhapExcel.Size = new System.Drawing.Size(132, 23);
            this.btnNhapExcel.TabIndex = 18;
            this.btnNhapExcel.Text = "NHẬP EXEL";
            this.btnNhapExcel.UseVisualStyleBackColor = true;
            this.btnNhapExcel.Click += new System.EventHandler(this.btnNhapExcel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 758);
            this.Controls.Add(this.btnNhapExcel);
            this.Controls.Add(this.btnDangXuat);
            this.Controls.Add(this.btnQuanLyTaiKhoan);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.btnCanhBao);
            this.Controls.Add(this.cbTimKiemDanhMuc);
            this.Controls.Add(this.cbDanhMuc);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.dgvKho);
            this.Controls.Add(this.btnThemMoi);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtSKU);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSKU;
        private System.Windows.Forms.MaskedTextBox txtName;
        private System.Windows.Forms.MaskedTextBox txtQty;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.DataGridView dgvKho;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ComboBox cbDanhMuc;
        private System.Windows.Forms.ComboBox cbTimKiemDanhMuc;
        private System.Windows.Forms.Button btnCanhBao;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnQuanLyTaiKhoan;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Button btnNhapExcel;
    }
}