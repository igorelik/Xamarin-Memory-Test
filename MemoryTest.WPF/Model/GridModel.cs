using System;

namespace MemoryTest
{
	public class GridModel
	{ 
		private static Random Randomisator = new Random();

		public string ImageUrl {get; set;}
		public string ImgLabel {get; set;}

		public static GridModel GenerateRandom()
		{
			var imageId = 11121 + Randomisator.Next () % 15;
			var res = new GridModel
			{
				ImgLabel = Guid.NewGuid().ToString(),
				ImageUrl = string.Format("BigImages/{0}.png", imageId)
			};
			return res;
		}

	}
}

