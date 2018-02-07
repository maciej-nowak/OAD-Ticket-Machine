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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Biletomat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        private string adres;


        public BlankPage1()
        {
            this.InitializeComponent();

            adres = "http://localhost/web_app";
        }

        private void OnWyborBiletu(object sender, TappedRoutedEventArgs e)
        {

            Widok.Source = new Uri("http://localhost/web_app/tickets/index.html");
        }

        private void OnRozkladJazdy(object sender, TappedRoutedEventArgs e)
        {
            Widok.Source = new Uri("http://localhost/web_app/schedule");
        }

        private void OnSerwis(object sender, TappedRoutedEventArgs e)
        {
            Widok.Source = new Uri("http://localhost/web_app/service/index-service.html");
        }

        private void OnZaplata(object sender, TappedRoutedEventArgs e)
        {
            


            Frame rootFrame = Window.Current.Content as Frame;

            rootFrame.Navigate(typeof(MainPage));
        }

        private void Widok_OnFrameDOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            
            string html = Widok.InvokeScript("eval", new string[] { "document.documentElement.textContent;" });

            if (html.StartsWith("!OUTPUT!"))
            {

                var liczba = float.Parse(html.Substring(8));
                Zaplata.Content = liczba;
            }
        }
    }
}
