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
            this.menuPaginas = new List<MenuPagina>();
            var page1 = new MenuPagina() { TipoPagina = typeof(ViewLogin), Titulo = "Login" };
            this.menuPaginas.Add(page1);
            var page2 = new MenuPagina() { TipoPagina = typeof(ViewRegistro), Titulo = "Nuevo registro" };

            this.menuPaginas.Add(page2);

            var page3 = new MenuPagina() { TipoPagina = typeof(ViewPrueba), Titulo = "Pagina de prueba" };
            this.menuPaginas.Add(page3);

            var page4 = new MenuPagina() { TipoPagina = typeof(ViewPerfil), Titulo = "Perfil" };
            this.menuPaginas.Add(page4);

            Detail = new NavigationPage((Page)Activator.CreateInstance((typeof(ViewLogin))));
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