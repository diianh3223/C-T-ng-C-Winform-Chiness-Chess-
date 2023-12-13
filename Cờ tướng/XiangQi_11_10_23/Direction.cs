using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangQi_11_10_23
{
    public class Direction
    {
        //Lớp này hỗ trợ cho việc tạo các nước đi bởi vì các quân cờ đều di chuyển theo các hướng nhất định
            
            //4 hướng ngang và dọc
            public readonly static Direction North = new Direction(-1, 0); 
            public readonly static Direction South = new Direction(1, 0); 
            public readonly static Direction East = new Direction(0, 1); 
            public readonly static Direction West = new Direction(0, -1);
            
            //4 hướng chéo
            public readonly static Direction NorthEast = North + East;
            public readonly static Direction NorthWest = North + West;
            public readonly static Direction SouthWest = South + West;
            public readonly static Direction SouthEast = South + East;
            public int DongDelta { get; }
            public int CotDelta { get; }

            public Direction(int dongDelta, int cotDelta)
            {
                DongDelta = dongDelta;
                CotDelta = cotDelta;
            }

            //
            public static Direction operator +(Direction dir1, Direction dir2)
            {
                return new Direction(dir1.DongDelta + dir2.DongDelta, dir1.CotDelta + dir2.CotDelta);
            }

            //
            public static Direction operator *(int scalar, Direction dir)
            {
                return new Direction(scalar * dir.DongDelta, scalar * dir.CotDelta);
            }
     }
}

