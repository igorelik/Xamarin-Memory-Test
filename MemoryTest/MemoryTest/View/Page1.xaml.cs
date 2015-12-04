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
    public partial class Page1 : ContentPage
    {
        public Page1ViewModel ViewModel
        {
            get { return ((ViewModelLocator) Application.Current.Resources["Locator"]).Page1VM; }
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


        public Page1()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = ViewModel;
        }

        ~Page1()
        {
            Debug.WriteLine("Destructor PAGE1");
        }

    }
}
