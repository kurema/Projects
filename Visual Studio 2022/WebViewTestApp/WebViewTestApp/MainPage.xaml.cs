using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace WebViewTestApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void WebView_WebResourceRequested(WebView sender, WebViewWebResourceRequestedEventArgs args)
        {
            var deferral = args.GetDeferral();
            Uri uri = null;
            var sb = new StringBuilder();
            sb.AppendLine($"1: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            await sender.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                sb.AppendLine($"2: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                uri = sender.Source;
            });
            sb.AppendLine($"3: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            Debug.WriteLine(sb.ToString());
            args.Response = new Windows.Web.Http.HttpResponseMessage(Windows.Web.Http.HttpStatusCode.Forbidden);
            deferral.Complete();
            deferral.Dispose();
        }

        private async void WebView_WebResourceRequested2(WebView sender, WebViewWebResourceRequestedEventArgs args)
        {
            Uri uri = null;
            var sb = new StringBuilder();
            sb.AppendLine($"1: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(async () =>
            {
                sb.AppendLine($"2: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                await sender.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    sb.AppendLine($"3: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                    uri = sender.Source;
                });
            }).ConfigureAwait(true);
            sb.AppendLine($"4: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            Debug.WriteLine(sb.ToString());
            args.Response = new Windows.Web.Http.HttpResponseMessage(Windows.Web.Http.HttpStatusCode.Forbidden);
        }

    }
}
