using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCinema.ViewModel
{
    public class ViewModelPelicula : ViewModelBase
    {
        RepositoryMovie repoMovie;
        RepositoryCinema repoCine;
        SessionService session;
        public bool InList { get; set; }
        private Movie _Movie;
        public Movie Movie
        {
            get { return this._Movie; }
            set
            {
                this._Movie = value;
                this.Genres = new ObservableCollection<Genre>(value.Genres);
                this.Cast = new ObservableCollection<Cast>(value.People.Cast);
                OnPropertyChanged("Movie");
            }
        }
        private ObservableCollection<Genre> _Genres;
        public ObservableCollection<Genre> Genres
        {
            get { return this._Genres; }
            set
            {
                this._Genres = value;
                OnPropertyChanged("Genres");
            }
        }
        private ObservableCollection<Cast> _Cast;
        public ObservableCollection<Cast> Cast
        {
            get { return this._Cast; }
            set
            {
                this._Cast = value;
                OnPropertyChanged("Cast");
            }
        }
        public Command AddToList
        {
            get
            {
                return new Command(async () => {
                    if (session.Email != null)
                    {
                        await repoCine.AddMovieToList(Movie.ID, session.Email);
                        await Application.Current.MainPage.DisplayAlert("Pelicula añadida a tu lista", "", "OK");
                        this.InList = await repoCine.CheckInList(Movie.ID, session.Email);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Inicia sesion para añadir peliculas", "", "OK");
                    }
                });
            }            
        }
        public Command RemoveFromList
        {
            get
            {
                return new Command(async () => {
                    if (session.Email != null)
                    {
                        await repoCine.RemoveMovieFromList(Movie.ID, session.Email);
                        await Application.Current.MainPage.DisplayAlert("Pelicula retirada de tu lista", "", "OK");
                        this.InList = await repoCine.CheckInList(Movie.ID, session.Email);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Inicia sesion para retirar peliculas", "", "OK");
                    }
                });
            }            
        }        
        public ViewModelPelicula()
        {
            InList = false;
            repoMovie = new RepositoryMovie();
            repoCine = new RepositoryCinema();
            session = App.Locator.SessionService;
            Task.Run(async() => {                
                if(session.Email != null)
                    this.InList = await repoCine.CheckInList(Movie.ID, session.Email);
            });
        }
    }
}
