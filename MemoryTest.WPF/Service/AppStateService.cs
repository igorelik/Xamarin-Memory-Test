using System.Collections.Generic;
using System.Threading.Tasks;
using MemoryTest.Model;

namespace MemoryTest.Service
{
    public class AppStateService : IAppStateService
    {
        public IList<Model1> GetDataForVM(int size)
        {
            var result = new List<Model1>();
            for (var i = 0; i < size; i++)
            {
                var model = Model1.GenerateRandom();
                result.Add(model);
            }
            return result;
        }

        public Task<IList<Model1>> GetDataForVMAsync(int size)
        {
            return Task.Run(() => GetDataForVM(size));
        }

		public IList<GridModel> GetDataForGridVM(int size)
		{
			var result = new List<GridModel>();
			for (var i = 0; i < size; i++)
			{
				var model = GridModel.GenerateRandom();
				result.Add(model);
			}
			return result;
		}

		public Task<IList<GridModel>> GetDataForGridVMAsync(int size)
		{
			return Task.Run(() => GetDataForGridVM(size));
		}

        private Model1 _selectedModel;

        public Model1 SelectedModel
        {
            get
            {
                if (_selectedModel == null)
                {
                    _selectedModel = Model1.GenerateRandom();
                }
                return _selectedModel;
            }
        }
    }
}