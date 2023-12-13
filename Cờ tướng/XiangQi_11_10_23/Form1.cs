using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace XiangQi_11_10_23
{
    public partial class Form1 : Form
    {
        private PictureBox[,] banco = new PictureBox[10, 9];

        ////Tạo ra danh sách để lưu trữ các vị trí mà quân cờ được chọn có thể di chuyển đến
        ////Và hiển thị chúng thông qua việc ẩn/hiện Highlights
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        private GameState gameState;
        private Position VitriDuocChon = null;
        
        public Form1()
        {
            InitializeComponent();
            NewGame();
            this.FormClosing += Form1_FormClosing;
        }

        // Để tắt cả Form NewGame
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Khi Form1 đóng, đóng cả hai form
            NewGame newGameForm = Application.OpenForms.OfType<NewGame>().FirstOrDefault();
            if (newGameForm != null)
            {
                newGameForm.Close();
            }
        }
        public void NewGame()
        {
            TaoBanCo();
            gameState = new GameState(NguoiChoi.Do, BanCo.KhoiTao());
            VeBanCo(gameState.Board);
            Luot(gameState.NguoiChoiHienTai);
            txt1.Text = PlayerExtensions.ten_nguoichoi1;
            txt2.Text = PlayerExtensions.ten_nguoichoi2;
            


        }
        //Hàm này sử dụng PictureBox và TableLayoutPanel để hiển thị và quản lý các ô trên bàn cờ.
        //
        public void TaoBanCo()
        {
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 9; c++)
                {

                    PictureBox pictureBox = new PictureBox()//Tạo 1 PictureBox mới
                    {
                        Size = new Size(60, 60),//Điều chỉnh kích thước của PB
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = Color.Transparent,
                        BackgroundImageLayout = ImageLayout.Stretch
                    };
                    banco[r, c] = pictureBox;//Lưu PB vừa tạo vào mảng 2 chiều
                    pictureBox.Click += PictureBox_Click;
                    tlpBanCo.Controls.Add(pictureBox); //Thêm PB vào TableLayoutPanel đã tạo

                }
            }
        }

        ////
        private void VeBanCo(BanCo board)
        {
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    //Cập nhập hình ảnh tương ứng với quân cờ
                    QuanCo piece = board[r, c];
                    banco[r, c].Image = Images.GetImage(piece);
                }
            }
        }


        private void PictureBox_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện khi PictureBox được click
            PictureBox clickedPictureBox = (PictureBox)sender;
            int row = -1, col = -1;

            // Tìm vị trí của PictureBox được click trong mảng
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    if (banco[r, c] == clickedPictureBox)
                    {
                        row = r;
                        col = c;
                        break;
                    }
                }
            }

            if (row != -1 && col != -1)
            {
                Position pos = new Position(row, col);
                //Kiểm tra xem vị trí được chọn có tồn tại quân cờ nào hay không?
                if (VitriDuocChon == null)//Nếu vị trí không tồn tại quân cờ nào
                {
                    VitriBatdauDuocChon(pos);
                }
                else//Ngược lại, vị trí có tồn tại quân cờ
                {
                    VitriKetthucDuocChon(pos);
                }
            }
        }
        ////Phương thức được gọi khi 1 PB được click mà không tồn tại quân cờ nào
        private void VitriBatdauDuocChon(Position pos)
        {
            //Tham số sẽ là PB được click
            //Kiểm tra nước đi hợp lệ cho quân cờ ở vị trí đó
            //Lưu ý: Danh sách sẽ trống nếu click vào 1 PB trống hoặc 1 quân cờ khác phe
            List<Move> moves = gameState.NuocDiHopLeChoQuanCo(pos).ToList();

            //Nếu có ít nhất 1 nước đi hợp lệ
            if (moves.Any())
            {
                VitriDuocChon = pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }


        ////Phương thức được gọi 1 PB được click có tồn tại quân cờ
        private void VitriKetthucDuocChon(Position pos)
        {
            VitriDuocChon = null;
            HideHighlights();

            if (moveCache.TryGetValue(pos, out Move move))
            {
                HandleMove(move);
            }
        }
        ////Phương thức này giúp gameState thực thi nước đi và vẽ lại bàn cờ để phản ánh sự thay đổi 
        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            VeBanCo(gameState.Board);
            Luot(gameState.NguoiChoiHienTai);
            if (gameState.GameOver())
            {
                HienThiMenuGameOver();
            }
            
        }
        //Lấy ra các nước đi hợp lệ của quân cờ đã chọn và lưu chúng vào danh sách
        private void CacheMoves(List<Move> moves)
        {
            moveCache.Clear();

            //Duyệt qua các nước đi 
            foreach (Move move in moves)
            {
                //Với mỗi nước đi, thêm vào danh sách với Key là 'VitriKetThuc' và value là 'move'
                //Nhằm danh sách moveCache duy trì thông tin về các nước đi dựa trên vị trí kết thúc của chúng
                moveCache[move.VitriKetThuc] = move;
            }
        }

        public void ShowHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                //Với mỗi vị trí đến trong danh sách, hiển thị 
                banco[to.Dong, to.Cot].BackgroundImage = Properties.Resources.CanMove;
            }
        }

        public void HideHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                banco[to.Dong, to.Cot].BackgroundImage = null;
            }
        }

        private void Luot(NguoiChoi player)
        {
            if(player == NguoiChoi.Do)
            {
                ptb1.BackColor = Color.LightBlue;
                ptb2.BackColor = Color.Azure;



                cauhoa1.Enabled = true;
                chiuthua1.Enabled = true;
                undo1.Enabled = true;

                cauhoa2.Enabled = false;
                chiuthua2.Enabled = false;
                undo2.Enabled = false;
            }
            else if(player == NguoiChoi.Den)
            {
                ptb1.BackColor = Color.Azure;
                ptb2.BackColor = Color.LightBlue;

                cauhoa1.Enabled = false;
                chiuthua1.Enabled = false;
                undo1.Enabled = false;

                cauhoa2.Enabled = true;
                chiuthua2.Enabled = true;
                undo2.Enabled = true;
            }
        }

       
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewGame frm = new NewGame();
            frm.ShowDialog();
        }

        private void HienThiMenuGameOver()
        {
            GameOver gameOverMenu = new GameOver(gameState);
            gameOverMenu.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tmCoolDown_Tick(object sender, EventArgs e)
        {

        }

        private void chiuthua1_Click(object sender, EventArgs e)
        {
            
        }
    }
}



