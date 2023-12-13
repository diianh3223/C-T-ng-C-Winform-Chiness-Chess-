using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XiangQi_11_10_23
{
    public partial class NewGame : Form
    {
        public NewGame()
        {
            InitializeComponent();

        }

        private void BatDau_Click(object sender, EventArgs e)
        {
            
            if (QuanDo.Text != "" && QuanDen.Text != "")
            {
                PlayerExtensions.ten_nguoichoi1 = QuanDo.Text;
                PlayerExtensions.ten_nguoichoi2 = QuanDen.Text;
                QuanDen.Text = "";
                QuanDo.Text = "";
                Hide();
                Form1 frm = new Form1();
                frm.Show();
                
            }
            else MessageBox.Show("Nhap ten 2 nguoi choi");
        }
        
    }
}
