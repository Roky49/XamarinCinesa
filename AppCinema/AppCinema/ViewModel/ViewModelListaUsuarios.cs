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
                this._Movies = new ObservableCollection<Movie>(movies);
            });
        }
    }
}
