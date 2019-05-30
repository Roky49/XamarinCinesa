using AppCinema.Base;
using AppCinema.View;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCinema.ViewModel
{
    public class ViewModelCerrarSesion:ViewModelBase
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
                Barrel.Current.EmptyAll();

                MasterPrincipal master = new MasterPrincipal();
                Xamarin.Forms.MessagingCenter.Send(App.Locator.ViewModelMasterPrincipal, "ViewModelMasterPrincipal");
                ViewModelMasterPrincipal viewModel =  new ViewModelMasterPrincipal() ;
              
                master.BindingContext = viewModel;
                OnPropertyChanged("MenuPaginas");
                await Application.Current.MainPage.Navigation.PushModalAsync(master);

                




            });
            
            
        }

    }
}
