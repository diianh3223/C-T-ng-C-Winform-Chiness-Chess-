using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiangQi_11_10_23;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChessLogic
{
    public class Tuong : QuanCo
    {
        public override KieuQuanCo Type => KieuQuanCo.Tuong;

        public override NguoiChoi Color { get; }
        public readonly Position pos;

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast,
        };
        

        public Tuong(NguoiChoi color)
        {
            Color= color;

            
        }

        public override QuanCo Copy()
        {
            Tuong copy = new Tuong(Color);
            return copy;
        }
          private List<Position> ElephantMoves(Position from, BanCo board)
        {// Tạo danh sách để lưu trữ các vị trí đến
            List<Position> positions = new List<Position>();

            int row = from.Dong;
            int col = from.Cot;

            // Kiểm tra phe của quân tượng và khoảng di chuyển tương ứng
            int minRow = (Color == NguoiChoi.Do) ? 5 : 0;
            int maxRow = (Color == NguoiChoi.Do) ? 9 : 4;

            foreach (Direction dir in dirs)
            {
                int toRow = row + 2 * dir.DongDelta;
                int toCol = col + 2 * dir.CotDelta;
                
                // Kiểm tra xem vị trí mới có nằm trong bàn cờ và frame của tương ứng không
                Position toPos = new Position(toRow, toCol);
                Position chanPos = new Position(row+dir.DongDelta, col+dir.CotDelta);
                if (BanCo.IsInside(toPos) &&(board.IsEmpty(chanPos))&& ((Color == NguoiChoi.Do && BanCo.IsInsideFrameDo(toPos)) || (Color == NguoiChoi.Den && BanCo.IsInsideFrameDen(toPos))))
                {
                    // Kiểm tra phe của quân tượng và xác định khoảng di chuyển hợp lệ
                    if (toRow >= minRow && toRow <= maxRow && (board.IsEmpty(toPos) || board[toRow, toCol].Color != Color))
                    {
                        positions.Add(toPos);
                    }
                }
            }
            return positions;
        }
       
          



        public override List<Move> GetMoves(Position from, BanCo board)
        {
            List<Move> moves = new List<Move>();
            foreach (Position to in ElephantMoves(from, board))
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
