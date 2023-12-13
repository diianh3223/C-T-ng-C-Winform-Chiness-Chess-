using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiangQi_11_10_23;

namespace ChessLogic
{
    public class Si : QuanCo
    {
        public override KieuQuanCo Type => KieuQuanCo.Si;
        public override NguoiChoi Color { get; }
        

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast,
        };

        public Si(NguoiChoi color)
        {
            Color = color;
        }

        public override QuanCo Copy()
        {
            Si copy = new Si(Color);
            return copy;
        }

        // //Tìm tất cả các vị trí có thể di chuyển đến của quân sĩ
        private List<Position> AdvisorMoves(Position from, BanCo board)
        {
            //Tạo 1 danh sách để lưu trữ các vị trí đến
            List<Position> positions = new List<Position>();
            foreach (Direction dir in dirs)
            {//Kiểm tra tính hợp lệ của vị trí đến
                Position to = from + dir;
                //Kiểm tra vị trí đến của quân sĩ có nằm trung Cung Tướng hay không?
                if (!BanCo.IsInsideFrames(to))
                {
                    continue;
                }
                //Kiểm tra vị trí đến của quân sĩ có rỗng hoặc có quân khác phe hay không?
                if (board.IsEmpty(to) || board[to].Color != Color)
                {
                    positions.Add(to);
                }
            }
            return positions;
        }

        public override List<Move> GetMoves(Position from, BanCo board)
        {
            List<Move> moves = new List<Move>();
            foreach (Position to in AdvisorMoves(from, board))
            {
                moves.Add(new NormalMove(from, to));
            }
            return moves;
        }
        public override bool CanCaptureOpponentKing(Position from, BanCo board)
        {
            List<Move> moves = GetMoves(from, board);

            return moves.Any(move =>
            {
                QuanCo piece = board[move.VitriKetThuc];
                return piece != null && piece.Type == KieuQuanCo.Vua;
            });
        }
    }
}
