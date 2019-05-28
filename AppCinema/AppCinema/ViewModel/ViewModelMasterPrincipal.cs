using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Repositories;
using AppCinema.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCinema.ViewModel
{
    class ViewModelMasterPrincipal:ViewModelBase
    {
        RepositoryCinema repo;
        SessionService session;



        public ViewModelMasterPrincipal()
        {


            this.repo = new RepositoryCinema();
            this.session = App.Locator.SessionService;
            Task.Run( () => {
                this.menuPaginas = new List<MenuPagina>();
                
                if (App.Locator.SessionService.token != null)
                {
                    this.menuPaginas.Clear();
                    var page1 = new MenuPagina() { TipoPagina = typeof(ViewPerfil), Titulo = "Hola " + App.Locator.SessionService.Name };
                    var userMovieList = new MenuPagina() { TipoPagina = typeof(ViewListaUsuario), Titulo = "Mi lista" };
                    this.menuPaginas.Add(page1);
                    this.menuPaginas.Add(userMovieList);
                    var page99 = new MenuPagina() { TipoPagina = typeof(ViewCerrarSesion), Titulo = "Cerrar Sesion" };
                    this.menuPaginas.Add(page99);
                }
                else
                {
                    this.menuPaginas.Clear();
                    var page1 = new MenuPagina() { TipoPagina = typeof(ViewLogin), Titulo = "Login" };
                    this.menuPaginas.Add(page1);
                }
                var page2 = new MenuPagina() { TipoPagina = typeof(ViewRegistro), Titulo = "Nuevo registro" };

                this.menuPaginas.Add(page2);

                var page3 = new MenuPagina() { TipoPagina = typeof(ViewPrueba), Titulo = "Pagina de prueba" };
                this.menuPaginas.Add(page3);
                

                
            });

        }

        private List<MenuPagina> _menuPaginas;

        public List<MenuPagina> menuPaginas
        {
            set
            {
                this._menuPaginas = value;
                OnPropertyChanged("menuPaginas");
            }

            get { return this._menuPaginas; }
        }




    }
}
