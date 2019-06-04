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
    public class ViewModelListaUsuarios : ViewModelBase
    {
        RepositoryCinema repoCine;
        RepositoryMovie repoMovie;
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
                    //Recuperamos la pelicula
                    Movie tappedMovie = movie as Movie;
                    //Creamos el viewmodel y vinculamos la pelicula                    
                    App.Locator.ViewModelPelicula.Movie = await repoMovie.GetMovie(tappedMovie.ID);
                    //Creamos la nueva view y vinculamos el viewmodel                    
                    App.Locator.ViewPelicula.BindingContext = App.Locator.ViewModelPelicula;
                    //Pusheamos la navegación
                    await Application.Current.MainPage.Navigation.PushModalAsync(App.Locator.ViewPelicula);

                });
            }
        }
        public ViewModelListaUsuarios()
        {
            repoCine = new RepositoryCinema();
            repoMovie = new RepositoryMovie();
            SessionService session = App.Locator.SessionService;
            Task.Run(async() => {
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
            });
        }
    }
}
