using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiangQi_11_10_23;

namespace ChessLogic
{
    public class Vua : QuanCo
    {
        public override KieuQuanCo Type => KieuQuanCo.Vua;

        public override NguoiChoi Color { get; }

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
        };
        public Vua(NguoiChoi color)
        {
            Color = color;
        }

        public override QuanCo Copy()
        {
            Vua copy = new Vua(Color);
            return copy;
        }

        private List<Position> KingMoves(Position from, BanCo board)
        {
            List<Position> positions = new List<Position>();
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;
                if (!BanCo.IsInsideFrames(to))
                {
                    continue;
                }
                if (board.IsEmpty(to) || board[to].Color != Color)
                {

                    positions.Add(to);
                }
                if (Color == NguoiChoi.Do)
                {
                    for (Position vitriden1=from+Direction.North;BanCo.IsInside(vitriden1); vitriden1 += Direction.North)
                    {
                        if (!board.IsEmpty(vitriden1) && board[vitriden1].Type!=KieuQuanCo.Vua ) 
                        { 
                            break; 
                        }
                        else if(!board.IsEmpty(vitriden1)  && board[vitriden1].Type == KieuQuanCo.Vua)
                        {
                            positions.Add(vitriden1);
                            break;
                        }
                    }
                }
                if (Color == NguoiChoi.Den)
                {
                    for (Position vitriden2 = from + Direction.South; BanCo.IsInside(vitriden2); vitriden2 += Direction.South)
                    {
                        if (!board.IsEmpty(vitriden2) && board[vitriden2].Type != KieuQuanCo.Vua)
                        {
                            break;
                        }
                        else if (!board.IsEmpty(vitriden2) && board[vitriden2].Type == KieuQuanCo.Vua)
                        {
                            positions.Add(vitriden2);
                            break;
                        }
                    }
                }
            }

            return positions;
        }

        public override List<Move> GetMoves(Position from, BanCo board)
        {
            List<Move> moves = new List<Move>();
            foreach (Position to in KingMoves(from, board))
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