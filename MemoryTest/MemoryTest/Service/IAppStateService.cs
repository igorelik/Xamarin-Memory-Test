using System.Collections.Generic;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using MemoryTest.Model;
using Xamarin.Forms;

namespace MemoryTest.Service
{
    public interface IAppStateService
    {
        IList<Model1> GetDataForVM(int size);
        Task<IList<Model1>> GetDataForVMAsync(int size);
		Task<IList<GridModel>> GetDataForGridVMAsync (int size);
        Model1 SelectedModel { get; }
    }
}
