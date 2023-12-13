using System.Collections.Generic;
using System.Linq;
using XiangQi_11_10_23;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChessLogic
{
    public class Ma : QuanCo
    {
        public override KieuQuanCo Type => KieuQuanCo.Ma;

        public override NguoiChoi Color { get; }
        private static readonly Direction[] dirs = new Direction[]
       {
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast,
       };
        public Ma(NguoiChoi color)
        {
            Color = color;
        }

        public override QuanCo Copy()
        {
            Ma copy = new Ma(Color);
            //copy.HasMoved = HasMoved;
            return copy;
        }
        private List<Position> HorseMoves(Position from, BanCo board)
        {
            List<Position> positions = new List<Position>();
            Position chanNorth = from + Direction.North;
            Position huongNW = from + 2 * Direction.North + Direction.West;
            Position huongNE = from + 2 * Direction.North + Direction.East;

            Position chanSouth = from + Direction.South;
            Position huongSW = from + 2 * Direction.South + Direction.West;
            Position huongSE = from + 2 * Direction.South + Direction.East;

            Position chanWest = from + Direction.West;
            Position huongWN = from + 2 * Direction.West + Direction.North;
            Position huongWS = from + 2 * Direction.West + Direction.South;
            
            Position chanEast = from + Direction.East;
            Position huongEN = from + 2 * Direction.East + Direction.North;
            Position huongES = from + 2 * Direction.East + Direction.South;

            // Hướng Lên/North
            if (BanCo.IsInside(huongNW) && board.IsEmpty(chanNorth) && (board.IsEmpty(huongNW) || board[huongNW].Color != Color))
            {
                positions.Add(huongNW);
            }

            if (BanCo.IsInside(huongNE) && board.IsEmpty(chanNorth) && (board.IsEmpty(huongNE) || board[huongNE].Color != Color))
            {
                positions.Add(huongNE);
            }

            // Hướng Xuống/South
            if (BanCo.IsInside(huongSW) && board.IsEmpty(chanSouth) && (board.IsEmpty(huongSW) || board[huongSW].Color != Color))
            {
                positions.Add(huongSW);
            }

            if (BanCo.IsInside(huongSE) && board.IsEmpty(chanSouth) && (board.IsEmpty(huongSE) || board[huongSE].Color != Color))
            {
                positions.Add(huongSE);
            }

            // Hướng Trái/West
            if (BanCo.IsInside(huongWN) && board.IsEmpty(chanWest) && (board.IsEmpty(huongWN) || board[huongWN].Color != Color))
            {
                positions.Add(huongWN);
            }

            if (BanCo.IsInside(huongWS) && board.IsEmpty(chanWest) && (board.IsEmpty(huongWS) || board[huongWS].Color != Color))
            {
                positions.Add(huongWS);
            }

            // Hướng Phải/East
            if (BanCo.IsInside(huongEN) && board.IsEmpty(chanEast) && (board.IsEmpty(huongEN) || board[huongEN].Color != Color))
            {
                positions.Add(huongEN);
            }

            if (BanCo.IsInside(huongES) && board.IsEmpty(chanEast) && (board.IsEmpty(huongES) || board[huongES].Color != Color))
            {
                positions.Add(huongES);
            }
            return positions;
        }
    
        public override List<Move> GetMoves(Position from, BanCo board)
        {
            List<Move> moves = new List<Move>();
            foreach (Position to in HorseMoves(from, board))
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