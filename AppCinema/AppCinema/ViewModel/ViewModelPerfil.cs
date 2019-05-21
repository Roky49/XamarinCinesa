using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCinema.ViewModel
{
    public class ViewModelPerfil:ViewModelBase
    {
        RepositoryCinema repo;
      

        public ViewModelPerfil()
        {


            this.repo = new RepositoryCinema();
            
            //App.Locator.SessionService.Datos.
            SessionService session = App.Locator.SessionService;
            Task.Run(async () => {
                this.Usuario = await this.repo.GetUser(session.Email, session.token);
            });

           
        }


        private Cinephile _Usuario;

        public Cinephile Usuario
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
