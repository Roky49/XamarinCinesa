using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Repositories;
using AppCinema.View;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppCinema.ViewModel
{
    public class ViewModelLogin: ViewModelBase 
    {
        
        RepositoryCinema repo;
        Login login;

        public ViewModelLogin()
        {
           
             
                this.repo = new RepositoryCinema();
            login = new Login();

        }


        private String _user;

        public String user
        {
            set
            {
                this._user = value;
                OnPropertyChanged("user");
            }

            get { return this._user; }
        }

        private String _pass;

        public string pass
        {
            set
            {
                this._pass = value;
                OnPropertyChanged("pass");
            }

            get { return this._pass; }
        }


        public Command Logueo
        {
            get
            {
                return new Command(async () => {



                    String token = await this.repo.Login(user, pass);
                    if (token != null) {
                        SessionService session = App.Locator.SessionService;
                        Cinephile usuario = await this.repo.GetUser(user);
                        usuario.token = token;
                        session.Datos.Add(usuario);
                        ViewPrueba view = new ViewPrueba();
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                    }

                });
            }
        }
    }
}
