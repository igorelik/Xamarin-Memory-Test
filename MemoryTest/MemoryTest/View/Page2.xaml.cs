using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoryTest.ViewModel;
using Xamarin.Forms;

namespace MemoryTest.View
{
	public partial class Page2 : ViewBase
    {
        public Page2ViewModel ViewModel
        {
            get { return ((ViewModelLocator)Application.Current.Resources["Locator"]).Page2VM; }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnDisappearing();
        }
        public Page2()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = ViewModel;
        }
        ~Page2()
        {
            Debug.WriteLine("Destructor PAGE2");
        }
    }
}
