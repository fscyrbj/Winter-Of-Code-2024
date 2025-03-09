using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImg.Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImageView : Page
    {
        public ImageViewModel imageViewModel { get; } = new ImageViewModel();
        public ImageView()
        {
            this.InitializeComponent();
        }
        private async Task UpdateImageAsync()
        {
            var s = new MemoryStream(imageViewModel.ImageData);
            var bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(s.AsRandomAccessStream());
            img.Source = bitmap;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var app = (App)Application.Current;

            if (e.Parameter is long imageId)
            {
                var isSuccess = await imageViewModel.ShowImageAsync(imageId);
                if (isSuccess)
                {
                    await UpdateImageAsync();
                    app.ImageData = imageViewModel.ImageData;
                }

            }
            else if (app.ImageData != null)
            {
                imageViewModel.ImageData = app.ImageData;
                await UpdateImageAsync();
            }
            
        }
        


    }
}
