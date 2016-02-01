using System;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace MemoryTest.Controls
{
	public interface IFastImageProvider
	{
        void SetImageUrl(FastImage imageUrl);
    }

	public class FastImage : Image
	{

		public FastImage ()
		{
			//Aspect = Aspect.AspectFit; 
			
		}

		public static readonly BindableProperty ImageUrlProperty = BindableProperty.Create<FastImage, string> (w => w.ImageUrl, null);

		/// <summary>
		/// sets the image URL.
		/// </summary>
		/// <value>The image URL.</value>
		public string ImageUrl {
			get { return (string)GetValue (ImageUrlProperty); }
			set { 
				SetValue (ImageUrlProperty, value);
			}
		}
	}
}

