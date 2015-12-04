using GalaSoft.MvvmLight.Ioc;
using MemoryTest.Service;
using MemoryTest.View;
using MemoryTest.ViewModel;
using Xamarin.Forms;

namespace MemoryTest
{
    public partial class App: Application
    {
        public static INavigation Navigation
        {
            get;
            set;
        }
        public App()
        {
            InitializeComponent();

            ViewModelLocator.ConfigureViewModelLocator();
            IOCConfig.Configure();

            var nav = SimpleIoc.Default.GetInstance<IAppNavigationService>();

            nav.Configure(ViewModelLocator.Page1PageKey, typeof(Page1));
            nav.Configure(ViewModelLocator.Page2PageKey, typeof(Page2));
            nav.Configure(ViewModelLocator.Page3PageKey, typeof(Page3));

            var navPage = SimpleIoc.Default.GetInstance<NavigationPage>(ViewModelLocator.Page1PageKey);
            nav.Initialize(navPage);

            MainPage = navPage;


        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
