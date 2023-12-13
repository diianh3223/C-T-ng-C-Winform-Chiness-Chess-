using System;
using System.Linq;
using System.Windows.Forms;

namespace XiangQi_11_10_23
{
    public partial class GameOver : Form
    {
        public GameOver(GameState gamestate)
        {
            InitializeComponent();
            KetQua rs = gamestate.ketqua;
            txtnguoichoichienthang.Text = GetWinnerText(rs.Winner);
            
        }

        private static string GetWinnerText(NguoiChoi winner)
        {
            switch (winner)
            {
                case NguoiChoi.Do:
                    return "Nguoi Choi "+ PlayerExtensions.ten_nguoichoi1 + " Thang!!!";
                case NguoiChoi.Den:
                    return "Nguoi Choi "+ PlayerExtensions.ten_nguoichoi2 + " Thang!!!";
                case NguoiChoi.None:
                    return "Hoa";
                default:
                    return null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.Close();
            // Kiểm tra xem có thể một thể hiện của Form1 đang mở không
            Form1 existingForm = Application.OpenForms.OfType<Form1>().FirstOrDefault();

            if (existingForm != null)
            {
                existingForm.Close(); //Đóng thể hiện của Form1 nếu nó đang mở
            }

            Form1 frm = new Form1();
            frm.ShowDialog();
        }
    }
}
