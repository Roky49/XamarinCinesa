using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AppCinema.ViewModel
{
    public class ViewModelBuscar : ViewModelBase
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
