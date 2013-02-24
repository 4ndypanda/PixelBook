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
        private int tileSize = 12;
        Boolean clickHold;

        // Bitmap information
   //     private WriteableBitmap bitmap;
   //     private Stream pixelStream;

   //     List<byte[]> animation;
   //     List<long> delay;
   //     int curFrame;

        public MainPage()
        {
            this.InitializeComponent();
            Application.Current.DebugSettings.EnableFrameRateCounter=true;
            Loaded += OnMainPageLoaded;
            Globals.bitmap = new WriteableBitmap((int)display.Width, (int)display.Height);
            clickHold = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
        }

        private void newPage_Click(object sender, RoutedEventArgs e)
        {
            byte [] newFrame = new byte[Globals.animation[Globals.animation.Count - 1].Length];
            for (int i = 0; i < Globals.animation[Globals.animation.Count - 1].Length; i++)
                newFrame[i] = 255;
            Globals.animation.Add(newFrame);
            Globals.delay.Add(1000);
            Globals.curFrame++;
            Globals.WriteToDisplay(Globals.curFrame);
        }

        private void animate_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Animator));
        }

        private void More_Click_1(object sender, RoutedEventArgs e)
        {
            Popup popUp = new Popup(); //menu popup
            popUp.IsLightDismissEnabled = true; //auto hides when the user interacts with another part of the app.

            StackPanel panel = new StackPanel();
            panel.Background = BottomAppBar1.Background;
            panel.Height = 140;
            panel.Width = 180;

            Button save = new Button();
            save.Content = "Save";
            save.Style = (Style)App.Current.Resources["TextButtonStyle"];
            save.Margin = new Thickness(20, 5, 20, 5);
            save.Click += Save_Click;
            panel.Children.Add(save);

            Button load = new Button();
            load.Content = "Load";
            load.Style = (Style)App.Current.Resources["TextButtonStyle"];
            load.Margin = new Thickness(20, 5, 20, 5);
            load.Click += Load_Click;
            panel.Children.Add(load);

            Button newPage = new Button();
            newPage.Content = "New Page";
            newPage.Style = (Style)App.Current.Resources["TextButtonStyle"];
            newPage.Margin = new Thickness(20, 5, 20, 5);
            newPage.Click += newPage_Click;
            panel.Children.Add(newPage);

            Button animate = new Button();
            animate.Content = "Animate";
            animate.Style = (Style)App.Current.Resources["TextButtonStyle"];
            animate.Margin = new Thickness(20, 5, 20, 5);
            animate.Click += animate_Click;
            panel.Children.Add(animate);

            popUp.Child = panel;

            popUp.HorizontalOffset = Window.Current.CoreWindow.Bounds.Right - panel.Width - 4;
            popUp.VerticalOffset = Window.Current.CoreWindow.Bounds.Bottom - BottomAppBar1.ActualHeight - panel.Height - 4;
            popUp.IsOpen = true;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>

        private void OnMainPageLoaded(object sender, RoutedEventArgs args)
        {
            // Set the bitmap to the Image element
            display.Source = Globals.bitmap;

            // Get Stream and create array for updating bitmap
            Globals.pixelStream = Globals.bitmap.PixelBuffer.AsStream();
            if (Globals.animation == null)
            {
                byte[] pixels = new byte[4 * (int)(display.Width * display.Height)];
                for (int i = 0; i < pixels.Length; i++)
                    pixels[i] = 255;
                Globals.animation = new List<byte[]>();
                Globals.animation.Add(pixels);
                Globals.delay = new List<long>();
                Globals.delay.Add(1000);
                Globals.curFrame = 0;
            }
            Globals.WriteToDisplay(Globals.curFrame);
        }
        private Boolean outOfBounds()
        {
            return curPoint.X < 0 || curPoint.X >= display.Width || curPoint.Y < 0 || curPoint.Y >= display.Height;
        }
        private void editTile()
        {
            if (outOfBounds())
            {
                return;
            }

            topleft = new Point(Math.Floor(curPoint.X / tileSize) * tileSize, Math.Floor(curPoint.Y / tileSize) * tileSize);
            for (int i = (int)topleft.Y; i < (int)topleft.Y + tileSize; i++)
                for (int j = (int)topleft.X; j < (int)topleft.X + tileSize; j++)
                {
                    Globals.animation[Globals.curFrame][(i * Globals.bitmap.PixelWidth + j) * 4] = Globals.color.B;
                    Globals.animation[Globals.curFrame][(i * Globals.bitmap.PixelWidth + j) * 4 + 1] = Globals.color.G;
                    Globals.animation[Globals.curFrame][(i * Globals.bitmap.PixelWidth + j) * 4 + 2] = Globals.color.R;
                    Globals.animation[Globals.curFrame][(i * Globals.bitmap.PixelWidth + j) * 4 + 3] = Globals.color.A;
                }
            Globals.WriteToDisplay(Globals.curFrame);
        }

        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        
        }

        private void display_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            clickHold = true;
            curPoint = e.GetCurrentPoint(display).Position;
            editTile();
        }

        private void display_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (!clickHold)
                return;
            curPoint = e.GetCurrentPoint(display).Position;
            editTile();
        }

        private void Grid_PointerReleased_1(object sender, PointerRoutedEventArgs e)
        {
            clickHold = false;
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            var popup = new ColorPicker.Popup();
            popup.ShowAsync();
        }
    }
}
