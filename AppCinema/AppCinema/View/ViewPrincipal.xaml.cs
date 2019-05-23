using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace AppCinema.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPrincipal : Xamarin.Forms.TabbedPage
    {
        public ViewPrincipal()
        {
            InitializeComponent();
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
        }
    }
}