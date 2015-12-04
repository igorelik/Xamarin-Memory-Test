using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;

namespace MemoryTest.Model
{
    public class Model1 : ObservableObject
    {
		public static int RefCnounter = 0;
		private static Random Randomisator = new Random();

        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
		public string Field6 { get; set; }
		public string IconUrl { get; set; }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<object> ToggleSelection { get; private set; }

        public Model1()
        {
			RefCnounter++;
            ToggleSelection = new RelayCommand<object>(OnRowSelected);
        }

        private void OnRowSelected(object rowData)
        {
            IsSelected = !IsSelected;
        }

        public static Model1 GenerateRandom()
        {
			var imageId = Randomisator.Next () % 16;
            var res = new Model1
            {
                Field1 = Guid.NewGuid().ToString(),
                Field2 = Guid.NewGuid().ToString(),
                Field3 = Guid.NewGuid().ToString(),
                Field4 = Guid.NewGuid().ToString(),
                Field5 = Guid.NewGuid().ToString(),
                Field6 = Guid.NewGuid().ToString(),
				IconUrl = string.Format("SmallImages/{0}.jpg", imageId)
            };
            return res;
        }

		~Model1()
		{
			RefCnounter--;
			Debug.WriteLine ("DESTRUCTOR MODEL1 RC={0}", RefCnounter);
		}
    }
}
