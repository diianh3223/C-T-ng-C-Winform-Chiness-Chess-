using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiangQi_11_10_23;

namespace ChessLogic
{
    public class Xe : QuanCo
    {
        public override KieuQuanCo Type => KieuQuanCo.Xe;
        public override NguoiChoi Color { get; }

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
        };

        public Xe(NguoiChoi color)
        {
            Color = color;
        }

        public override QuanCo Copy()
        {
            Xe copy = new Xe(Color);
            return copy;
        }

        //Tìm tất cả các vị trí có thể đến theo 1 hướng chỉ định
        protected List<Position> MovePositionInDir(Position from, BanCo board, Direction dir)
        {
            List<Position> positions = new List<Position>();//Tạo 1 danh sách để lưu trữ các vị trí
            for (Position pos = from + dir; BanCo.IsInside(pos); pos += dir)
                //Kiểm tra vị trí cách quân cờ 1 nước đi theo hướng chỉ định
            {
                if (board.IsEmpty(pos))// Nếu vị trí đó rỗng
                {
                   positions.Add(pos);//Thêm vị trí vào danh sách
                    continue;
                }
                //Trường hợp vị trí đó không rỗng(==Nếu có quân cờ khác ở vị trí đó)
                QuanCo piece = board[pos];
                if (piece.Color != Color)//Nếu quân cờ đó không cùng phe(màu)
                {
                    positions.Add(pos);//Thêm vị trí vào danh sách
                }
                 break;
            }
            return positions;//Trả về danh sách
        }

        //Tìm tất cả các vị trí có thể đến theo nhiều hướng chỉ định
        protected List<Position> MovePositionsInDirs(Position from, BanCo board, Direction[] dirs)
        {
            List<Position> positions = new List<Position>();
            foreach (Direction dir in dirs)
            {
                positions.AddRange(MovePositionInDir(from, board, dir));
            }

            return positions;
        }

        //Trả về danh sách các nước đi từ danh sách các vị trí đã tìm
        public override List<Move> GetMoves(Position from, BanCo board)
        {
            List<Move> moves = new List<Move>();
            foreach (Position pos in MovePositionsInDirs(from, board, dirs))
            {
                //với mỗi 1 vị trí(pos), tạo 1 đối tượng NormalMove
                //NormalMove có vị trí bắt đầu là from và vị trí kết thúc là danh sách các vị trí đã tìm
                //Thêm đối tượng NormalMove vào danh sách moves
                moves.Add(new NormalMove(from, pos));
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
