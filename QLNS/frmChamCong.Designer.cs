// frmChamCong.Designer.cs
using System;
using System.Windows.Forms;
using System.Drawing;

namespace UnileverHRM
{
    partial class frmChamCong
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
            this.panelControl = new System.Windows.Forms.Panel();
            this.btnXemThang = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.dtpNgayChamCong = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvChamCong = new System.Windows.Forms.DataGridView();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lblDiMuon = new System.Windows.Forms.Label();
            this.lblVang = new System.Windows.Forms.Label();
            this.lblDiLam = new System.Windows.Forms.Label();
            this.lblTongNV = new System.Windows.Forms.Label();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong)).BeginInit();
            this.panelSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(113)))));
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(248, 30);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "THEO DÕI CHẤM CÔNG";
            // 
            // panelControl
            // 
            this.panelControl.BackColor = System.Drawing.Color.White;
            this.panelControl.Controls.Add(this.btnXemThang);
            this.panelControl.Controls.Add(this.btnLamMoi);
            this.panelControl.Controls.Add(this.btnCheckOut);
            this.panelControl.Controls.Add(this.btnCheckIn);
            this.panelControl.Controls.Add(this.dtpNgayChamCong);
            this.panelControl.Controls.Add(this.label1);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 50);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(984, 70);
            this.panelControl.TabIndex = 1;
            // 
            // btnXemThang
            // 
            this.btnXemThang.BackColor = System.Drawing.Color.SteelBlue;
            this.btnXemThang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemThang.ForeColor = System.Drawing.Color.White;
            this.btnXemThang.Location = new System.Drawing.Point(600, 15);
            this.btnXemThang.Name = "btnXemThang";
            this.btnXemThang.Size = new System.Drawing.Size(120, 35);
            this.btnXemThang.TabIndex = 5;
            this.btnXemThang.Text = "Xem lịch sử tháng";
            this.btnXemThang.UseVisualStyleBackColor = false;
            this.btnXemThang.Click += new System.EventHandler(this.btnXemThang_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.Gray;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(740, 15);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(80, 35);
            this.btnLamMoi.TabIndex = 4;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.BackColor = System.Drawing.Color.Firebrick;
            this.btnCheckOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCheckOut.ForeColor = System.Drawing.Color.White;
            this.btnCheckOut.Location = new System.Drawing.Point(450, 15);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(130, 35);
            this.btnCheckOut.TabIndex = 3;
            this.btnCheckOut.Text = "Check-Out (Ra về)";
            this.btnCheckOut.UseVisualStyleBackColor = false;
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.BackColor = System.Drawing.Color.ForestGreen;
            this.btnCheckIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCheckIn.ForeColor = System.Drawing.Color.White;
            this.btnCheckIn.Location = new System.Drawing.Point(300, 15);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(130, 35);
            this.btnCheckIn.TabIndex = 2;
            this.btnCheckIn.Text = "Check-In (Vào làm)";
            this.btnCheckIn.UseVisualStyleBackColor = false;
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // dtpNgayChamCong
            // 
            this.dtpNgayChamCong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpNgayChamCong.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayChamCong.Location = new System.Drawing.Point(110, 20);
            this.dtpNgayChamCong.Name = "dtpNgayChamCong";
            this.dtpNgayChamCong.Size = new System.Drawing.Size(150, 23);
            this.dtpNgayChamCong.TabIndex = 1;
            this.dtpNgayChamCong.ValueChanged += new System.EventHandler(this.dtpNgayChamCong_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày làm việc:";
            // 
            // dgvChamCong
            // 
            this.dgvChamCong.AllowUserToAddRows = false;
            this.dgvChamCong.AllowUserToDeleteRows = false;
            this.dgvChamCong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChamCong.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvChamCong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChamCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChamCong.Location = new System.Drawing.Point(0, 120);
            this.dgvChamCong.Name = "dgvChamCong";
            this.dgvChamCong.ReadOnly = true;
            this.dgvChamCong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChamCong.Size = new System.Drawing.Size(984, 391);
            this.dgvChamCong.TabIndex = 2;
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.AliceBlue;
            this.panelSummary.Controls.Add(this.lblDiMuon);
            this.panelSummary.Controls.Add(this.lblVang);
            this.panelSummary.Controls.Add(this.lblDiLam);
            this.panelSummary.Controls.Add(this.lblTongNV);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSummary.Location = new System.Drawing.Point(0, 511);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(984, 50);
            this.panelSummary.TabIndex = 3;
            // 
            // lblDiMuon
            // 
            this.lblDiMuon.AutoSize = true;
            this.lblDiMuon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiMuon.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblDiMuon.Location = new System.Drawing.Point(500, 15);
            this.lblDiMuon.Name = "lblDiMuon";
            this.lblDiMuon.Size = new System.Drawing.Size(65, 15);
            this.lblDiMuon.TabIndex = 3;
            this.lblDiMuon.Text = "Đi muộn: 0";
            // 
            // lblVang
            // 
            this.lblVang.AutoSize = true;
            this.lblVang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVang.ForeColor = System.Drawing.Color.Red;
            this.lblVang.Location = new System.Drawing.Point(350, 15);
            this.lblVang.Name = "lblVang";
            this.lblVang.Size = new System.Drawing.Size(71, 15);
            this.lblVang.TabIndex = 2;
            this.lblVang.Text = "Vắng mặt: 0";
            // 
            // lblDiLam
            // 
            this.lblDiLam.AutoSize = true;
            this.lblDiLam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiLam.ForeColor = System.Drawing.Color.Green;
            this.lblDiLam.Location = new System.Drawing.Point(180, 15);
            this.lblDiLam.Name = "lblDiLam";
            this.lblDiLam.Size = new System.Drawing.Size(81, 15);
            this.lblDiLam.TabIndex = 1;
            this.lblDiLam.Text = "Đã Check-in: 0";
            // 
            // lblTongNV
            // 
            this.lblTongNV.AutoSize = true;
            this.lblTongNV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongNV.Location = new System.Drawing.Point(30, 15);
            this.lblTongNV.Name = "lblTongNV";
            this.lblTongNV.Size = new System.Drawing.Size(101, 15);
            this.lblTongNV.TabIndex = 0;
            this.lblTongNV.Text = "Tổng nhân viên: 0";
            // 
            // frmChamCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.dgvChamCong);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.labelTitle);
            this.Name = "frmChamCong";
            this.Padding = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.Text = "Chấm công";
            this.Load += new System.EventHandler(this.frmChamCong_Load);
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong)).EndInit();
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Button btnCheckOut;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.DateTimePicker dtpNgayChamCong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvChamCong;
        private System.Windows.Forms.Button btnXemThang;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label lblTongNV;
        private System.Windows.Forms.Label lblDiMuon;
        private System.Windows.Forms.Label lblVang;
        private System.Windows.Forms.Label lblDiLam;
    }
}
