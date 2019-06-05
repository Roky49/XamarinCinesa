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
        SessionService session;



        public ViewModelLogin()
        {

           
            this.repo = new RepositoryCinema();
            this.session =  App.Locator.SessionService;


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

                    Application.Current.MainPage = new ViewEfecto();
                    

                    String token = session.token; 
                   
                    if (token != null) {

                        Cinephile usuario = await this.repo.GetUser(user,token);
                        

                        
                        session.token = token;
                        session.Age = usuario.Age;
                        session.Email = usuario.Email;
                        session.Image = usuario.Image;
                        session.LastName = usuario.LastName;
                        session.Name = usuario.Name;
                        session.Password = usuario.Password;
                        MasterUsuario master = new MasterUsuario();
                        await Application.Current.MainPage.Navigation.PushModalAsync(master);

                    }
                    else
                    {
                        token = await this.repo.Login(user, pass);

                        if (token != null) { 

                            Cinephile usuario = await this.repo.GetUser(user, token);

                        session.token = token;
                            session.Age = usuario.Age;
                            session.Email = usuario.Email;
                            session.Image = usuario.Image;
                            session.LastName = usuario.LastName;
                            session.Name = usuario.Name;
                            session.Password = usuario.Password;
                            MasterUsuario master = new MasterUsuario();
                            await Application.Current.MainPage.Navigation.PushModalAsync(master);
                        }
                        else
                        {
                            ViewLogin master = new ViewLogin();
                            await Application.Current.MainPage.Navigation.PushModalAsync(master);
                        }

                    }

                });
            }
        }
    }
}
