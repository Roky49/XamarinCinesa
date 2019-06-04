using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCinema.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewEfecto : ContentPage
	{
		public ViewEfecto ()
		{
			InitializeComponent ();
            Task.Run(async () => {

                await this.palomitas.RotateTo(1080, 5000, Easing.CubicOut);

            });
        }
	}
}