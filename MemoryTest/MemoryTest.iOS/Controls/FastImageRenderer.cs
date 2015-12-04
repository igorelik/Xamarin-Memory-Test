using System.ComponentModel;
using MemoryTest.iOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MemoryTest.Controls;

[assembly: ExportRenderer (typeof(FastImage), typeof(FastImageRenderer))]
namespace MemoryTest.iOS.Controls
{
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
	                SetImageUrl(fastImage.ImageUrl);
	            }
	        }
	    }

	    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
	    {
	        base.OnElementPropertyChanged(sender, e);
	        if (e.PropertyName == "ImageUrl")
	        {
	            var fastImage = Element as FastImage;
	            if (fastImage != null)
	            {
	                SetImageUrl(fastImage.ImageUrl);
	            }
	        }
	    }

	    #region FastImageProvider implementation

	    public void SetImageUrl(string imageUrl)
	    {
	        if (Control == null)
	        {
	            return;
	        }
	        if (imageUrl != null)
	        {
	            Control.Image = UIImage.FromFile(imageUrl);

	        }
	    }

	    #endregion

		protected override void Dispose (bool disposing)
		{
			if (Control != null && Control.Image != null) 
			{
				var image = Control.Image;
				Control.Image.Dispose ();
				Control.Dispose ();
			}
			base.Dispose (disposing);
		}
	}
}

