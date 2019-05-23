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
        private ObservableCollection<DiscoverMovie> _InTheatreMovies;
        public ObservableCollection<DiscoverMovie> InTheatreMovies
        {
            get { return this._InTheatreMovies; }
            set
            {
                this._InTheatreMovies = value;
                OnPropertyChanged("InTheatreMovies");
            }
        }
        private ObservableCollection<DiscoverMovie> _KidsMovies;
        public ObservableCollection<DiscoverMovie> KidsMovies
        {
            get { return this._KidsMovies; }
            set
            {
                this._KidsMovies = value;
                OnPropertyChanged("KidsMovies");
            }
        }
        private ObservableCollection<DiscoverMovie> _SpainMovies;
        public ObservableCollection<DiscoverMovie> SpainMovies
        {
            get { return this._SpainMovies; }
            set
            {
                this._SpainMovies = value;
                OnPropertyChanged("SpainMovies");
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
            this.InTheatreMovies = new ObservableCollection<DiscoverMovie>(coleccion);
            this.KidsMovies = new ObservableCollection<DiscoverMovie>(coleccion);
            this.SpainMovies = new ObservableCollection<DiscoverMovie>(coleccion);
            repoMovie = new RepositoryMovie();
            //Ahora, rellena el carousel con la informacion de verdad
            Task.Run(async () =>
            {
                DiscoverMovieRequest request = await repoMovie.DiscoverInTheatreMovies(Sort.PopularityDesc, IncludeAdult.Yes);
                this.InTheatreMovies = new ObservableCollection<DiscoverMovie>(request.Movies);

                request = await repoMovie.DiscoverSpainMovies(Sort.PopularityDesc, IncludeAdult.Yes);
                this.SpainMovies = new ObservableCollection<DiscoverMovie>(request.Movies);

                request = await repoMovie.DiscoverKidsMovies(Sort.PopularityDesc, IncludeAdult.No);
                this.KidsMovies = new ObservableCollection<DiscoverMovie>(request.Movies);
            });
        }
    }
}

