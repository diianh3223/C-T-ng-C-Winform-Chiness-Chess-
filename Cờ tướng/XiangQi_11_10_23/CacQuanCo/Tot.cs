using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiangQi_11_10_23;

namespace ChessLogic
{
    public class Tot : QuanCo
    {
        public override KieuQuanCo Type => KieuQuanCo.Tot;
        public override NguoiChoi Color { get; }

       
        public Tot(NguoiChoi color)
        {
            Color = color;
        }

        public override QuanCo Copy()
        {
            Tot copy = new Tot(Color);
            return copy;
        }

        private List<Position> PawnMoves(Position from, BanCo board)
        {
            List<Position> positions = new List<Position>();

            if (Color == NguoiChoi.Den)
            {
                if (from.Dong >= 0 && from.Dong <= 4)
                {
                    Position to = from + Direction.South;
                    if (BanCo.IsInside(to) && (board.IsEmpty(to) || board[to].Color != Color))
                    {
                        positions.Add(to);
                    }
                }
                else if (from.Dong > 4 && from.Dong <= 9)
                {
                    foreach (Direction dir in new Direction[] { Direction.South, Direction.East, Direction.West })
                    {
                        Position to = from + dir;
                        if (BanCo.IsInside(to) && (board.IsEmpty(to) || board[to].Color != Color))
                        {
                            positions.Add(to);
                        }
                    }
                }
            }

            if (Color == NguoiChoi.Do)
            {
                if (from.Dong <= 9 && from.Dong >= 5)
                {
                    Position to = from + Direction.North;
                    if (BanCo.IsInside(to) && (board.IsEmpty(to) || board[to].Color != Color))
                    {
                        positions.Add(to);
                    }
                }
                else if (from.Dong < 5 && from.Dong >= 0)
                {
                    foreach (Direction dir in new Direction[] { Direction.North, Direction.East, Direction.West })
                    {
                        Position to = from + dir;
                        if (BanCo.IsInside(to) && (board.IsEmpty(to) || board[to].Color != Color))
                        {
                            positions.Add(to);
                        }
                    }
                }
            }

            return positions;
        }


        public override List<Move> GetMoves(Position from, BanCo board)
        {
            List<Move> moves = new List<Move>();
            foreach (Position to in PawnMoves(from, board))
            {
                moves.Add(new NormalMove(from, to));
            }
            return moves;
        }

        //

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