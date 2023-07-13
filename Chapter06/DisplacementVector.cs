using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter06
{
    // если поля занимают не более 16 байт в стеке то используется структура
    public struct DisplacementVector
    {
        public int X; // 4 байт
        public int Y;
        public DisplacementVector(int initialX, int initialY)
        {
            X = initialX;
            Y = initialY;
        }

        public static DisplacementVector operator +(DisplacementVector vector1, DisplacementVector vector2)
        {
            return new(
            vector1.X + vector2.X,
            vector1.Y + vector2.Y);
        }
    }
}
