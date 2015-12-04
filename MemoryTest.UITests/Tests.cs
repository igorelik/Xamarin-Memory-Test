using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Threading;

namespace MemoryTest.UITests
{
	//[TestFixture (Platform.Android)]
	[TestFixture (Platform.iOS)]
	public class Tests
	{
		IApp app;
		Platform platform;

		public Tests (Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest ()
		{
			app = AppInitializer.StartApp (platform);
		}

		[Test]
		public void AppLaunches ()
		{
			while (true) 
			{
				app.Tap (x => x.Text ("FORWARD"));
				app.ScrollDown ();
				Thread.Sleep (5000);
				app.Tap (x => x.Text ("FORWARD"));
				app.ScrollDown ();
				Thread.Sleep (5000);
				app.Tap (x => x.Text ("BACKWARD"));
				Thread.Sleep (5000);
				app.Tap (x => x.Text ("BACKWARD"));
				Thread.Sleep (15000);
			}
		}
	}
}

