using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace MemoryTest.Service
{
    public interface IAppNavigationService : INavigationService
    {
        void Configure(string pageKey, Type pageType);
        void Initialize(NavigationPage navigation);
        Task GoBack(int numberOfPages = 1);
        Task NavigateToAsync(string pageKey, object parameter = null);
    }
}
