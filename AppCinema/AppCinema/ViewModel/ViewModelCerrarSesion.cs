﻿using AppCinema.View;
using MonkeyCache.FileStore;
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

                MasterPrincipal master = new MasterPrincipal();
                ViewModelMasterPrincipal viewModel = new ViewModelMasterPrincipal();

                master.BindingContext = viewModel;
                await Application.Current.MainPage.Navigation.PushModalAsync(master);

                ViewPrueba view2 = new ViewPrueba(); 
                await Application.Current.MainPage.Navigation.PushModalAsync(view2);

                Barrel.Current.EmptyAll();

            });
            
            
        }

    }
}
