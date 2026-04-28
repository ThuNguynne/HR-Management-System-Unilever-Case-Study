namespace UnileverHRM
{
    partial class frmNhanVien
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
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.panelInput = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpNgayVaoLam = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLuongCoBan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboChucVu = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPhongBan = new System.Windows.Forms.ComboBox();
            this.txtCMND = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnInThe = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            this.panelInput.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            this.SuspendLayout();

            // dgvNhanVien
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhanVien.Location = new System.Drawing.Point(0, 270);
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new System.Drawing.Size(984, 291);
            this.dgvNhanVien.TabIndex = 2;
            this.dgvNhanVien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhanVien_CellClick);

            // panelInput
            this.panelInput.BackColor = System.Drawing.Color.White;
            this.panelInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInput.Controls.Add(this.label11);
            this.panelInput.Controls.Add(this.dtpNgayVaoLam);
            this.panelInput.Controls.Add(this.label10);
            this.panelInput.Controls.Add(this.txtDiaChi);
            this.panelInput.Controls.Add(this.label9);
            this.panelInput.Controls.Add(this.txtEmail);
            this.panelInput.Controls.Add(this.label8);
            this.panelInput.Controls.Add(this.txtDienThoai);
            this.panelInput.Controls.Add(this.label7);
            this.panelInput.Controls.Add(this.cboGioiTinh);
            this.panelInput.Controls.Add(this.label6);
            this.panelInput.Controls.Add(this.txtLuongCoBan);
            this.panelInput.Controls.Add(this.label5);
            this.panelInput.Controls.Add(this.dtpNgaySinh);
            this.panelInput.Controls.Add(this.btnLamMoi);
            this.panelInput.Controls.Add(this.btnThem);
            this.panelInput.Controls.Add(this.label4);
            this.panelInput.Controls.Add(this.cboChucVu);
            this.panelInput.Controls.Add(this.label3);
            this.panelInput.Controls.Add(this.cboPhongBan);
            this.panelInput.Controls.Add(this.txtCMND);
            this.panelInput.Controls.Add(this.label2);
            this.panelInput.Controls.Add(this.txtHoTen);
            this.panelInput.Controls.Add(this.label1);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInput.Location = new System.Drawing.Point(0, 50);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(984, 170);
            this.panelInput.TabIndex = 1;

            // Các control trong panelInput (Giữ nguyên như code bạn cung cấp)
            this.label11.AutoSize = true; this.label11.Location = new System.Drawing.Point(720, 48); this.label11.Text = "Vào làm:";
            this.dtpNgayVaoLam.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpNgayVaoLam.Location = new System.Drawing.Point(780, 45); this.dtpNgayVaoLam.Size = new System.Drawing.Size(150, 20);
            this.label10.AutoSize = true; this.label10.Location = new System.Drawing.Point(480, 83); this.label10.Text = "Địa chỉ:";
            this.txtDiaChi.Location = new System.Drawing.Point(550, 80); this.txtDiaChi.Size = new System.Drawing.Size(380, 20);
            this.label9.AutoSize = true; this.label9.Location = new System.Drawing.Point(250, 83); this.label9.Text = "Email:";
            this.txtEmail.Location = new System.Drawing.Point(310, 80); this.txtEmail.Size = new System.Drawing.Size(150, 20);
            this.label8.AutoSize = true; this.label8.Location = new System.Drawing.Point(15, 83); this.label8.Text = "Điện thoại:";
            this.txtDienThoai.Location = new System.Drawing.Point(80, 80); this.txtDienThoai.Size = new System.Drawing.Size(150, 20);
            this.label7.AutoSize = true; this.label7.Location = new System.Drawing.Point(250, 48); this.label7.Text = "Giới tính:";
            this.cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" }); this.cboGioiTinh.Location = new System.Drawing.Point(310, 45); this.cboGioiTinh.Size = new System.Drawing.Size(150, 21);
            this.label6.AutoSize = true; this.label6.Location = new System.Drawing.Point(470, 48); this.label6.Text = "Lương CB:";
            this.txtLuongCoBan.Location = new System.Drawing.Point(550, 45); this.txtLuongCoBan.Size = new System.Drawing.Size(150, 20); this.txtLuongCoBan.Text = "0";
            this.label5.AutoSize = true; this.label5.Location = new System.Drawing.Point(15, 48); this.label5.Text = "Ngày sinh:";
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpNgaySinh.Location = new System.Drawing.Point(80, 45); this.dtpNgaySinh.Size = new System.Drawing.Size(150, 20);

            this.btnLamMoi.BackColor = System.Drawing.Color.Gray; this.btnLamMoi.Text = "Làm mới"; this.btnLamMoi.Location = new System.Drawing.Point(860, 120); this.btnLamMoi.Size = new System.Drawing.Size(80, 30); this.btnLamMoi.ForeColor = System.Drawing.Color.White; this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(0, 59, 113); this.btnThem.Text = "+ Thêm NV"; this.btnThem.Location = new System.Drawing.Point(750, 120); this.btnThem.Size = new System.Drawing.Size(100, 30); this.btnThem.ForeColor = System.Drawing.Color.White; this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.label4.AutoSize = true; this.label4.Location = new System.Drawing.Point(720, 15); this.label4.Text = "Chức vụ:";
            this.cboChucVu.Location = new System.Drawing.Point(780, 12); this.cboChucVu.Size = new System.Drawing.Size(150, 21);
            this.label3.AutoSize = true; this.label3.Location = new System.Drawing.Point(480, 15); this.label3.Text = "Phòng ban:";
            this.cboPhongBan.Location = new System.Drawing.Point(550, 12); this.cboPhongBan.Size = new System.Drawing.Size(150, 21);
            this.txtCMND.Location = new System.Drawing.Point(310, 12); this.txtCMND.Size = new System.Drawing.Size(150, 20);
            this.label2.AutoSize = true; this.label2.Location = new System.Drawing.Point(250, 15); this.label2.Text = "CMND:";
            this.txtHoTen.Location = new System.Drawing.Point(80, 12); this.txtHoTen.Size = new System.Drawing.Size(150, 20);
            this.label1.AutoSize = true; this.label1.Location = new System.Drawing.Point(15, 15); this.label1.Text = "Họ Tên:";

            // labelTitle
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(113)))));
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(242, 30);
            this.labelTitle.Text = "QUẢN LÝ NHÂN VIÊN";

            // panelToolbar
            this.panelToolbar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelToolbar.Controls.Add(this.btnInThe);
            this.panelToolbar.Controls.Add(this.btnXuatExcel);
            this.panelToolbar.Controls.Add(this.btnTimKiem);
            this.panelToolbar.Controls.Add(this.txtTimKiem);
            this.panelToolbar.Controls.Add(this.label12);
            this.panelToolbar.Controls.Add(this.btnXoa);
            this.panelToolbar.Controls.Add(this.btnSua);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Location = new System.Drawing.Point(0, 220);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(984, 50);
            this.panelToolbar.TabIndex = 3;

            // Các control trong Toolbar
            this.btnInThe.BackColor = System.Drawing.Color.Teal; this.btnInThe.Text = "In thẻ NV"; this.btnInThe.Location = new System.Drawing.Point(820, 10); this.btnInThe.Size = new System.Drawing.Size(100, 30); this.btnInThe.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.BackColor = System.Drawing.Color.Green; this.btnXuatExcel.Text = "Xuất Excel"; this.btnXuatExcel.Location = new System.Drawing.Point(710, 10); this.btnXuatExcel.Size = new System.Drawing.Size(100, 30); this.btnXuatExcel.ForeColor = System.Drawing.Color.White; this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(0, 59, 113); this.btnTimKiem.Text = "Tìm"; this.btnTimKiem.Location = new System.Drawing.Point(580, 10); this.btnTimKiem.Size = new System.Drawing.Size(80, 28); this.btnTimKiem.ForeColor = System.Drawing.Color.White; this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            this.txtTimKiem.Location = new System.Drawing.Point(380, 13); this.txtTimKiem.Size = new System.Drawing.Size(190, 23);
            this.label12.AutoSize = true; this.label12.Location = new System.Drawing.Point(315, 16); this.label12.Text = "Tìm kiếm:";
            this.btnXoa.BackColor = System.Drawing.Color.Firebrick; this.btnXoa.Text = "Cho nghỉ việc"; this.btnXoa.Location = new System.Drawing.Point(120, 10); this.btnXoa.Size = new System.Drawing.Size(100, 30); this.btnXoa.ForeColor = System.Drawing.Color.White; this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            this.btnSua.BackColor = System.Drawing.Color.Orange; this.btnSua.Text = "Sửa thông tin"; this.btnSua.Location = new System.Drawing.Point(10, 10); this.btnSua.Size = new System.Drawing.Size(100, 30); this.btnSua.ForeColor = System.Drawing.Color.White; this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.dgvNhanVien);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.labelTitle);
            this.Name = "frmNhanVien";
            this.Text = "Quản lý nhân viên";
            this.Padding = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.Load += new System.EventHandler(this.frmNhanVien_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.DataGridView dgvNhanVien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCMND;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboPhongBan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboChucVu;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLuongCoBan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpNgayVaoLam;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnInThe;
    }
}