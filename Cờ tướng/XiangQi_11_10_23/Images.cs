using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangQi_11_10_23
{
    //Mục đích là để tải hình ảnh của các quân cờ từ tài nguyên và tiện lợi để truy cập chúng
    public static class Images
    {
        //Lưu trữ hình ảnh trong Dictionary giúp dễ dàng truy cập hình ảnh tương ứng với mỗi loại quân cờ 
        private static readonly Dictionary<KieuQuanCo, Image> doSources = new Dictionary<KieuQuanCo, Image>
        {
            {KieuQuanCo.Vua, Properties.Resources.VuaDo },
            {KieuQuanCo.Si, Properties.Resources.SiDo},
            {KieuQuanCo.Tuong, Properties.Resources.TuongDo},
            {KieuQuanCo.Xe, Properties.Resources.XeDo},
            {KieuQuanCo.Phao,Properties.Resources.PhaoDo},
            {KieuQuanCo.Ma, Properties.Resources.MaDo},
            {KieuQuanCo.Tot, Properties.Resources.TotDo},
        };

        private static readonly Dictionary<KieuQuanCo, Image> denSources = new Dictionary<KieuQuanCo, Image>
        {
            {KieuQuanCo.Vua, Properties.Resources.VuaDen },
            {KieuQuanCo.Si, Properties.Resources.SiDen},
            {KieuQuanCo.Tuong, Properties.Resources.TuongDen},
            {KieuQuanCo.Xe, Properties.Resources.XeDen},
            {KieuQuanCo.Phao,Properties.Resources.PhaoDen},
            {KieuQuanCo.Ma, Properties.Resources.MaDen},
            {KieuQuanCo.Tot, Properties.Resources.TotDen},
        };
        // Trả về hình ảnh tương ứng với loại quân cờ và màu sắc (được xác định bởi KieuQuanCo và NguoiChoi) 
        public static Image GetImage(NguoiChoi color, KieuQuanCo type)
        {
           
            if (color == NguoiChoi.Do)
            {
                //Kiểm tra trong Dictionary có chứa key type(1 kiểu quân cờ) ko? Có -> trả về giá trị(hình ảnh) tương ứng
                return doSources.ContainsKey(type) ? doSources[type] : null;
            }
            else if (color == NguoiChoi.Den)
            {
                return denSources.ContainsKey(type) ? denSources[type] : null;
            }
            else
            {
                return null;
            }
        }

        //Dùng để lấy hình ảnh từ một đối tượng QuanCo mà không cần biết màu và kiểu quân cờ.
        public static Image GetImage(QuanCo piece)
        {
            if (piece == null)
            {
                return null;
            }

            return GetImage(piece.Color, piece.Type);
        }
    }
}
