using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImg.Client.Views.Dialogs;

[ObservableObject]
public sealed partial class LoginDialog : ContentDialog
{
    [ObservableProperty]
    private string _username="";

    [ObservableProperty]
    private string _password="";

    [ObservableProperty]
    private bool _isLoggingIn=false;

    [ObservableProperty]
    private bool _isLoginFailed = false;

    private CancellationTokenSource? _loginCts;

    public LoginDialog ( )
    {
        XamlRoot = App.MainWindow?.Content.XamlRoot;
        this.InitializeComponent();
        this.PrimaryButtonClick += LoginDialog_PrimaryButtonClick;
        this.CloseButtonClick += LoginDialog_CloseButtonClick;
    }

    private void LoginDialog_CloseButtonClick (ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        _loginCts?.Cancel();
    }

    private async void LoginDialog_PrimaryButtonClick (ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        var deferral =  args.GetDeferral();
        _loginCts = new();

        IsLoggingIn = true;
        IsLoginFailed = false;
        try
        {
            if ( await App.AuthService.LoginAsync(Username, Password) )
            {
                // 对话框关闭后显示登陆成功弹窗
                this.Closed += (ContentDialog sender, ContentDialogClosedEventArgs args) =>
                {
                    if ( args.Result is not ContentDialogResult.Primary )
                        return;
                    var successDialog = new ContentDialog()
                    {
                        XamlRoot = this.XamlRoot,
                        Title="登录成功",
                        CloseButtonText="确定"
                    };
                    var _ = successDialog.ShowAsync();
                    
                };
                
            }
            else
            {
                // 对话框关闭后显示登陆失败弹窗
                this.Closed += (ContentDialog sender, ContentDialogClosedEventArgs args) =>
                {
                    if ( args.Result is not ContentDialogResult.Primary )
                        return;
                    var successDialog = new ContentDialog()
                    {
                        XamlRoot = this.XamlRoot,
                        Title="登录失败",
                        CloseButtonText="确定"
                    };
                    var _ = successDialog.ShowAsync();
                };
            }
        }
        catch ( System.Exception )
        {
            args.Cancel = true;
            IsLoggingIn = false;
            IsLoginFailed = true;
        }
        deferral.Complete();
    }

}
