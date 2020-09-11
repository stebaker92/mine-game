using System;

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
