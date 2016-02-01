using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MemoryTest.Model;
using MemoryTest.Service;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;

namespace MemoryTest.ViewModel
{
    public class Page2ViewModel : ViewModelBase
    {
        private readonly IAppNavigationService _appNavigationService;
        private readonly IAppStateService _appStateService;
        public RelayCommand GoForwardCommand { get; private set; }
        public RelayCommand GoBackwardCommand { get; private set; }

        public Page2ViewModel(IAppNavigationService appNavigationService, IAppStateService appStateService)
        {
            _appNavigationService = appNavigationService;
            _appStateService = appStateService;
            GoForwardCommand = new RelayCommand(async () => await _appNavigationService.NavigateToAsync(ViewModelLocator.Page3PageKey));
			GoBackwardCommand = new RelayCommand(async () => 
				{
					SelectedModel = null;
					await _appNavigationService.GoBack();
				});
			

        }

		private IList<GridModel> _models;
		public IList<GridModel> Models {
			get { return _models; }
			set
			{
				_models = value;
				RaisePropertyChanged ();
			}
		}

        public void OnAppearing()
        {
            PageTitle = "Page Two";
            Task.Run(async () =>
            {
                Models = await _appStateService.GetDataForGridVMAsync(80);
                SelectedModel = _appStateService.SelectedModel;
                if (Device.OS == TargetPlatform.Android)
                {
                    foreach (var model in Models)
                    {
                        model.ImageUrl = model.ImageUrl.Replace("/", "/a");
                    }
                }
                if (Device.OS == TargetPlatform.Windows)
                {
                    foreach (var model in Models)
                    {
                        model.ImageUrl = model.ImageUrl.Replace("BigImages/",
                            @"C:\Users\igorgorelik\AppData\Local\Packages\8cc3cc35-3c9e-49dd-9b56-726442badb37_z5cwy852zd69t\LocalState\")
                            .Replace(@"\", "/");
                    }
                }
            });
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
			//SimpleIoc.Default.Unregister (this);
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
