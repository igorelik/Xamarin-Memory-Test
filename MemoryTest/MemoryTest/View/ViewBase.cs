using Xamarin.Forms;

namespace MemoryTest
{
	public class ViewBase: ContentPage
	{
		public NavigationPage NavigationPage {get;set;}

		protected NavigationPage GetContainingNavigationPage()
		{
			return GetContainingNavigationPage (this);
		}

		protected NavigationPage GetContainingNavigationPage(Element element)
		{
			var parentElement = element.Parent;

			if (parentElement == null)
				return null;

			if (parentElement is NavigationPage)
				return (NavigationPage)parentElement;
			return GetContainingNavigationPage(parentElement);
		}

		private void OnPagePopped(object s, NavigationEventArgs e)
		{
			if (e.Page == this)
			{
				BindingContext = null;
				NavigationPage.Popped -= OnPagePopped;
			}
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			NavigationPage = GetContainingNavigationPage ();
			if (NavigationPage != null) 
			{
				NavigationPage.Popped += OnPagePopped;
			}

		}

	}
}