using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Providers;
using AppCinema.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AppCinema.ViewModel
{
    public class ViewModelPrincipal : ViewModelBase
    {
        RepositoryMovie repoMovie;
        private ObservableCollection<DiscoverMovie> _Movies;
        public ObservableCollection<DiscoverMovie> Movies
        {
            get { return this._Movies; }
            set
            {
                this._Movies = value;
                OnPropertyChanged("Movies");
            }
        }
        public ViewModelPrincipal()
        {
            //CarouselView se muestra antes de ejecutar el código del viewmodel, por lo que si 
            //la lista está vacia, el programa crashea

            //Instancia una nueva pelicula vacia para que el CarouselView no crashee la aplicación
            List<DiscoverMovie> coleccion = new List<DiscoverMovie>();
            DiscoverMovie movie = new DiscoverMovie()
            {
                ID = 1,
                PosterPath = "",
                Title = ""
            };
            coleccion.Add(movie);
            //Rellena la coleccion con la pelicula vacia
            this.Movies = new ObservableCollection<DiscoverMovie>(coleccion);
            repoMovie = new RepositoryMovie();
            //Ahora, rellena el carousel con la informacion de verdad
            Task.Run(async () => {
                DiscoverMovieRequest request = await repoMovie.DiscoverInTheatreMovies(Sort.PopularityDesc, IncludeAdult.Yes);
                this.Movies = new ObservableCollection<DiscoverMovie>(request.Movies);
            });
        }
    }
}

