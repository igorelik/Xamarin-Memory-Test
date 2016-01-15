using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MemoryTest.Model;
using MemoryTest.Service;

namespace MemoryTest.ViewModel
{
    public class Page3ViewModel : ViewModelBase
    {
        private readonly IAppNavigationService _appNavigationService;
        private readonly IAppStateService _appStateService;
        public RelayCommand GoBackwardCommand { get; private set; }
        public RelayCommand<object> ToggleSelection { get; private set; }

        public Page3ViewModel()
        {
            
        }

        [PreferredConstructor]
        public Page3ViewModel(IAppNavigationService appNavigationService, IAppStateService appStateService)
        {
            _appNavigationService = appNavigationService;
            _appStateService = appStateService;
			GoBackwardCommand = new RelayCommand (async () => {
				Model1s = null;
				await _appNavigationService.GoBack ();
//				await _appNavigationService.GoBack (2);
			});
            ToggleSelection = new RelayCommand<object>(OnRowSelected);
            PageTitle = "Page Three-0";
        }

        public void OnAppearing()
        {
            PageTitle = "Page Three";
            //Model1s = _appStateService.GetDataForVM(100);
            var t = _appStateService.GetDataForVMAsync(200);
            t.Wait();
            Model1s = t.Result;
            SelectedModel = _appStateService.SelectedModel;
        }

        private void OnRowSelected(object rowData)
        {
            var model = rowData as Model1;
            if (model == null)
            {
                return;
            }
            model.IsSelected = !model.IsSelected;
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

        private IList<Model1> _model1S;
        public IList<Model1> Model1s
        {
            get { return _model1S; }
            set
            {
                _model1S = value; 
                RaisePropertyChanged();
                RaisePropertyChanged(()=>NumberOfModels);
            }
        }

        public int NumberOfModels { get { return Model1s == null ? 0 : Model1s.Count(); } }
    }

}
