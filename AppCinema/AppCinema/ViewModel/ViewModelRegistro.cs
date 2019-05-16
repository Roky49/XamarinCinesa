using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Repositories;
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
                    SessionService session = App.Locator.SessionService;
                    Cinephile usuario = await this.repo.GetUser(Email);
                    usuario.token = await this.repo.Login(Email,Pass);
                    session.Datos.Add(usuario);

                    await Application.Current.MainPage.Navigation.PushModalAsync(view);


                });
            }
        }
    }
}
