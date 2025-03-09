using System;
using System.Security.Cryptography;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.System;

namespace SastImg.Client.Views;
public sealed partial class ShellPage : Page
{
    private ShellPageViewModel vm = new();

    public ShellPage ()
    {
        this.InitializeComponent();
        // 首先显示首页
        MainFrame.Navigate(typeof(HomeView));
        NavView.SelectedItem = NavView.MenuItems[0];
        MainFrame.Navigated += MainFrame_Navigated;
        
    }

    private void MainFrame_Navigated(object sender, NavigationEventArgs e)
    {
        var pageType = e.SourcePageType;

        if (pageType == typeof(HomeView))
        {
            NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().FirstOrDefault(item => item.Tag.Equals("Home"));
        }
        else if (pageType == typeof(AlbumView))
        {
            NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().FirstOrDefault(item => item.Tag.Equals("Album"));
        }
        else if (pageType == typeof(ImageView))
        {
            NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().FirstOrDefault(item => item.Tag.Equals("Image"));
        }
        else if (pageType == typeof(SettingsView))
        {
            NavView.SelectedItem = NavView.FooterMenuItems.OfType<NavigationViewItem>().FirstOrDefault(item => item.Tag.Equals("Settings"));
        }
    }

    private async void NavigationView_ItemInvoked (NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if ( args.InvokedItemContainer is NavigationViewItem item )
        {
            switch ( item.Tag )
            {
                case "Home":
                    MainFrame.Navigate(typeof(HomeView));
                    break;
                case "Settings":
                    MainFrame.Navigate(typeof(SettingsView));
                    break;
                case "GitHub":
                    await Launcher.LaunchUriAsync(new Uri("https://github.com/NJUPT-SAST-Csharp/Winter-Of-Code-2024"));
                    break;
                case "Album":
                    MainFrame.Navigate(typeof(AlbumView));
                    break;
                case "Image":
                    MainFrame.Navigate(typeof(ImageView));
                    break;
               
            }
        };
    }

    private void TitleBar_BackButtonClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (MainFrame.CanGoBack)
        {
            MainFrame.GoBack();
        }
    }

   
}
