using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Pixel_Book
{
    public static class Globals
    {
        public static byte a;
        public static byte r;
        public static byte g;
        public static byte b;

        public static Color color
        {
            get { return Color.FromArgb(a, r, g, b); }
        }
        
    }
}
