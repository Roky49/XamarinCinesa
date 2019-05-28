using AppCinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCinema.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPrincipal : MasterDetailPage
    {
        public List<MenuPagina> menuPaginas { set; get; }
        public MasterPrincipal()
        {
            InitializeComponent();
            
            //this.menuPaginas = new List<MenuPagina>();
            //if (App.Locator.SessionService.token != null)
            //{
            //    this.menuPaginas.Clear();
            //    var page1 = new MenuPagina() { TipoPagina = typeof(ViewPerfil), Titulo = "Hola "+App.Locator.SessionService.Name };
            //    var userMovieList = new MenuPagina() { TipoPagina = typeof(ViewListaUsuario), Titulo = "Mi lista" };
            //    this.menuPaginas.Add(page1);
            //    this.menuPaginas.Add(userMovieList);
            //    var page99 = new MenuPagina() { TipoPagina = typeof(ViewCerrarSesion), Titulo = "Cerrar Sesion" };
            //    this.menuPaginas.Add(page99);
            //}
            //else
            //{
            //    this.menuPaginas.Clear();
            //    var page1 = new MenuPagina() { TipoPagina = typeof(ViewLogin), Titulo = "Login" };
            //    this.menuPaginas.Add(page1);
            //}            
            //var page2 = new MenuPagina() { TipoPagina = typeof(ViewRegistro), Titulo = "Nuevo registro" };

            //this.menuPaginas.Add(page2);

            //var page3 = new MenuPagina() { TipoPagina = typeof(ViewPrueba), Titulo = "Pagina de prueba" };
            //this.menuPaginas.Add(page3);

            

            

            Detail = new NavigationPage((Page)Activator.CreateInstance((typeof(ViewPrincipal))));
            IsPresented = false;


            this.lista.ItemsSource = this.menuPaginas;

            this.lista.ItemSelected += CambiarPagina;


        }

        private void CambiarPagina(object sender, SelectedItemChangedEventArgs e)
        {
            MenuPagina sellecionado = (MenuPagina)e.SelectedItem;
            Detail = new NavigationPage((Page)Activator.CreateInstance(sellecionado.TipoPagina));
            IsPresented = false;
        }
    }
}