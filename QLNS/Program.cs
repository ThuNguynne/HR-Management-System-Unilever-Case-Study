using System;
using System.Windows.Forms;

namespace UnileverHRM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Chạy Form Main mới (bỏ qua Login tạm thời để test)
            Application.Run(new frmLogin());

            // Nếu có form login:
            // Application.Run(new FrmDangNhap());
        }
    }
}