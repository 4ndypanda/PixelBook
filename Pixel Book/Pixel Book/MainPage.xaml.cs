using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixel_Book
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Point curPoint, topleft;
        private int tileSize;

        // Bitmap information
        private WriteableBitmap bitmap;
        private Stream pixelStream;
        private byte[] pixels;

        public MainPage()
        {
            this.InitializeComponent();
            Application.Current.DebugSettings.EnableFrameRateCounter=true;
            Loaded += OnMainPageLoaded;
            bitmap = new WriteableBitmap((int)display.Width, (int)display.Height);
         }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>

        private void OnMainPageLoaded(object sender, RoutedEventArgs args)
        {
            // Set the bitmap to the Image element
            display.Source = bitmap;

            // Get Stream and create array for updating bitmap
            pixelStream = bitmap.PixelBuffer.AsStream();
            pixels = new byte[4 * (int)(display.Width * display.Height)];

            tileSize = 12;
        }

        private void WriteToDisplay()
        {
            // Transfer the pixels to the bitmap
            pixelStream.Seek(0, SeekOrigin.Begin);
            pixelStream.Write(pixels, 0, pixels.Length);
            bitmap.Invalidate();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        
        }

        private void display_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            curPoint = e.GetCurrentPoint(display).Position;
            topleft = new Point(Math.Floor(curPoint.X / tileSize) * tileSize, Math.Floor(curPoint.Y / tileSize) * tileSize);
            for (int i = (int)topleft.Y; i < (int)topleft.Y + tileSize; i++)
                for (int j = (int)topleft.X; j < (int)topleft.X + tileSize; j++)
                {
                    pixels[(i * bitmap.PixelWidth + j) * 4] = 255;
                    pixels[(i * bitmap.PixelWidth + j) * 4 + 1] = 255;
                    pixels[(i * bitmap.PixelWidth + j) * 4 + 2] = 255;
                    pixels[(i * bitmap.PixelWidth + j) * 4 + 3] = 255;
                }
            WriteToDisplay();
        }
    }
}
