using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangQi_11_10_23
{
    //Lớp cơ sở cho 1 nước đi 
    public abstract class Move
    {
        
        public abstract Position VitriBatDau { get; }

        public abstract Position VitriKetThuc { get; }

        public abstract void ThucThi(BanCo board);//Giúp các quân cờ thực thi các nước đi

        //
        public virtual bool IsLegal(BanCo board)
        {
            NguoiChoi player = board[VitriBatDau].Color;
            BanCo boardCopy = board.Copy();
            ThucThi(boardCopy);
            return !boardCopy.IsInCheck(player);
        }
    }
}
