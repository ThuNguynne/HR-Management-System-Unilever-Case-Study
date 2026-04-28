// ColorScheme.cs - Unified Color Management
using System.Drawing;

namespace UnileverHRM
{
    /// <summary>
    /// Quản lý bảng màu thống nhất cho toàn bộ ứng dụng Unilever HRMS
    /// </summary>
    public static class ColorScheme
    {
        // === PRIMARY COLORS - Màu chủ đạo Unilever ===
        public static readonly Color PrimaryDark = Color.FromArgb(0, 51, 102);        // #003366 - Xanh đậm
        public static readonly Color PrimaryMedium = Color.FromArgb(0, 102, 204);     // #0066CC - Xanh trung
        public static readonly Color PrimaryLight = Color.FromArgb(0, 120, 215);      // #0078D7 - Xanh sáng
        public static readonly Color PrimaryExtraLight = Color.FromArgb(200, 220, 240); // #C8DCF0 - Xanh nhạt

        // === SIDEBAR COLORS - Màu thanh menu ===
        public static readonly Color SidebarBackground = Color.FromArgb(0, 51, 102);   // Nền sidebar
        public static readonly Color SidebarLogoArea = Color.FromArgb(0, 41, 82);      // #002952 - Vùng logo
        public static readonly Color MenuDefault = Color.FromArgb(0, 51, 102);         // Menu mặc định
        public static readonly Color MenuHover = Color.FromArgb(0, 102, 204);          // Menu khi hover
        public static readonly Color MenuActive = Color.FromArgb(0, 120, 215);         // Menu đang active

        // === TEXT COLORS - Màu chữ ===
        public static readonly Color TextPrimary = Color.FromArgb(0, 51, 102);         // Chữ chính
        public static readonly Color TextSecondary = Color.Gray;                        // Chữ phụ
        public static readonly Color TextWhite = Color.White;                           // Chữ trắng
        public static readonly Color TextLight = Color.FromArgb(200, 220, 240);        // Chữ nhạt

        // === BACKGROUND COLORS - Màu nền ===
        public static readonly Color BackgroundWhite = Color.White;                     // Nền trắng
        public static readonly Color BackgroundLight = Color.FromArgb(245, 247, 250);  // #F5F7FA - Nền sáng
        public static readonly Color BackgroundGray = Color.WhiteSmoke;                 // Nền xám nhẹ

        // === BORDER COLORS - Màu viền ===
        public static readonly Color BorderPrimary = Color.FromArgb(0, 102, 204);      // Viền chính
        public static readonly Color BorderLight = Color.FromArgb(220, 220, 220);      // #DCDCDC - Viền nhẹ
        public static readonly Color BorderGray = Color.LightGray;                      // Viền xám

        // === STATUS COLORS - Màu trạng thái ===
        public static readonly Color Success = Color.FromArgb(40, 167, 69);            // #28A745 - Thành công
        public static readonly Color Warning = Color.FromArgb(255, 193, 7);            // #FFC107 - Cảnh báo
        public static readonly Color Danger = Color.FromArgb(220, 53, 69);             // #DC3545 - Nguy hiểm
        public static readonly Color Info = Color.FromArgb(23, 162, 184);              // #17A2B8 - Thông tin

        // === DATAGRID COLORS - Màu bảng dữ liệu ===
        public static readonly Color GridHeaderBackground = Color.FromArgb(0, 102, 204);
        public static readonly Color GridHeaderForeground = Color.White;
        public static readonly Color GridAlternatingRowBackground = Color.FromArgb(240, 248, 255); // #F0F8FF - AliceBlue
        public static readonly Color GridSelectionBackground = Color.FromArgb(0, 120, 215);
        public static readonly Color GridSelectionForeground = Color.White;

        // === BUTTON COLORS - Màu nút ===
        public static readonly Color ButtonPrimary = Color.FromArgb(0, 102, 204);
        public static readonly Color ButtonPrimaryHover = Color.FromArgb(0, 120, 215);
        public static readonly Color ButtonSuccess = Color.FromArgb(40, 167, 69);
        public static readonly Color ButtonSuccessHover = Color.FromArgb(34, 142, 58);
        public static readonly Color ButtonDanger = Color.FromArgb(220, 53, 69);
        public static readonly Color ButtonDangerHover = Color.FromArgb(200, 35, 51);
        public static readonly Color ButtonWarning = Color.FromArgb(255, 193, 7);
        public static readonly Color ButtonWarningHover = Color.FromArgb(224, 169, 6);

        /// <summary>
        /// Áp dụng theme màu Unilever cho DataGridView
        /// </summary>
        public static void ApplyDataGridViewTheme(System.Windows.Forms.DataGridView grid)
        {
            // Header
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeaderBackground;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = GridHeaderForeground;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            grid.ColumnHeadersHeight = 40;

            // Rows
            grid.AlternatingRowsDefaultCellStyle.BackColor = GridAlternatingRowBackground;
            grid.DefaultCellStyle.SelectionBackColor = GridSelectionBackground;
            grid.DefaultCellStyle.SelectionForeColor = GridSelectionForeground;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            grid.RowTemplate.Height = 35;

            // Grid appearance
            grid.BackgroundColor = BackgroundWhite;
            grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = BorderLight;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AllowUserToAddRows = false;
        }

        /// <summary>
        /// Tạo button với style Unilever
        /// </summary>
        public static void StyleButton(System.Windows.Forms.Button button, ButtonStyle style = ButtonStyle.Primary)
        {
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = System.Windows.Forms.Cursors.Hand;
            button.Height = 40;
            button.ForeColor = TextWhite;

            switch (style)
            {
                case ButtonStyle.Primary:
                    button.BackColor = ButtonPrimary;
                    button.FlatAppearance.MouseOverBackColor = ButtonPrimaryHover;
                    break;
                case ButtonStyle.Success:
                    button.BackColor = ButtonSuccess;
                    button.FlatAppearance.MouseOverBackColor = ButtonSuccessHover;
                    break;
                case ButtonStyle.Danger:
                    button.BackColor = ButtonDanger;
                    button.FlatAppearance.MouseOverBackColor = ButtonDangerHover;
                    break;
                case ButtonStyle.Warning:
                    button.BackColor = ButtonWarning;
                    button.FlatAppearance.MouseOverBackColor = ButtonWarningHover;
                    button.ForeColor = TextPrimary; // Chữ đen cho nền vàng
                    break;
            }
        }

        /// <summary>
        /// Tạo panel với shadow effect
        /// </summary>
        public static System.Windows.Forms.Panel CreateCardPanel()
        {
            var panel = new System.Windows.Forms.Panel
            {
                BackColor = BackgroundWhite,
                Padding = new System.Windows.Forms.Padding(20)
            };
            return panel;
        }
    }

    /// <summary>
    /// Enum cho các kiểu button
    /// </summary>
    public enum ButtonStyle
    {
        Primary,
        Success,
        Danger,
        Warning
    }
}