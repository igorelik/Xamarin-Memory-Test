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
	public partial class Page3 : ViewBase
    {
        public Page3ViewModel ViewModel
        {
            get { return ((ViewModelLocator)Application.Current.Resources["Locator"]).Page3VM; }
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
        public Page3()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Resources["ViewModel"] = ViewModel;
            BindingContext = ViewModel;
        }
        ~Page3()
        {
            Debug.WriteLine("Destructor PAGE3");
        }

    }
}
