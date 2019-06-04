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
    public class ViewModelBuscar : ViewModelBase
    {
        RepositoryMovie repoMovie;
        private ObservableCollection<DiscoverMovie> _Movies;
        private object e;

        public ObservableCollection<DiscoverMovie> Movies
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
                    DiscoverMovie tappedMovie = movie as DiscoverMovie;
                    //Creamos el viewmodel y vinculamos la pelicula                    
                    App.Locator.ViewModelPelicula.Movie = await repoMovie.GetMovie(tappedMovie.ID);                    
                    //Creamos la nueva view y vinculamos el viewmodel                    
                    App.Locator.ViewPelicula.BindingContext = App.Locator.ViewModelPelicula;
                    //Pusheamos la navegación
                    await Application.Current.MainPage.Navigation.PushModalAsync(App.Locator.ViewPelicula);

                });
            }
        }
        public ViewModelBuscar()
        {
            repoMovie = new RepositoryMovie();
            Task.Run(async() => {
                DiscoverMovieRequest request = await repoMovie.SearchMovie("avengers");
                this.Movies = new ObservableCollection<DiscoverMovie>(request.Movies);
            });
        }
    }
}
