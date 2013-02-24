using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SocialEbola.Lib.PopupHelpers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixel_Book
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorPicker : UserControl, IPopupControl
    {
        public ColorPicker()
        {
            this.InitializeComponent();

            RSlider.Value = 1.0 * Globals.r;
            GSlider.Value = 1.0 * Globals.g;
            BSlider.Value = 1.0 * Globals.b;
            ASlider.Value = 1.0 * Globals.a;
        }

        public class Popup : PopupHelper<ColorPicker>
        {
        }
        
        private Popup m_popup;
        
        public void SetParent(PopupHelper parent)
        {
            m_popup = (Popup)parent;
        }

        public void Closed(CloseAction action)
        {

        }

        public void Opened()
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            m_popup.CloseAsync();
        }

        private void Slider_ValueChangedA(object sender, RangeBaseValueChangedEventArgs e)
        {
            Globals.a = Convert.ToByte(ASlider.Value);
            showColor.Fill = new SolidColorBrush(Globals.color);
        }


        private void Slider_ValueChangedR(object sender, RangeBaseValueChangedEventArgs e)
        {
            Globals.r = Convert.ToByte(RSlider.Value);
            showColor.Fill = new SolidColorBrush(Globals.color);
        }

        private void Slider_ValueChangedG(object sender, RangeBaseValueChangedEventArgs e)
        {
            Globals.g = Convert.ToByte(GSlider.Value);
            showColor.Fill = new SolidColorBrush(Globals.color);
        }

        private void Slider_ValueChangedB(object sender, RangeBaseValueChangedEventArgs e)
        {
            Globals.b = Convert.ToByte(BSlider.Value);
            showColor.Fill = new SolidColorBrush(Globals.color);
        }
    }
}
