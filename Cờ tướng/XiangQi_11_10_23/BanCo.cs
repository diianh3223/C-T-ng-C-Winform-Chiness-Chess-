using ChessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangQi_11_10_23
{
    //Lưu trữ tất cả các quân cờ đang hoạt động của trò chơi
    public class BanCo
    {
        
        private readonly QuanCo[,] pieces = new QuanCo[10, 9];
       
        public QuanCo this[int dong, int cot]
        {
            get { return pieces[dong, cot]; }
            set { pieces[dong, cot] = value; }
        }

        public QuanCo this[Position pos]
        {
            get { return this[pos.Dong, pos.Cot]; }
            set { this[pos.Dong, pos.Cot] = value; }
        }


        // Phương thức trả về bàn cờ với tất cả các quân cờ đã được set up chính xác đ
        public static BanCo KhoiTao() 
        {
            BanCo board = new BanCo();
            board.AddStartPieces();
            return board;
        }

        private void AddStartPieces()
        {
            this[0, 0] = new Xe(NguoiChoi.Den);
            this[0, 1] = new Ma(NguoiChoi.Den);
            this[0, 2] = new Tuong(NguoiChoi.Den);
            this[0, 3] = new Si(NguoiChoi.Den);
            this[0, 4] = new Vua(NguoiChoi.Den);
            this[0, 5] = new Si(NguoiChoi.Den);
            this[0, 6] = new Tuong(NguoiChoi.Den);
            this[0, 7] = new Ma(NguoiChoi.Den);
            this[0, 8] = new Xe(NguoiChoi.Den);
            this[2, 1] = new Phao(NguoiChoi.Den);
            this[2, 7] = new Phao(NguoiChoi.Den);
            this[3, 0] = new Tot(NguoiChoi.Den);
            this[3, 2] = new Tot(NguoiChoi.Den);
            this[3, 4] = new Tot(NguoiChoi.Den);
            this[3, 6] = new Tot(NguoiChoi.Den);
            this[3, 8] = new Tot(NguoiChoi.Den);

            this[9, 0] = new Xe(NguoiChoi.Do);
            this[9, 1] = new Ma(NguoiChoi.Do);
            this[9, 2] = new Tuong(NguoiChoi.Do);
            this[9, 3] = new Si(NguoiChoi.Do);
            this[9, 4] = new Vua(NguoiChoi.Do);
            this[9, 5] = new Si(NguoiChoi.Do);
            this[9, 6] = new Tuong(NguoiChoi.Do);
            this[9, 7] = new Ma(NguoiChoi.Do);
            this[9, 8] = new Xe(NguoiChoi.Do);
            this[7, 1] = new Phao(NguoiChoi.Do);
            this[7, 7] = new Phao(NguoiChoi.Do);
            this[6, 0] = new Tot(NguoiChoi.Do);
            this[6, 2] = new Tot(NguoiChoi.Do);
            this[6, 4] = new Tot(NguoiChoi.Do);
            this[6, 6] = new Tot(NguoiChoi.Do);
            this[6, 8] = new Tot(NguoiChoi.Do);
        }

        //Kiểm tra tính hợp lệ của vị trí
        public static bool IsInside(Position pos) //Trong bàn cờ
        {
            return pos.Dong >= 0 && pos.Dong < 10 && pos.Cot >= 0 && pos.Cot < 9;
        }
        public bool IsEmpty(Position pos) //Rỗng
        {
            return this[pos] == null;
        }

        public static bool IsInsideFrames(Position pos) //Cung điện cho tướng và sĩ
        {
            bool isInFirstFrame = (pos.Dong >= 0 && pos.Dong <= 2 && pos.Cot >= 3 && pos.Cot <= 5);
            bool isInSecondFrame = (pos.Dong >= 7 && pos.Dong <= 9 && pos.Cot >= 3 && pos.Cot <= 5);

            return isInFirstFrame || isInSecondFrame;
        }

        public static bool IsInsideFrameDen(Position pos) //Lãnh thổ cho tượng đen
        {
            return (pos.Dong >= 0 && pos.Dong <= 4 && pos.Cot >= 0 && pos.Cot <= 8);
        }

        public static bool IsInsideFrameDo(Position pos) //Lãnh thổ cho tượng đỏ
        {
            return (pos.Dong >= 5 && pos.Dong <= 9 && pos.Cot >= 0 && pos.Cot <= 8);
        }

        //
        
        //
        public List<Position> PiecePositions()
        {
            List<Position> positions = new List<Position>();
            for (int r=0;r<10;r++)
            {
                for(int c=0;c<9;c++)
                {
                    Position pos = new Position(r, c);
                    if(!IsEmpty(pos))
                    {
                        positions.Add(pos);
                    }
                }
            }
            return positions;
        }

        //
        public List<Position> PiecePositionsFor(NguoiChoi player)
        {
            return PiecePositions().Where(pos => this[pos] != null && this[pos].Color == player).ToList();
        }

        //
        public bool IsInCheck(NguoiChoi player)
        {
            return PiecePositionsFor(player.TimDoiThu()).Any(pos =>
            {
                QuanCo piece = this[pos];
                return piece.CanCaptureOpponentKing(pos, this);
            });
        }
        //
        public BanCo Copy()
        {
            BanCo copy= new BanCo();
            foreach(Position pos in PiecePositions())
            {
                if (this[pos] != null)
                {
                    copy[pos] = this[pos].Copy();
                }
                
            }
            return copy;
        }
    }
}
