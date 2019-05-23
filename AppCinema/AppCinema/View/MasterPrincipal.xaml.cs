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
	public partial class MasterModelPrincipal : MasterDetailPage
    {
        public List<MenuPagina> menuPaginas { set; get; }
         
        public MasterModelPrincipal()
        {
            InitializeComponent();
            
            this.menuPaginas = new List<MenuPagina>();
            if (App.Locator.SessionService.token != null)
            {
                this.menuPaginas.Clear();
                var page1 = new MenuPagina() { TipoPagina = typeof(ViewLogin), Titulo = "Hola "+App.Locator.SessionService.Name };
                this.menuPaginas.Add(page1);
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

            var page4 = new MenuPagina() { TipoPagina = typeof(ViewRegistro), Titulo = "Inicio" };

            this.menuPaginas.Add(page4);
            if (App.Locator.SessionService.token != null)
            {
                var page99 = new MenuPagina() { TipoPagina = typeof(ViewCerrarSesion), Titulo = "Cerrar Sesion" };
                this.menuPaginas.Add(page99);
            }
            Detail = new NavigationPage((Page)Activator.CreateInstance((typeof(ViewPrincipal))));
            IsPresented = false;

            this.lista.ItemsSource = null;
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