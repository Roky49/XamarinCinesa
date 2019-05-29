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
                this.Cast = new ObservableCollection<Cast>(value.People.Cast);
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
        private ObservableCollection<Cast> _Cast;
        public ObservableCollection<Cast> Cast
        {
            get { return this._Cast; }
            set
            {
                this._Cast = value;
                OnPropertyChanged("Cast");
            }
        }
        public ViewModelPelicula()
        {
            repoMovie = new RepositoryMovie();
            InstanceCarouselPeople();
            Task.Run(async() => {
                this.Movie = await repoMovie.GetMovie(121);
            });
        }
        private void InstanceCarouselPeople()
        {
            List<Cast> initialCast = new List<Cast>();
            initialCast.Add(new Cast() { ID = 0, Name = "John Doe", Character = "John Doe", ProfilePath = "" });
            this.Cast = new ObservableCollection<Cast>(initialCast);
        }
    }
}
