using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using MemoryTest.Model;
using MemoryTest.Service;

namespace MemoryTest.ViewModel
{
    public class Page1ViewModel: ViewModelBase
    {
        private readonly IAppNavigationService _appNavigationService;
        private readonly IAppStateService _appStateService;
        public RelayCommand GoForwardCommand { get; private set; }

        public Page1ViewModel(IAppNavigationService appNavigationService, IAppStateService appStateService)
        {
            _appNavigationService = appNavigationService;
            _appStateService = appStateService;
            GoForwardCommand = new RelayCommand(async () => await _appNavigationService.NavigateToAsync(ViewModelLocator.Page2TPageKey));
        }

        public void OnAppearing()
        {
            PageTitle = "Page One";
            SelectedModel = _appStateService.SelectedModel;
        }

        private Model1 _selectedModel;
        public Model1 SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                _selectedModel = value;
                RaisePropertyChanged();
            }
        }

        public void OnDisappearing()
        {

        }

        private string _pageTitle;
        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                _pageTitle = value; 
                RaisePropertyChanged();
            }
        }


    }
}
