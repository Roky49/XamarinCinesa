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
    public class ViewModelPerfil:ViewModelBase
    {
        RepositoryCinema repo;
        RepositoryMovie repomovie;

        public ViewModelPerfil()
        {
            this.repo = new RepositoryCinema();
            
            //App.Locator.SessionService.Datos.
            SessionService session = App.Locator.SessionService;
            Task.Run(async () => {
                this.Usuario = await this.repo.GetUser(session.Email, session.token);
                this.Movie = await this.repo.GetUserList(session.Email, session.token);
                List<Lists> listMovies = await this.repo.GetUserList(session.Email, session.token);
                List<Movie> movies = new List<Movie>();
                if (listMovies != null)
                {
                    foreach (Lists lItem in listMovies)
                    {
                        movies.Add(await this.repomovie.GetMovie(lItem.IdMovie));
                    }
                }
                this._Movies = new ObservableCollection<Movie>(movies);
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

        private List<Lists> _Movie;

        public List<Lists> Movie
        {
            set
            {
                this._Movie = value;
                OnPropertyChanged("Movie");
            }

            get { return this._Movie; }
        }

        private ObservableCollection<Movie> _Movies;
        public ObservableCollection<Movie> Movies
        {
            set
            {
                this._Movies = value;
                OnPropertyChanged("Movies");
            }

            get { return this._Movies; }
        }
    }
}
