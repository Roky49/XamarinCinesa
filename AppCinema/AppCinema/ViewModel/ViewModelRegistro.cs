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
        SessionService session;



        public ViewModelRegistro()
        {


            this.repo = new RepositoryCinema();
            session = App.Locator.SessionService;

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

                    Application.Current.MainPage = new ViewEfecto(); 

                    await this.repo.RegisterUser(Email,Pass,Name,LastName,Age);
                    session.token = await this.repo.Login(Email, Pass);
                    session.Age = Age;
                    session.Email = Email;
                    //session.Image = image;
                    session.LastName = LastName;
                    session.Name = Name;
                    session.Password = Pass;

                    MasterUsuario master = new MasterUsuario();
                    await Application.Current.MainPage.Navigation.PushModalAsync(master);

                    


                });
            }
        }
    }
}
