using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using SastImg.Client.Service.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastImg.Client.Views.Dialogs
{
    public partial class CreateAlbumDialogViewModel : ObservableObject
    {

        public ObservableCollection<CategoryDto> Categories { get; set; } = new ObservableCollection<CategoryDto>();
        
        [ObservableProperty]
        public CategoryDto selectedCategory;

        [ObservableProperty]
        public string albumTitle;

        [ObservableProperty]
        public string albumDescription;

        [ObservableProperty]
        private long albumCategoryId;
        public async Task LoadCategoriesAsync()
        {
            var response = await App.API!.Category.GetCategoryAsync();
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                Categories.Clear();
                foreach (var category in response.Content)
                {
                    Categories.Add(category);
                }
            }
            }
        public async Task CreateAlbumAsync()
        {
            if (string.IsNullOrEmpty(AlbumTitle))
            {
                var messageDialog = new ContentDialog
                {
                    Content = "创建失败，相册名称不能为空。",
                    PrimaryButtonText = "确定"
                };
                await messageDialog.ShowAsync();
                return;
            }
            if (SelectedCategory == null)
            {
                var selectCategoryDialog = new ContentDialog
                {
                    Title = "请选择分类",
                    Content = "创建相册需要选择一个分类。",
                    PrimaryButtonText = "确定"
                };
                await selectCategoryDialog.ShowAsync();
                return;
            }
            AlbumCategoryId = SelectedCategory.Id;
            var createAlbumRequest = new CreateAlbumRequest
            {
                Title = AlbumTitle,
                Description = AlbumDescription,
                CategoryId = AlbumCategoryId,
                AccessLevel = 1
            };

            var albumRequest = await App.API!.Album.CreateAlbumAsync(createAlbumRequest);
            if (albumRequest.IsSuccessful)
            {

                var successDialog = new ContentDialog
                {
                    Content = "相册创建成功！",
                    PrimaryButtonText = "确定"
                };
                await successDialog.ShowAsync();
            }
            else
            {
                var errorDialog = new ContentDialog
                {
                    Content = "相册创建失败，请稍后重试。",
                    PrimaryButtonText = "确定"
                };
                await errorDialog.ShowAsync();
            }
        }
    }
}