namespace UnileverHRM
{
    partial class Dashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();

            // Chúng ta XÓA Sidebar và Header ở đây, vì frmMain đã lo việc đó
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panelChart = new System.Windows.Forms.Panel();
            this.chartThongKe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblChartTitle = new System.Windows.Forms.Label();
            this.flowPanelCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCard1 = new System.Windows.Forms.Panel();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.pnlCard2 = new System.Windows.Forms.Panel();
            this.lblValue2 = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.pnlCard3 = new System.Windows.Forms.Panel();
            this.lblValue3 = new System.Windows.Forms.Label();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.pnlCard4 = new System.Windows.Forms.Panel();
            this.lblValue4 = new System.Windows.Forms.Label();
            this.lblTitle4 = new System.Windows.Forms.Label();

            this.pnlMain.SuspendLayout();
            this.panelChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKe)).BeginInit();
            this.flowPanelCards.SuspendLayout();
            this.pnlCard1.SuspendLayout();
            this.pnlCard2.SuspendLayout();
            this.pnlCard3.SuspendLayout();
            this.pnlCard4.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlMain.Controls.Add(this.panelChart);
            this.pnlMain.Controls.Add(this.flowPanelCards);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill; // Lấp đầy form cha
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(964, 601);
            this.pnlMain.TabIndex = 2;

            // 
            // panelChart
            // 
            this.panelChart.BackColor = System.Drawing.Color.White;
            this.panelChart.Controls.Add(this.chartThongKe);
            this.panelChart.Controls.Add(this.lblChartTitle);
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelChart.Location = new System.Drawing.Point(20, 170);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(924, 400);
            this.panelChart.TabIndex = 1;

            // 
            // chartThongKe
            // 
            chartArea1.Name = "ChartArea1";
            this.chartThongKe.ChartAreas.Add(chartArea1);
            this.chartThongKe.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend1.Name = "Legend1";
            this.chartThongKe.Legends.Add(legend1);
            this.chartThongKe.Location = new System.Drawing.Point(0, 50);
            this.chartThongKe.Name = "chartThongKe";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartThongKe.Series.Add(series1);
            this.chartThongKe.Size = new System.Drawing.Size(924, 350);
            this.chartThongKe.TabIndex = 1;
            this.chartThongKe.Text = "chart1";

            // 
            // lblChartTitle
            // 
            this.lblChartTitle.AutoSize = true;
            this.lblChartTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblChartTitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblChartTitle.Location = new System.Drawing.Point(20, 15);
            this.lblChartTitle.Name = "lblChartTitle";
            this.lblChartTitle.Size = new System.Drawing.Size(155, 21);
            this.lblChartTitle.TabIndex = 0;
            this.lblChartTitle.Text = "Biểu đồ thống kê";

            // 
            // flowPanelCards
            // 
            this.flowPanelCards.Controls.Add(this.pnlCard1);
            this.flowPanelCards.Controls.Add(this.pnlCard2);
            this.flowPanelCards.Controls.Add(this.pnlCard3);
            this.flowPanelCards.Controls.Add(this.pnlCard4);
            this.flowPanelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowPanelCards.Location = new System.Drawing.Point(20, 20);
            this.flowPanelCards.Name = "flowPanelCards";
            this.flowPanelCards.Size = new System.Drawing.Size(924, 150);
            this.flowPanelCards.TabIndex = 0;

            // 
            // pnlCard1, 2, 3, 4 (Giữ nguyên logic như cũ, chỉ chỉnh lại kích thước nếu cần)
            // 
            this.pnlCard1.BackColor = System.Drawing.Color.White;
            this.pnlCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard1.Controls.Add(this.lblValue1);
            this.pnlCard1.Controls.Add(this.lblTitle1);
            this.pnlCard1.Location = new System.Drawing.Point(3, 3);
            this.pnlCard1.Name = "pnlCard1";
            this.pnlCard1.Size = new System.Drawing.Size(220, 120);
            this.pnlCard1.TabIndex = 0;

            this.lblValue1.AutoSize = true;
            this.lblValue1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValue1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.lblValue1.Location = new System.Drawing.Point(15, 45);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(49, 37);
            this.lblValue1.TabIndex = 1;
            this.lblValue1.Text = "0";

            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTitle1.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle1.Location = new System.Drawing.Point(15, 15);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(101, 19);
            this.lblTitle1.TabIndex = 0;
            this.lblTitle1.Text = "Tổng nhân viên";

            // Tương tự cho Card 2, 3, 4 (Copy paste và đổi tên biến)
            // Card 2
            this.pnlCard2.BackColor = System.Drawing.Color.White;
            this.pnlCard2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard2.Controls.Add(this.lblValue2);
            this.pnlCard2.Controls.Add(this.lblTitle2);
            this.pnlCard2.Location = new System.Drawing.Point(229, 3);
            this.pnlCard2.Name = "pnlCard2";
            this.pnlCard2.Size = new System.Drawing.Size(220, 120);
            this.pnlCard2.TabIndex = 1;

            this.lblValue2.AutoSize = true;
            this.lblValue2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValue2.ForeColor = System.Drawing.Color.Green;
            this.lblValue2.Location = new System.Drawing.Point(15, 45);
            this.lblValue2.Name = "lblValue2";
            this.lblValue2.Text = "0";

            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTitle2.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle2.Location = new System.Drawing.Point(15, 15);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Text = "Ứng viên mới";

            // Card 3
            this.pnlCard3.BackColor = System.Drawing.Color.White;
            this.pnlCard3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard3.Controls.Add(this.lblValue3);
            this.pnlCard3.Controls.Add(this.lblTitle3);
            this.pnlCard3.Location = new System.Drawing.Point(455, 3);
            this.pnlCard3.Name = "pnlCard3";
            this.pnlCard3.Size = new System.Drawing.Size(220, 120);
            this.pnlCard3.TabIndex = 2;

            this.lblValue3.AutoSize = true;
            this.lblValue3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValue3.ForeColor = System.Drawing.Color.Orange;
            this.lblValue3.Location = new System.Drawing.Point(15, 45);
            this.lblValue3.Name = "lblValue3";
            this.lblValue3.Text = "0";

            this.lblTitle3.AutoSize = true;
            this.lblTitle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTitle3.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle3.Location = new System.Drawing.Point(15, 15);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Text = "Nghỉ việc";

            // Card 4
            this.pnlCard4.BackColor = System.Drawing.Color.White;
            this.pnlCard4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard4.Controls.Add(this.lblValue4);
            this.pnlCard4.Controls.Add(this.lblTitle4);
            this.pnlCard4.Location = new System.Drawing.Point(681, 3);
            this.pnlCard4.Name = "pnlCard4";
            this.pnlCard4.Size = new System.Drawing.Size(220, 120);
            this.pnlCard4.TabIndex = 3;

            this.lblValue4.AutoSize = true;
            this.lblValue4.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold); // Giảm font xíu để vừa số tiền
            this.lblValue4.ForeColor = System.Drawing.Color.Red;
            this.lblValue4.Location = new System.Drawing.Point(15, 48);
            this.lblValue4.Name = "lblValue4";
            this.lblValue4.Text = "0";

            this.lblTitle4.AutoSize = true;
            this.lblTitle4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTitle4.ForeColor = System.Drawing.Color.Gray;
            this.lblTitle4.Location = new System.Drawing.Point(15, 15);
            this.lblTitle4.Name = "lblTitle4";
            this.lblTitle4.Text = "Quỹ lương tháng";

            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 601);
            this.Controls.Add(this.pnlMain);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.pnlMain.ResumeLayout(false);
            this.panelChart.ResumeLayout(false);
            this.panelChart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKe)).EndInit();
            this.flowPanelCards.ResumeLayout(false);
            this.pnlCard1.ResumeLayout(false);
            this.pnlCard1.PerformLayout();
            this.pnlCard2.ResumeLayout(false);
            this.pnlCard2.PerformLayout();
            this.pnlCard3.ResumeLayout(false);
            this.pnlCard3.PerformLayout();
            this.pnlCard4.ResumeLayout(false);
            this.pnlCard4.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.FlowLayoutPanel flowPanelCards;
        private System.Windows.Forms.Panel pnlCard1;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Panel pnlCard2;
        private System.Windows.Forms.Label lblValue2;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Panel pnlCard3;
        private System.Windows.Forms.Label lblValue3;
        private System.Windows.Forms.Label lblTitle3;
        private System.Windows.Forms.Panel pnlCard4;
        private System.Windows.Forms.Label lblValue4;
        private System.Windows.Forms.Label lblTitle4;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartThongKe;
        private System.Windows.Forms.Label lblChartTitle;
    }
}