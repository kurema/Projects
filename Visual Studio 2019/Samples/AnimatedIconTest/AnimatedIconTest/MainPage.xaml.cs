using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace AnimatedIconTest
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en";
        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if ((sender as ContentControl)?.Content is Microsoft.UI.Xaml.Controls.AnimatedIcon icon)
                Microsoft.UI.Xaml.Controls.AnimatedIcon.SetState(icon, "PointerOver");
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if ((sender as ContentControl)?.Content is Microsoft.UI.Xaml.Controls.AnimatedIcon icon)
                Microsoft.UI.Xaml.Controls.AnimatedIcon.SetState(icon, "Normal");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            double scaling = 1.75;
            //var height = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar.Height;
            var height = 50;
            var cview = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            cview.TryResizeView(new Size { Width = 1280 / scaling, Height = (720 - height) / scaling +1});
        }
    }
}
