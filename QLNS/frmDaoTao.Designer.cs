namespace UnileverHRM
{
    partial class frmDaoTao
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelInput = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnDangKyHV = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dtpNgayKetThuc = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDiaDiem = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGiangVien = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenKhoaHoc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDaoTao = new System.Windows.Forms.DataGridView();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblDangMo = new System.Windows.Forms.Label();
            this.lblTongKhoa = new System.Windows.Forms.Label();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaoTao)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(113)))));
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(329, 30);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "QUẢN LÝ ĐÀO TẠO & PHÁT TRIỂN";
            // 
            // panelInput
            // 
            this.panelInput.BackColor = System.Drawing.Color.White;
            this.panelInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInput.Controls.Add(this.label6);
            this.panelInput.Controls.Add(this.numSoLuong);
            this.panelInput.Controls.Add(this.btnDangKyHV);
            this.panelInput.Controls.Add(this.btnLamMoi);
            this.panelInput.Controls.Add(this.btnXoa);
            this.panelInput.Controls.Add(this.btnThem);
            this.panelInput.Controls.Add(this.dtpNgayKetThuc);
            this.panelInput.Controls.Add(this.label5);
            this.panelInput.Controls.Add(this.dtpNgayBatDau);
            this.panelInput.Controls.Add(this.label4);
            this.panelInput.Controls.Add(this.txtDiaDiem);
            this.panelInput.Controls.Add(this.label3);
            this.panelInput.Controls.Add(this.txtGiangVien);
            this.panelInput.Controls.Add(this.label2);
            this.panelInput.Controls.Add(this.txtTenKhoaHoc);
            this.panelInput.Controls.Add(this.label1);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInput.Location = new System.Drawing.Point(0, 50);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(1000, 160);
            this.panelInput.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(830, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Số lượng:";
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(890, 55);
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(80, 20);
            this.numSoLuong.TabIndex = 14;
            this.numSoLuong.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // btnDangKyHV
            // 
            this.btnDangKyHV.BackColor = System.Drawing.Color.SteelBlue;
            this.btnDangKyHV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKyHV.ForeColor = System.Drawing.Color.White;
            this.btnDangKyHV.Location = new System.Drawing.Point(780, 100);
            this.btnDangKyHV.Name = "btnDangKyHV";
            this.btnDangKyHV.Size = new System.Drawing.Size(120, 35);
            this.btnDangKyHV.TabIndex = 13;
            this.btnDangKyHV.Text = "Danh sách học viên";
            this.btnDangKyHV.UseVisualStyleBackColor = false;
            this.btnDangKyHV.Click += new System.EventHandler(this.btnDangKyHV_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.Gray;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(910, 100);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(80, 35);
            this.btnLamMoi.TabIndex = 12;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.Firebrick;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(670, 100);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 35);
            this.btnXoa.TabIndex = 11;
            this.btnXoa.Text = "Xóa khóa học";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(113)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(540, 100);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(120, 35);
            this.btnThem.TabIndex = 10;
            this.btnThem.Text = "+ Thêm khóa học";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dtpNgayKetThuc
            // 
            this.dtpNgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(700, 55);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(120, 20);
            this.dtpNgayKetThuc.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(620, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ngày kết thúc:";
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(450, 55);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(120, 20);
            this.dtpNgayBatDau.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(370, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ngày bắt đầu:";
            // 
            // txtDiaDiem
            // 
            this.txtDiaDiem.Location = new System.Drawing.Point(120, 55);
            this.txtDiaDiem.Name = "txtDiaDiem";
            this.txtDiaDiem.Size = new System.Drawing.Size(220, 20);
            this.txtDiaDiem.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Địa điểm:";
            // 
            // txtGiangVien
            // 
            this.txtGiangVien.Location = new System.Drawing.Point(450, 20);
            this.txtGiangVien.Name = "txtGiangVien";
            this.txtGiangVien.Size = new System.Drawing.Size(200, 20);
            this.txtGiangVien.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Giảng viên:";
            // 
            // txtTenKhoaHoc
            // 
            this.txtTenKhoaHoc.Location = new System.Drawing.Point(120, 20);
            this.txtTenKhoaHoc.Name = "txtTenKhoaHoc";
            this.txtTenKhoaHoc.Size = new System.Drawing.Size(220, 20);
            this.txtTenKhoaHoc.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên khóa học:";
            // 
            // dgvDaoTao
            // 
            this.dgvDaoTao.AllowUserToAddRows = false;
            this.dgvDaoTao.AllowUserToDeleteRows = false;
            this.dgvDaoTao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDaoTao.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDaoTao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDaoTao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDaoTao.Location = new System.Drawing.Point(0, 210);
            this.dgvDaoTao.Name = "dgvDaoTao";
            this.dgvDaoTao.ReadOnly = true;
            this.dgvDaoTao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDaoTao.Size = new System.Drawing.Size(1000, 340);
            this.dgvDaoTao.TabIndex = 2;
            this.dgvDaoTao.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDaoTao_CellClick);
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFooter.Controls.Add(this.lblDangMo);
            this.panelFooter.Controls.Add(this.lblTongKhoa);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 550);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1000, 50);
            this.panelFooter.TabIndex = 3;
            // 
            // lblDangMo
            // 
            this.lblDangMo.AutoSize = true;
            this.lblDangMo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDangMo.ForeColor = System.Drawing.Color.Green;
            this.lblDangMo.Location = new System.Drawing.Point(180, 18);
            this.lblDangMo.Name = "lblDangMo";
            this.lblDangMo.Size = new System.Drawing.Size(72, 15);
            this.lblDangMo.TabIndex = 1;
            this.lblDangMo.Text = "Đang mở: 0";
            // 
            // lblTongKhoa
            // 
            this.lblTongKhoa.AutoSize = true;
            this.lblTongKhoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongKhoa.Location = new System.Drawing.Point(30, 18);
            this.lblTongKhoa.Name = "lblTongKhoa";
            this.lblTongKhoa.Size = new System.Drawing.Size(102, 15);
            this.lblTongKhoa.TabIndex = 0;
            this.lblTongKhoa.Text = "Tổng khóa học: 0";
            // 
            // frmDaoTao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvDaoTao);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.labelTitle);
            this.Name = "frmDaoTao";
            this.Padding = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.Text = "Quản lý đào tạo";
            this.Load += new System.EventHandler(this.frmDaoTao_Load);
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaoTao)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.DataGridView dgvDaoTao;
        private System.Windows.Forms.TextBox txtTenKhoaHoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGiangVien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDiaDiem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpNgayKetThuc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDangKyHV;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label lblTongKhoa;
        private System.Windows.Forms.Label lblDangMo;
    }
}