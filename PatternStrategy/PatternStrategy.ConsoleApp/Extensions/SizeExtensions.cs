﻿using System.Drawing;

namespace PatternStrategy.ConsoleApp.Extensions
{
    public static class SizeExtensions
    {
        public static bool FitsIn(this Size size, int width, int height) 
            => size.Width <= width && size.Height <= height;
    }
}
