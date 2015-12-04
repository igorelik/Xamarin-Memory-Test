using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MemoryTest.View.Controls
{
	public partial class SingleEntryView 
	{
		protected override void InitializeCell ()
		{
			InitializeComponent ();
		}


		protected override void SetupCell (bool isRecycled)
		{
			var mediaItem = BindingContext as GridModel;
			if (mediaItem != null) {
				// TODO PERFORMANCE
				ImageView.ImageUrl = mediaItem.ImageUrl;
			}
		}
	}
}

