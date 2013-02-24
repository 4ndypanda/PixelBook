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
    }
}
