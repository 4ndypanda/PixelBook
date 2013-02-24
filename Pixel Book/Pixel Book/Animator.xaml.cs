using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixel_Book
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Animator : Page
    {
        public Boolean stop;
        public Animator()
        {
            this.InitializeComponent();
            movie.Source = Globals.bitmap;
            stop = false;
 //           Globals.WriteToDisplay(0);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            stop = true;
            Frame.Navigate(typeof(MainPage));
        }

        private async void aButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; !stop; i++)
            {
                Globals.WriteToDisplay(i);
                await Task.Delay((int)Globals.delay[i]);
                if (i == Globals.animation.Count - 1)
                {
                    i = -1;
                    await Task.Delay(3000);
                }
            }
        }
        
    }
}
