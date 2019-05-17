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
    class ViewModelRegistro : ViewModelBase
    {
        RepositoryCinema repo;
        Cinephile NewUsers;

        public ViewModelRegistro()
        {


            this.repo = new RepositoryCinema();
            NewUsers = new Cinephile();

        }


        private String _Email;

        public String Email
        {
            set
            {
                this._Email = value;
                OnPropertyChanged("Email");
            }

            get { return this._Email; }
        }

        private String _Pass;

        public string Pass
        {
            set
            {
                this._Pass = value;
                OnPropertyChanged("Pass");
            }

            get { return this._Pass; }
        }

        private String _Name;

        public string Name
        {
            set
            {
                this._Name = value;
                OnPropertyChanged("Name");
            }

            get { return this._Name; }
        }

        private String _LastName;

        public string LastName
        {
            set
            {
                this._LastName = value;
                OnPropertyChanged("LastName");
            }

            get { return this._LastName; }
        }

        private int _Age;

        public int Age
        {
            set
            {
                this._Age = value;
                OnPropertyChanged("Age");
            }

            get { return this._Age; }
        }

        public Command NewUser
        {
            get
            {
                return new Command(async () => {



                    await this.repo.RegisterUser(Email,Pass,Name,LastName,Age);
                    App.Locator.SessionService.User.token = await this.repo.Login(Email, Pass);
                    Cinephile usuario = await this.repo.GetUser(Email, App.Locator.SessionService.User.token);
                    
                    App.Locator.SessionService.User.Age = usuario.Age;
                    App.Locator.SessionService.User.Email = usuario.Email;
                    App.Locator.SessionService.User.Image = usuario.Image;
                    App.Locator.SessionService.User.LastName = usuario.LastName;
                    App.Locator.SessionService.User.Name = usuario.Name;
                    App.Locator.SessionService.User.Password = usuario.Password;
                    ViewPrueba view = new ViewPrueba();
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);


                });
            }
        }
    }
}
