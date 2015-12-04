using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MemoryTest.View.Controls
{
    public partial class GCInfoView 
    {
        public GCInfoView()
        {
            InitializeComponent();

            lblMemory.Text = GC.GetTotalMemory(true).ToString();
            //btnGCRun.Clicked += BtnGCRun_Clicked;
        }

        private void BtnGCRun_Clicked(object sender, EventArgs e)
        {
            lblMemory.Text = GC.GetTotalMemory(true).ToString();
        }
    }
}
