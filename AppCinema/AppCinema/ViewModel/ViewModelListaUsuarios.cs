using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Repositories;
using AppCinema.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCinema.ViewModel
{
    public class ViewModelListaUsuarios : ViewModelBase
    {
        RepositoryCinema repoCine;
        RepositoryMovie repoMovie;
        SessionService session;
        private ObservableCollection<Movie> _Movies;
        public ObservableCollection<Movie> Movies
        {
            get { return this._Movies; }
            set
            {
                this._Movies = value;
                OnPropertyChanged("Movies");
            }
        }
        public Command ShowMovieDetails
        {
            get
            {
                return new Command(async (movie) =>
                {                    
                    Movie tappedMovie = movie as Movie;                    
                    ViewModelPelicula viewmodel = new ViewModelPelicula();
                    viewmodel.Movie = await repoMovie.GetMovie(tappedMovie.ID);
                    ViewPelicula view = new ViewPelicula();
                    viewmodel.InList = await repoCine.CheckInList(tappedMovie.ID, App.Locator.SessionService.Email);
                    view.BindingContext = viewmodel;                    
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);

                });
            }
        }
        public ViewModelListaUsuarios()
        {
            repoCine = new RepositoryCinema();
            repoMovie = new RepositoryMovie();
            session = App.Locator.SessionService;
            Task.Run(async() => {
                await this.LoadList();
            });
            MessagingCenter.Subscribe<ViewModelListaUsuarios>(this, "RELOAD", async (sender) => {
                await this.LoadList();
            });
        }
        private async Task LoadList()
        {
            List<Lists> listMovies = await repoCine.GetUserList(session.Email, session.token);
            List<Movie> movies = new List<Movie>();
            if (listMovies != null)
            {
                foreach (Lists lItem in listMovies)
                {
                    movies.Add(await repoMovie.GetMovie(lItem.IdMovie));
                }
            }
            this.Movies = new ObservableCollection<Movie>(movies);
        }
    }
}
