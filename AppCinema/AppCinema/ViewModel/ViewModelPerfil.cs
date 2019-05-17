using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCinema.ViewModel
{
    public class ViewModelPerfil:ViewModelBase
    {
        RepositoryCinema repo;
        

        public ViewModelPerfil()
        {


            this.repo = new RepositoryCinema();
            Usuario = new SessionService();
            //App.Locator.SessionService.Datos.
            SessionService session = App.Locator.SessionService;
            Usuario = session.User;
        }


        private SessionService _Usuario;

        public SessionService Usuario
        {
            set
            {
                this._Usuario = value;
                OnPropertyChanged("Usuario");
            }

            get { return this._Usuario; }
        }

       
    }
}
