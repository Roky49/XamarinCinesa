using AppCinema.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCinema.ViewModel
{
    public class ViewModelCerrarSesion
    {
        public ViewModelCerrarSesion()
        {
            Task.Run(async () => {

                
                App.Locator.SessionService.Email = null;
                App.Locator.SessionService.Image = null;
                App.Locator.SessionService.Name = null;
                App.Locator.SessionService.Password = null;
                App.Locator.SessionService.token = null;
                App.Locator.SessionService.Age = 0 ;

                Application.Current.MainPage = new MasterPrincipal();

              



            });
            
            
        }

    }
}
