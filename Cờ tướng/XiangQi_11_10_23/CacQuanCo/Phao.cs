using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiangQi_11_10_23;

namespace ChessLogic
{
    public class Phao : QuanCo
    {
        public override KieuQuanCo Type => KieuQuanCo.Phao;

        public override NguoiChoi Color { get; }

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
        };

        public Phao(NguoiChoi color)
        {
            Color = color;
        }

        public override QuanCo Copy()
        {
            Phao copy = new Phao(Color);
            return copy;
        }
        
        protected List<Position> MovePositionInDir(Position from, BanCo board, Direction dir)
        {
            List<Position> positions = new List<Position>();

            bool foundPiece = false; // Sử dụng biến này để đánh dấu khi gặp một quân cờ

            for (Position pos = from + dir; BanCo.IsInside(pos); pos += dir)
            {
                if (!foundPiece)
                {
                    if (board.IsEmpty(pos))
                    {
                        positions.Add(pos);
                        continue;
                    }
                    else
                    {
                        foundPiece = true; // Đánh dấu đã tìm thấy quân cờ
                    }
                }
                else // Nếu đã tìm thấy quân cờ
                {
                    if (!board.IsEmpty(pos))
                    {
                        QuanCo piece = board[pos];
                        if (piece.Color != Color)
                        {
                            positions.Add(pos);
                            break;
                        }
                        
                    }
                    
                }
            }

            return positions;
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