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
        RepositoryCinema repoCine;
        private ObservableCollection<DiscoverMovie> _Movies;
        public String CadenaBuscar;

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
                    DiscoverMovie tappedMovie = movie as DiscoverMovie;
                    ViewModelPelicula viewmodel = new ViewModelPelicula();
                    viewmodel.Movie = await repoMovie.GetMovie(tappedMovie.ID);
                    ViewPelicula view = new ViewPelicula();                    
                    if (App.Locator.SessionService != null)
                    {
                        viewmodel.InList = await repoCine.CheckInList(tappedMovie.ID, App.Locator.SessionService.Email);
                    }                        
                    view.BindingContext = viewmodel;
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);

                });
            }
        }
        public ViewModelBuscar()
        {
            repoMovie = new RepositoryMovie();
            repoCine = new RepositoryCinema();
            Task.Run(async() => {
                DiscoverMovieRequest request = await repoMovie.SearchMovie(CadenaBuscar);
                this.Movies = new ObservableCollection<DiscoverMovie>(request.Movies);
            });
        }
    }
}
