using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace Pixel_Book
{
    public static class Globals
    {
        public static byte a;
        public static byte r;
        public static byte g;
        public static byte b;

        public static List<byte[]> animation;
        public static List<long> delay;
        public static int curFrame=-1;

        public static WriteableBitmap bitmap;
        public static  Stream pixelStream;
        public static Color color
        {
            get { return Color.FromArgb(a, r, g, b); }
        }
        public static void WriteToDisplay(int index)
        {
            // Transfer the pixels to the bitmap
            pixelStream.Seek(0, SeekOrigin.Begin);
            pixelStream.Write(animation[index], 0, animation[index].Length);
            bitmap.Invalidate();
        }
    }
}
