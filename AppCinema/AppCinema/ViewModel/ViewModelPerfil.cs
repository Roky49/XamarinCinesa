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
    public class ViewModelPerfil:ViewModelBase
    {
        RepositoryCinema repo;
        
        RepositoryMovie repoMovie;

        public ViewModelPerfil()
        {
            this.repo = new RepositoryCinema();

            this.repoMovie = new RepositoryMovie();
            this.repo = new RepositoryCinema();
            
            //App.Locator.SessionService.Datos.
            SessionService session = App.Locator.SessionService;
            Task.Run(async () =>
            {
                this.Usuario = await this.repo.GetUser(session.Email, session.token);
                List<Lists> listMovies = await repo.GetUserList(session.Email, session.token);
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
        //public Command ShowMovieDetails
        //{
        //    get
        //    {
        //        return new Command(async (movie) =>
        //        {
        //            //Recuperamos la pelicula
        //            Movie tappedMovie = movie as Movie;
        //            //Creamos el viewmodel y vinculamos la pelicula                    
        //            App.Locator.ViewModelPelicula.Movie = await repoMovie.GetMovie(tappedMovie.ID);
        //            //Creamos la nueva view y vinculamos el viewmodel                    
        //            App.Locator.ViewPelicula.BindingContext = App.Locator.ViewModelPelicula;
        //            //Pusheamos la navegación
        //            await Application.Current.MainPage.Navigation.PushModalAsync(App.Locator.ViewPelicula);

        //        });
        //    }
        //}
      

    }
}
