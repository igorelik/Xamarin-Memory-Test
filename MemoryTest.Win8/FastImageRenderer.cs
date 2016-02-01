using System;
using System.ComponentModel;
using Windows.UI.Xaml.Media.Imaging;
using MemoryTest.Controls;
using MemoryTest.Win8;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;

[assembly: ExportRenderer(typeof(Label), typeof(FastLabelRenderer))]
[assembly: ExportRenderer(typeof(Image), typeof(FastImageRenderer))]
[assembly: ExportRenderer(typeof(FastImage), typeof(FastImageRenderer))]
namespace MemoryTest.Win8
{
    public class FastLabelRenderer : LabelRenderer
    {
        public FastLabelRenderer()
        {
            
        }
    }

    public class FastImageRenderer : ImageRenderer, IFastImageProvider
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var fastImage = e.NewElement as FastImage;
                if (fastImage != null)
                {
                    SetImageUrl(fastImage);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == FastImage.ImageUrlProperty.PropertyName)
            {
                var fastImage = Element as FastImage;
                if (fastImage != null)
                {
                    SetImageUrl(fastImage);
                }
            }
        }

        #region FastImageProvider implementation

        public void SetImageUrl(FastImage image)
        {
            if (Control == null)
            {
                return;
            }
            if (image.ImageUrl != null)
            {
                Uri imgUri;
                try
                {
                    imgUri = new Uri(image.ImageUrl);
                }
                catch (Exception)
                {
                    imgUri = new Uri("ms-appx:/" + image.ImageUrl);
                }
                Control.Source = new BitmapImage(imgUri);
                if (image.WidthRequest > 0)
                {
                    Control.Width = image.WidthRequest;
                }
                if (image.HeightRequest > 0)
                {
                    Control.Height = image.HeightRequest;
                }
            }
        }

        #endregion


    }
}
