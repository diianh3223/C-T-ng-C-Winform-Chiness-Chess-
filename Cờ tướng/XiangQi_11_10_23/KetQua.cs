namespace XiangQi_11_10_23
{
    public class KetQua
    {
        //Chứa thông tin của người thắng hoặc hòa

        public NguoiChoi Winner { get; }
        public LyDoKetThuc LyDo { get; }

        public KetQua(NguoiChoi winner, LyDoKetThuc lydo)
        {
            Winner= winner;
            LyDo = lydo;
        }

        public static KetQua Win(NguoiChoi winner)
        {
            return new KetQua(winner, LyDoKetThuc.ChieuTuong);
        }

        public static KetQua Hoa(LyDoKetThuc lydo)
        {
            return new KetQua(NguoiChoi.None, lydo);
        }
    }
}
