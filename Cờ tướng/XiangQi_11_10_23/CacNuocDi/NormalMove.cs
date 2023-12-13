using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangQi_11_10_23
{
    //Dùng cho tất cả các nước đi để di chuyển 1 quân cờ từ 1 vị trí này đến 1 vị trí khác
    public class NormalMove : Move
    {
        
        public override Position VitriBatDau { get; }

        public override Position VitriKetThuc { get; }

        public NormalMove(Position batdau, Position ketthuc)
        {
            VitriBatDau= batdau;
            VitriKetThuc= ketthuc;
        }

        // Phương thức dùng để di chuyển quân cờ từ vị trí bắt đầu đến vị trí kết thúc
        public override void ThucThi(BanCo board)
        {
            QuanCo piece = board[VitriBatDau];
            board[VitriKetThuc] = piece;
            board[VitriBatDau] = null;
        }
    }
}
