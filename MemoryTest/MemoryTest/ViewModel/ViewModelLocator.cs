/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MemoryTest"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MemoryTest.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public const string Page1PageKey = "Page1";
        public const string Page2PageKey = "Page2";
        public const string Page3PageKey = "Page3";

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public static void ConfigureViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
        }

        public Page1ViewModel Page1VM
        {
            get { return ServiceLocator.Current.GetInstance<Page1ViewModel>(); }
        }

        public Page2ViewModel Page2VM
        {
            get { return ServiceLocator.Current.GetInstance<Page2ViewModel>(); }
        }

        public Page3ViewModel Page3VM
        {
            get { return ServiceLocator.Current.GetInstance<Page3ViewModel>(); }
        }


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}