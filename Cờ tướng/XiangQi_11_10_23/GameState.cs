using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XiangQi_11_10_23
{
    //Lưu trữ thông tin về trạng thái hiện tại của trò chơi cờ tướng, bao gồm bàn cờ và người chơi đang thực hiện lượt đi.
    public class GameState
    {
        public BanCo Board { get; }
        public NguoiChoi NguoiChoiHienTai { get; private set; }

        public KetQua ketqua { get; private set; } = null;
        public GameState(NguoiChoi player, BanCo board)
        {
            NguoiChoiHienTai = player;
            Board = board;
        }

        //Trả về các nước đi hợp lệ của quân cờ tại vị trí cụ thể
        public List<Move> NuocDiHopLeChoQuanCo(Position pos)
        {
            //Nếu vị trí đó rỗng hoặc chứa quân cờ khác phe
            if(Board.IsEmpty(pos)|| Board[pos].Color!=NguoiChoiHienTai)
            {
                //Trả về danh sách rỗng
                return new List<Move>();
            }

            QuanCo piece = Board[pos]; 
            List<Move> moveCandidates= piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => move.IsLegal(Board)).ToList();
        }

        //Thực thi nước đi
        public void MakeMove(Move move)
        {
            move.ThucThi(Board);
            NguoiChoiHienTai = NguoiChoiHienTai.TimDoiThu();
            KTGameOver();
        }

        //Tất cả các nước đi mà người chơi hiện tại có thể thực hiện để kiểm tra chiếu tướng hoặc bị chiếu bí
        public List<Move> TatCaCacNuocDiHopLe(NguoiChoi player)
        {
            List<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
            {
                QuanCo piece = Board[pos];
                return piece.GetMoves(pos, Board);
            }).ToList();
            return moveCandidates.Where(move => move.IsLegal(Board)).ToList();
        }

        //Kiểm tra chiếu tướng / chiếu bí

        public void KTGameOver()
        {
            if(!TatCaCacNuocDiHopLe(NguoiChoiHienTai).Any())
            {
                if(Board.IsInCheck(NguoiChoiHienTai))
                {
                    ketqua = KetQua.Win(NguoiChoiHienTai.TimDoiThu());
                }
                else
                {
                    ketqua = KetQua.Hoa(LyDoKetThuc.ChieuBi);
                }
            }
        }
        
        //Thông báo GameOver
        public bool GameOver()
        {
            return ketqua != null;
        }
        
    }
}
