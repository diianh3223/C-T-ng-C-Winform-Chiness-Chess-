using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangQi_11_10_23
{
    //Dùng để lưu trữ dữ liệu về lượt chơi của người chơi nào và màu sắc của quân cờ ( Đỏ, Đen) 
    public enum NguoiChoi
    {
        Do,
        Den,
        None // Khi hòa thì có thể chỉnh Người chơi thành None
    }
    
    //

    public static class PlayerExtensions
    {
        public static string ten_nguoichoi1;
        public static string ten_nguoichoi2;
        
        public static NguoiChoi TimDoiThu(this NguoiChoi player)
        {
            switch(player)
            {
                case NguoiChoi.Do:
                    return NguoiChoi.Den;
                case NguoiChoi.Den:
                    return NguoiChoi.Do;
                default:
                    return NguoiChoi.None;
            }
        }
    }
}
