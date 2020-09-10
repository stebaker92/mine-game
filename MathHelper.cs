using System;
using System.Collections.Generic;
using System.Text;

namespace MineGame
{
    public static class MathHelper
    {
        public static double ConvertToRadians(this int angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
