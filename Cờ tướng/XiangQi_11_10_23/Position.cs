using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiangQi_11_10_23
{
    public class Position
    {
        public int Dong { get; }
        public int Cot { get; }

        public Position(int dong, int cot)
        {
            Dong = dong;
            Cot = cot;
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Dong == position.Dong &&
                   Cot == position.Cot;
        }

        public override int GetHashCode()
        {
            int hashCode = -1774893876;
            hashCode = hashCode * -1521134295 + Dong.GetHashCode();
            hashCode = hashCode * -1521134295 + Cot.GetHashCode();
            return hashCode;
        }

        public static Position operator +(Position pos, Direction dir)
        {
            return new Position(pos.Dong + dir.DongDelta, pos.Cot + dir.CotDelta);
        }
    }
}
