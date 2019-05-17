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
       

        public ViewModelLogin()
        {

           
            this.repo = new RepositoryCinema();
          

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

                        Cinephile usuario = await this.repo.GetUser(user,token);

                        //SessionService session = App.Locator.SessionService ;
                        //session.Name = usuario.Name;
                        //App.Locator.SessionService.User.token = token;
                        //App.Locator.SessionService.User.Age = usuario.Age;
                        //App.Locator.SessionService.User.Email = usuario.Email;
                        //App.Locator.SessionService.User.Image = usuario.Image;
                        //App.Locator.SessionService.User.LastName = usuario.LastName;
                        //App.Locator.SessionService.User.Name = usuario.Name;
                        //App.Locator.SessionService.User.Password = usuario.Password;

                        ViewPrueba view = new ViewPrueba();
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                    }

                });
            }
        }
    }
}
