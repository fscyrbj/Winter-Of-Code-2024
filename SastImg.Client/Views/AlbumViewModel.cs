using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using SastImg.Client.Service.API;
using SastImg.Client.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;
using Windows.Graphics.Imaging;

namespace SastImg.Client.Views.Dialogs
{
   


    public partial class AlbumViewModel : ObservableObject
    {
        public ObservableCollection<AlbumDto> Albums { get; set; } = new ObservableCollection<AlbumDto>();
        [ObservableProperty]
        private AlbumDto selectedAlbum;
        public ObservableCollection<ImageDto> Images { get; set; } = new ObservableCollection<ImageDto>(); 
        [ObservableProperty]
        private ImageDto selectedImage;
        public ObservableCollection<DetailedAlbum> DetailedAlbums { get; set; } = new ObservableCollection<DetailedAlbum>();
        public DetailedAlbum SelectedDetailedAlbum
        {
            get
            {
                if (SelectedAlbum != null)
                {
                    int index = Albums.IndexOf(SelectedAlbum);
                    if (index >= 0 && index < DetailedAlbums.Count)
                    {
                        return DetailedAlbums[index];
                    }
                }
                return null;
            }
            set { }
        }
        public AlbumViewModel()
        {
            PropertyChanged += async (sender, e) =>
            {
                if (e.PropertyName == nameof(SelectedAlbum) && SelectedAlbum != null)
                {
                    await GetAllImagesAsync();
                    OnPropertyChanged(nameof(SelectedDetailedAlbum));
                }
            };
            Albums.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove)
                {
                    OnPropertyChanged(nameof(SelectedDetailedAlbum));
                }
            };
            DetailedAlbums.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove)
                {
                    OnPropertyChanged(nameof(SelectedDetailedAlbum));
                }
            };
        }
        
        public async Task GetAlbumsAsync()
        {
            var albumRequest = await App.API!.Album.GetAlbumsAsync(category: null, author: null, title: null);
            
            if (albumRequest.IsSuccessful)
            {
                Albums.Clear();
                DetailedAlbums.Clear();
                foreach (var album in albumRequest.Content)
                {

                    var descriptionRequest = await App.API!.Album.GetDetailedAlbumAsync(album.Id);

                    var description = descriptionRequest.Content;

                    var detailAlbum = new DetailedAlbum
                    {
                        Description = description.Description
                    };
                    DetailedAlbums.Add(detailAlbum);


                    Albums.Add(album);
                }
                
            }

        }

        public async Task<bool> GetAllImagesAsync()
        {
            Images.Clear();
            var imagesRequest = await App.API!.Image.GetImagesAsync(null,SelectedAlbum.Id,null);
            if (!imagesRequest.IsSuccessful) return false;

            foreach (var image in imagesRequest.Content)
            {
                Images.Add(image);
            }
            return true;
        }
        public async Task<bool> GetDetailedAlbun(AlbumDto album)
        {
            var DetailedRequest = await App.API!.Album.GetDetailedAlbumAsync(album.Id);
            if (DetailedRequest == null) return false;
            return true;
        }
        public async Task<bool> ToImagePage()
        {
            if (SelectedImage != null)
            {
                var id = SelectedImage.Id;
                ImageViewModel imageViewModel = new ImageViewModel();
                await imageViewModel.ShowImageAsync(id);

                return true;
                
            }
            return false;
        }

    }
}
