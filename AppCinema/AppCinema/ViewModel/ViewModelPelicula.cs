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
    public class ViewModelPelicula : ViewModelBase
    {
        RepositoryMovie repoMovie;
        private Movie _Movie;
        public Movie Movie
        {
            get { return this._Movie; }
            set
            {
                this._Movie = value;
                this.Genres = new ObservableCollection<Genre>(value.Genres);
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
        public ViewModelPelicula()
        {
            repoMovie = new RepositoryMovie();
            Task.Run(async() => {
                this.Movie = await repoMovie.GetMovie(120);
            });
        }
    }
}
