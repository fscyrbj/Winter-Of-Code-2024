using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Refit;
using SastImg.Client.Service.API;
using SastImg.Client.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SastImg.Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlbumView : Page
    {
        public AlbumViewModel ViewModel { get; } = new AlbumViewModel();

        public AlbumView()
        {
            this.InitializeComponent();
            Loaded += OnPageLoaded; 
        }
        public async void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.GetAlbumsAsync();
            
            if (ViewModel.Albums.Count > 0)
            {
                ViewModel.SelectedAlbum = ViewModel.Albums[0];
                
            }
            

        }
        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CreateAlbumDialog();
            dialog.XamlRoot = this.XamlRoot;
            await dialog.ShowAsync();
        }
        private void Image_Click(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is ImageDto clickedImage)
            {
                App.Shell.MainFrame.Navigate(typeof(ImageView), clickedImage.Id);
                
            }
        }

        private async void PickAPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            //disable the button to avoid double-clicking
            var senderButton = sender as Button;
            senderButton.IsEnabled = false;

            // Clear previous returned file name, if it exists, between iterations of this scenario
            PickAPhotoOutputTextBlock.Text = "";

            // Create a file picker
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            // See the sample code below for how to make the window accessible from the App class.
            var window = App.MainWindow;

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Initialize the file picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            // Set options for your file picker
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            // Open the picker for the user to pick a file
            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                using var stream = await file.OpenStreamForReadAsync();
                var streamPart = new StreamPart(stream, file.Name, "image/png");
                var response = await App.API!.Image.AddImageAsync(ViewModel.SelectedAlbum.Id, "title", streamPart, null);
                await ViewModel.GetAllImagesAsync();
                    
            }
            else
            {
                PickAPhotoOutputTextBlock.Text = "Operation cancelled.";
            }

            //re-enable the button
            senderButton.IsEnabled = true;
        }
    }

}
