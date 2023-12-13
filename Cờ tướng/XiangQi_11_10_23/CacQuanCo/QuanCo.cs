using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XiangQi_11_10_23
{
    //QuanCo là 1 lớp abstract, vì đây là lớp cơ sở mà tất cả các quân cờ cụ thể khác sẽ kế thừa
    public abstract class QuanCo 
    {
        public abstract KieuQuanCo Type { get; }
        public abstract NguoiChoi Color { get; }

        //public bool HasMoved { get; set; } = false; // Cần vì có 1 số nước đi chỉ hợp lẹ khi quân cờ chưa di chuyển.

        public abstract QuanCo Copy();

        //Tìm 1 danh sách các nước đi hợp lệ tương ứng với từng quân cờ
        public abstract List<Move> GetMoves(Position from, BanCo board);
        public virtual bool CanCaptureOpponentKing(Position from, BanCo board)
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
