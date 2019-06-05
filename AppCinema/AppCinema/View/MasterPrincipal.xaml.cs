using AppCinema.Models;
using AppCinema.ViewModel;
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

        public MasterPrincipal()
        {
            InitializeComponent();




            Detail = new NavigationPage((Page)Activator.CreateInstance((typeof(ViewPrincipal))));
            IsPresented = false;

            this.lista.ItemSelected += CambiarPagina;


        }

        private void CambiarPagina(object sender, SelectedItemChangedEventArgs e)
        {
            MenuPagina sellecionado = (MenuPagina)e.SelectedItem;



            Detail = new NavigationPage((Page)Activator.CreateInstance(sellecionado.TipoPagina));
            IsPresented = false;

        }

      

        private async void CountriesSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            String Busqueda = CountriesSearchBar.Text;
            ViewBuscar view = new ViewBuscar();
            ViewModelBuscar viewModel = new ViewModelBuscar();

            viewModel.CadenaBuscar = Busqueda;

            view.BindingContext = viewModel;

            await Application.Current.MainPage.Navigation.PushModalAsync(view);
        }
    }
}