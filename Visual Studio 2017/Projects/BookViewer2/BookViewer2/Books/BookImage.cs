using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;

using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media.Imaging;

namespace BookViewer2.Books
{
    public class PageImageUrl : IPageFixed
    {
        public Uri Uri
        {
            get; private set;
        }

        public IPageOptions Option
        {
            get; set;
        }

        public PageImageUrl(Uri uri) { this.Uri = uri; }

        public async Task<Windows.UI.Xaml.Media.Imaging.BitmapImage> GetBitmapAsync()
        {
            return new Windows.UI.Xaml.Media.Imaging.BitmapImage(Uri);
        }

        public async Task<bool> UpdateRequiredAsync()
        {
            return false;
        }

        public async Task SaveImageAsync(StorageFile file,uint width)
        {
            //ToDo: Fix me!
            if (Uri.IsFile)
            {
                var thm = await (await StorageFile.GetFileFromApplicationUriAsync(Uri)).GetThumbnailAsync(Windows.Storage.FileProperties.ThumbnailMode.PicturesView);

                await Functions.SaveStreamToFile(thm, file);
            }
        }

        public async Task SetBitmapAsync(BitmapImage image)
        {
            image.UriSource = Uri;
        }
    }


    public class PageImageStream : IPageFixed
    {
        private IRandomAccessStream stream;

        public PageImageStream(IRandomAccessStream stream)
        {
            this.stream = stream;
        }

        public IPageOptions Option
        {
            get; set;
        }

        public Task<Windows.UI.Xaml.Media.Imaging.BitmapImage> GetBitmapAsync()
        {
            var image = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            stream.Seek(0);
            image.SetSource(stream);
            return Task.FromResult(image);
        }

        public async Task SaveImageAsync(StorageFile file,uint width)
        {
            await Functions.SaveStreamToFile(stream, file);
        }

        public async Task SetBitmapAsync(BitmapImage image)
        {
            stream.Seek(0);
            image.SetSource(stream);
        }

        public Task<bool> UpdateRequiredAsync()
        {
            return Task.FromResult(false);
        }
    }

    public class ImageBookUriCollection : IBookFixed
    {
        public Uri[] Content { get { return _Content; } private set { _Content = value; OnLoaded(new EventArgs()); } }
        private Uri[] _Content = new Uri[0];

        public ImageBookUriCollection(params Uri[] uri)
        {
            this.Content = uri;
        }

        public ImageBookUriCollection(params String[] uri)
        {
            var result = new Uri[uri.Count()];
            for(int i = 0; i < uri.Count(); i++)
            {
                result[i] = new Uri(uri[i]);
            }
            Content = result;
        }

        public uint PageCount => (uint)Content.Count();

        public string ID
        {
            get
            {
                string result = "";
                foreach(var item in Content)
                {
                    result += "\"" + item.GetHashCode() + "\"";
                }
                return Functions.GetHash(result);
            }
        }

        public event EventHandler Loaded;

        private void OnLoaded(EventArgs e)
        {
            Loaded?.Invoke(this, e);
        }

        public IPageFixed GetPage(uint i)
        {
            return new PageImageUrl(Content[i]);
        }
    }
}
