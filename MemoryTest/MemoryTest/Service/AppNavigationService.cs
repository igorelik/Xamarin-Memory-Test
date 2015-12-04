using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MemoryTest.Service
{
    public class AppNavigationService : IAppNavigationService
    {
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private NavigationPage _navigation;
        //private static List<WeakReference> pageRefs = new List<WeakReference>();

        public string CurrentPageKey
        {
            get
            {
                if (_navigation.CurrentPage == null)
                {
                    return null;
                }

                var pageType = _navigation.CurrentPage.GetType();

                lock (_pagesByKey)
                {
                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey.First(p => p.Value == pageType).Key
                        : null;
                }
            }
        }

        public void GoBack()
        {
            _navigation.PopAsync();
        }

        public async Task GoBack(int numberOfPages)
        {
            for (var i = 0; i < numberOfPages - 1; i++)
            {
                var pageToDelete = _navigation.Navigation.NavigationStack[_navigation.Navigation.NavigationStack.Count - 2];
                //if (pageToDelete.BindingContext != null)
                //{
                //    pageToDelete.BindingContext = null;
                //}
                _navigation.Navigation.RemovePage(pageToDelete);
            }
            var lastPage = await _navigation.PopAsync(true);
            //if (lastPage.BindingContext != null)
            //{
            //    lastPage.BindingContext = null;
            //}
            GC.Collect();

            //var wr = pageRefs.SingleOrDefault(pr => pr.Target == lastPage);
            //if (wr != null)
            //{
            //    pageRefs.Remove(wr);
            //}

            Debug.WriteLine("Currently Allocated (GoBack): {0}", GC.GetTotalMemory(true));
            DelayedGCAsync();
        }

        private static async Task DelayedGCAsync()
        {
            await Task.Delay(2000);
            GC.Collect();
            Debug.WriteLine("Currently Allocated (DelayedGCAsync): {0}", GC.GetTotalMemory(true));
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            var task = NavigateToAsync(pageKey, parameter);
            Task.WaitAll(task);
        }

        //        Dictionary<string, Page> _views = new Dictionary<string, Page>();

        public async Task NavigateToAsync(string pageKey, object parameter = null)
        {
            Page pageToNavigate;
            //if (_views.ContainsKey(pageKey))
            //{
            //    pageToNavigate = _views[pageKey];
            //    Debug.WriteLine("navpage cached " + pageToNavigate.Title);
            //}
            //else
            //{


            Type pageType = GetPageType(pageKey);

            PageConstructor constructor = (parameter == null)
                ? PageConstructor.GetParameterlessConstructor(pageType)
                : PageConstructor.GetSingleParameterConstructor(pageType, parameter);

            pageToNavigate = constructor.CreateInstance();
            //           _views.Add(pageKey, pageToNavigate);
            //                pageRefs.Add(new WeakReference(pageToNavigate));
            Debug.WriteLine("navpage " + pageToNavigate.Title);
            //       }

            await _navigation.PushAsync(pageToNavigate, true);
            Debug.WriteLine("Currently Allocated (NavigateTo): {0}", GC.GetTotalMemory(true));
            DelayedGCAsync();
        }

        private Type GetPageType(string pageKey)
        {
            lock (_pagesByKey)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    var message = string.Format("No such page: {0}. Did you forget to call AppNavigationService.Configure?", pageKey);
                    throw new ArgumentException(message, "pageKey");
                }

                return _pagesByKey[pageKey];
            }
        }

        public void Configure(string pageKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        public void Initialize(NavigationPage navigation)
        {
            _navigation = navigation;
        }
    }
}
