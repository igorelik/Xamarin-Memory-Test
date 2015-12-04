using GalaSoft.MvvmLight.Ioc;
using MemoryTest.Service;
using MemoryTest.View;
using MemoryTest.ViewModel;
using Xamarin.Forms;

namespace MemoryTest
{
    public static class IOCConfig
    {
        public static void Configure()
        {
            RegisterServices();
            RegisterViewModels();
            RegisterPages();
        }

        public static void RegisterViewModels()
        {
            SimpleIoc.Default.Register<Page1ViewModel>();
            SimpleIoc.Default.Register<Page2ViewModel>();
            SimpleIoc.Default.Register<Page3ViewModel>();
        }

        public static void RegisterServices()
        {
            SimpleIoc.Default.Register<IAppNavigationService, AppNavigationService>();
            SimpleIoc.Default.Register<IAppStateService, AppStateService>();
        }

        public static void RegisterPages()
        {
            SimpleIoc.Default.Register<Page1>();
            SimpleIoc.Default.Register(() => new NavigationPage(SimpleIoc.Default.GetInstance<Page1>()), ViewModelLocator.Page1PageKey);
        }
    }
}
