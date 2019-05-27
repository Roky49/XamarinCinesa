using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Repositories;
using System;
using System.Collections.Generic;
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
                OnPropertyChanged("Movie");
            }
        }
        public ViewModelPelicula(int movieId)
        {
            repoMovie = new RepositoryMovie();
            Task.Run(async() => {
                this.Movie = await repoMovie.GetMovie(movieId);
            });
        }
    }
}
