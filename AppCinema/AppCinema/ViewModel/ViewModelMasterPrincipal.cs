using AppCinema.Base;
using AppCinema.Models;
using AppCinema.Repositories;
using AppCinema.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCinema.ViewModel
{
    public class ViewModelMasterPrincipal : ViewModelBase
    {
        RepositoryCinema repo;
        SessionService session;

        private List<MenuPagina> GetMenuPaginas()
        {

            List<MenuPagina> menuPaginas = new List<MenuPagina>();

            if (App.Locator.SessionService.token == null)
            {

                var page1 = new MenuPagina() { TipoPagina = typeof(ViewLogin), Titulo = "Login" };
                menuPaginas.Add(page1);

                var page3 = new MenuPagina() { TipoPagina = typeof(ViewPrincipal), Titulo = "Home" };
                menuPaginas.Add(page3);

                var page2 = new MenuPagina() { TipoPagina = typeof(ViewRegistro), Titulo = "Nuevo registro" };

                menuPaginas.Add(page2);

               


                

            }


            return menuPaginas;


        }

        public ViewModelMasterPrincipal()
        {


            this.repo = new RepositoryCinema();
            this.session = App.Locator.SessionService;
            Task.Run(() => {
                List<MenuPagina> menu = GetMenuPaginas();
                this.MenuPaginas = new ObservableCollection<MenuPagina>(menu);

                MessagingCenter.Subscribe<ViewModelMasterPrincipal>(this, "actualizar", (sender) => { GetMenuPaginas(); });


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
