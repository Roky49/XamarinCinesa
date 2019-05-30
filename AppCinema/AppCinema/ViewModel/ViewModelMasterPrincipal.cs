using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Repositories;
using AppCinema.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AppCinema.ViewModel
{
    class ViewModelMasterPrincipal:ViewModelBase
    {
        RepositoryCinema repo;
        SessionService session;

        private List<MenuPagina> GetMenuPaginas()
        {

            List<MenuPagina> menuPaginas = new List<MenuPagina>();

                if (App.Locator.SessionService.token != null)
                {
                    menuPaginas.Clear();
                    var page1 = new MenuPagina() { TipoPagina = typeof(ViewPerfil), Titulo = "Hola " + App.Locator.SessionService.Name };
                menuPaginas.Add(page1);
                
                var userMovieList = new MenuPagina() { TipoPagina = typeof(ViewListaUsuario), Titulo = "Mi lista" };
                 
                    menuPaginas.Add(userMovieList);
                    var page99 = new MenuPagina() { TipoPagina = typeof(ViewCerrarSesion), Titulo = "Cerrar Sesion" };
                    menuPaginas.Add(page99);
                }
                else
                {
                    menuPaginas.Clear();
                    var page1 = new MenuPagina() { TipoPagina = typeof(ViewLogin), Titulo = "Login" };
                    menuPaginas.Add(page1);
                }
                var page2 = new MenuPagina() { TipoPagina = typeof(ViewRegistro), Titulo = "Nuevo registro" };

                menuPaginas.Add(page2);

                var page3 = new MenuPagina() { TipoPagina = typeof(ViewPrueba), Titulo = "Pagina de prueba" };
                menuPaginas.Add(page3);

           

            return menuPaginas;

          
        }

        public ViewModelMasterPrincipal()
        {


            this.repo = new RepositoryCinema();
            this.session = App.Locator.SessionService;
            Task.Run(  () => {
                List<MenuPagina> menu = GetMenuPaginas();
                this.MenuPaginas =  new ObservableCollection<MenuPagina>(menu);
           
          
        });

        }

        private ObservableCollection<MenuPagina> _menuPaginas;

        public ObservableCollection<MenuPagina> MenuPaginas
        {
            set
            {
                this._menuPaginas = value;
                OnPropertyChanged("MenuPaginas");
            }

            get { return this._menuPaginas; }
        }

       


    }
}
