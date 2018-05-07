using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Microsoft.Toolkit.Win32.UI.Controls.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebBrowserSxS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseBack_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = WebView != null && WebView.CanGoBack;
        }

        private void BrowseBack_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            WebView?.GoBack();
            WebBrowser?.GoBack();
        }

        private void BrowseForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = WebView != null && WebView.CanGoForward;
        }
        private void GoToPage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void GoToPage_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var result = (Uri)new WebBrowserUriTypeConverter().ConvertFromString(Url.Text);
            WebView.Source = result;
            WebBrowser.Source = result;
        }

        private void BrowseForward_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            WebView?.GoForward();
            WebBrowser?.GoForward();
        }

        private void Url_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var result =
                    (Uri)new WebBrowserUriTypeConverter().ConvertFromString(
                        Url.Text);

                WebView?.Navigate(result);
                WebBrowser?.Navigate(result);
            }
        }

        private void WebView1_OnNavigationCompleted(object sender, WebViewControlNavigationCompletedEventArgs e)
        {
            Url.Text = e.Uri?.ToString() ?? string.Empty;
            Title = WebView.DocumentTitle;
            if (!e.IsSuccess)
            {
                MessageBox.Show($"Could not navigate to {e.Uri?.ToString() ?? "NULL"}", $"Error: {e.WebErrorStatus}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WebView1_OnNavigationStarting(object sender, WebViewControlNavigationStartingEventArgs e)
        {
            Title = $"Navigating {e.Uri?.ToString() ?? string.Empty}";
            Url.Text = e.Uri?.ToString() ?? string.Empty;
        }


    }
}
